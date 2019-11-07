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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    [PunRPC]
    void SetCurrentWeapon(int currentgunnum)
    {
        currentgun = currentgunnum;
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

            switch (currentgun)
            {
                case 0:
                    pistol.SetActive(true);

                    smg.SetActive(false);
                    minigun.SetActive(false);
                    break;
                case 1:
                    smg.SetActive(true);

                    pistol.SetActive(false);
                    minigun.SetActive(false);
                    break;
                case 2:
                    minigun.SetActive(true);

                    pistol.SetActive(false);
                    smg.SetActive(false);
                    break;
            }
        
    }
}
