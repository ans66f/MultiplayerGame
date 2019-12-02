using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponpickup : Photon.MonoBehaviour
{
    public int weapontype;
    GameObject PressETextObject;


    bool IsInTrigger = false;

    public int WeaponCost;
    public GameObject CostTextObject;


    GameObject ThisPlayer;

    // Start is called before the first frame update
    void Start()
    {
        PressETextObject = GameObject.FindGameObjectWithTag("PressETextObject");


        GameObject[] allplayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in allplayers)
        {
            if (p.GetPhotonView().isMine)
            {
                ThisPlayer = p;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {



        CostTextObject.GetComponent<Text>().text = "Cost: " + WeaponCost;
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {


            if (other.gameObject == ThisPlayer)
            {


                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (other.gameObject.GetComponent<Player>().currMoney >= WeaponCost)
                    {
                        if (other.gameObject.GetComponent<Player>().IsCurrentGunNotMaxStorageAmmo(weapontype))
                        {


                            other.gameObject.GetComponent<Player>().WeaponsObject.GetComponent<currentweaponscript>().AddAmmoAndWeapon(weapontype);
                            other.gameObject.GetComponent<Player>().DoModifyMoney(other.gameObject.GetComponent<Player>().currMoney - WeaponCost);
                        }
                    }
                    else
                    {
                        Debug.Log("Not enough money skrub, need: " + (WeaponCost - other.gameObject.GetComponent<Player>().currMoney) + " more");
                    }




                }


                PressETextObject.GetComponent<pressetextscript>().SetPressETextActive(true);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        PressETextObject.GetComponent<pressetextscript>().SetPressETextActive(false);
    }
}
