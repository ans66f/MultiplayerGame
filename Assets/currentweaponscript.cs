using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currentweaponscript : Photon.MonoBehaviour
{
    public int currentgun = 0;
    int availableguns = 2;

    public GameObject pistol;
    public GameObject smg;
    public GameObject minigun;


    List<int> enabledweapons = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        enabledweapons.Add(0);
    }


    public void ReloadWeapon(int weapontype)
    {
        bool alreadyhasweapon = false;
        foreach(int num in enabledweapons)
        {
            if(num == weapontype)
            {
                alreadyhasweapon = true;
            }
        }

        if(!alreadyhasweapon)
        {
            enabledweapons.Add(weapontype);
        }

    }

    public void BuyAmmo(int amount)
    {
        
    }


    [PunRPC]
    void SetCurrentWeapon(int currentgunnum)
    {
        currentgun = currentgunnum;
    }

    bool CheckIfPlayerHasWeapon(int w)
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



            photonView.RPC("SetCurrentWeapon", PhotonTargets.Others, currentgun);
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
