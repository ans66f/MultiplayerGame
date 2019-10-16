using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackGenerator : Photon.MonoBehaviour
{
    [SerializeField] private GameObject healthPack;
    [SerializeField] private int healthPackHealing = 50;
    [SerializeField] private int regenTime = 10;
    // Start is called before the first frame update
    void Start()
    {
        healthPack.GetComponent<HealthPack>().healing = healthPackHealing;
    }

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
        healthPack.SetActive(true);
    }
}
