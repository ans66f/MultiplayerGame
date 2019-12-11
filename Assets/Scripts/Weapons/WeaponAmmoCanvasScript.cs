using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmoCanvasScript : Photon.MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(photonView.isMine)
        {

        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
