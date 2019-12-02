using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currentweaponscript : Photon.MonoBehaviour
{
    public int currentgun = 0;
    int availableguns = 2;

    public GameObject pistol;
    public GameObject pistolbarrel;

    public GameObject smg;
    public GameObject smgbarrel;

    public GameObject minigun;
    public GameObject minigunbarrel;


    int pistolpurchaseamountgiven = 21;
    int smgpurchaseamountgiven = 150;
    int minigunpurchaseamountgiven = 500;


    List<int> enabledweapons = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        enabledweapons.Add(0);

    }


    public void AddAmmoAndWeapon(int weapontype)
    {
        bool alreadyhasweapon = false;
        foreach (int num in enabledweapons)
        {
            if (num == weapontype)
            {
                alreadyhasweapon = true;
            }
        }

        if (!alreadyhasweapon)
        {
            enabledweapons.Add(weapontype);
        }



        if (weapontype == 0 && CheckIfPlayerHasWeapon(0)) //pistol
        {
            if (pistolbarrel.GetComponent<Gunraycast>().CurrentAmmoStorage < pistolbarrel.GetComponent<Gunraycast>().MaxAmmoStorage)
            {
                pistolbarrel.GetComponent<Gunraycast>().CurrentAmmoStorage += pistolpurchaseamountgiven;
            }
        }
        if (weapontype == 1 && CheckIfPlayerHasWeapon(1)) //smg
        {
            if (smgbarrel.GetComponent<Gunraycast>().CurrentAmmoStorage < smgbarrel.GetComponent<Gunraycast>().MaxAmmoStorage)
            {
                smgbarrel.GetComponent<Gunraycast>().CurrentAmmoStorage += smgpurchaseamountgiven;
            }
        }
        if (weapontype == 2 && CheckIfPlayerHasWeapon(2)) //minigun
        {
            if (minigunbarrel.GetComponent<Gunraycast>().CurrentAmmoStorage < minigunbarrel.GetComponent<Gunraycast>().MaxAmmoStorage)
            {
                minigunbarrel.GetComponent<Gunraycast>().CurrentAmmoStorage += minigunpurchaseamountgiven;
            }
        }
    }


    [PunRPC]
    void SetCurrentWeapon(int currentgunnum, int availgun)
    {
        currentgun = currentgunnum;
        availableguns = availgun;
    }

    bool CheckIfPlayerHasWeapon(int w)
    {
        if (photonView.isMine)
        {
            bool hasweapon = false;
            foreach (int num in enabledweapons)
            {
                if (num == w)
                {
                    hasweapon = true;
                }
            }

            return hasweapon;
        }
        else
        {
            return true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                currentgun--;
                
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                currentgun++;
                
            }

            if (currentgun < 0) currentgun = availableguns;
            if (currentgun > availableguns) currentgun = 0;


            photonView.RPC("SetCurrentWeapon", PhotonTargets.Others, currentgun, availableguns);

        }


        if(currentgun == 0 && CheckIfPlayerHasWeapon(0))
        {
            pistol.SetActive(true);

            smg.SetActive(false);
            minigun.SetActive(false);
        }
        if (currentgun == 1 && CheckIfPlayerHasWeapon(1))
        {
            smg.SetActive(true);

            pistol.SetActive(false);
            minigun.SetActive(false);
        }
        if (currentgun == 2 && CheckIfPlayerHasWeapon(2))
        {
            minigun.SetActive(true);

            pistol.SetActive(false);
            smg.SetActive(false);
        }
        
    }
}
