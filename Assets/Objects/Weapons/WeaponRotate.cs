using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponRotate : Photon.MonoBehaviour
{
    float speed = 50.0f;
    public GameObject Weapon;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);



        
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

}