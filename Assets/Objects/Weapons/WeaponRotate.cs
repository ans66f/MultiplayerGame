using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponRotate : Photon.MonoBehaviour
{
    float speed = 50.0f;
    public GameObject Weapon;
    public int WeaponCost;


    public GameObject CostTextObject;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);



        CostTextObject.GetComponent<Text>().text = "Cost: " + WeaponCost;
    }


    void DoDestroyWeaponAndThis()
    {
        photonView.RPC("DestroyWeaponAndThis", PhotonTargets.AllBuffered);
    }

    [PunRPC]
    void DestroyWeaponAndThis()
    {
        Weapon.SetActive(false);
        GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y - 1000, GetComponent<Transform>().position.z);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Weapon.GetActive())
        {
            if (other.tag == "Player")
            {

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (other.gameObject.GetComponent<Player>().currMoney >= WeaponCost)
                    {
                        other.gameObject.GetComponent<Player>().DoModifyMoney(other.gameObject.GetComponent<Player>().currMoney - WeaponCost);

                       // DoDestroyWeaponAndThis();

                    }
                    else
                    {
                        Debug.Log("Not enough money skrub, need: " + (WeaponCost - other.gameObject.GetComponent<Player>().currMoney) + " more");
                    }
                }
            }
        }
    }

}