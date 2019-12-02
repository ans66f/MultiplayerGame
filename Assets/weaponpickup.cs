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

    // Start is called before the first frame update
    void Start()
    {
        PressETextObject = GameObject.FindGameObjectWithTag("PressETextObject");
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
            GameObject[] allplayers = GameObject.FindGameObjectsWithTag("Player");

            IsInTrigger = false;
            foreach (GameObject p in allplayers)
            {
                if (p.GetPhotonView().isMine)
                {
                    IsInTrigger = true;
                }

                Debug.Log("Photonview is mine: " + p.GetPhotonView().isMine);
            }

            if (IsInTrigger)
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
