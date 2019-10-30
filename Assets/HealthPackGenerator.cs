using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackGenerator : Photon.MonoBehaviour
{
    [SerializeField] private GameObject healthPack;
    [SerializeField] private int regenTime = 5;
    

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void ReloadPack()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        yield return new WaitForSeconds(regenTime);
        DoActivateHealthPack();
    }

    [PunRPC]
    void activateHealthPack()
    {
        healthPack.SetActive(true);
    }

    void DoActivateHealthPack()
    {
        photonView.RPC("activateHealthPack", PhotonTargets.AllBuffered);
    }
}
