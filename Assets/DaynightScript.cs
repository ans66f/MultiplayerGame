using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaynightScript : MonoBehaviour
{

    public GameObject[] AllLampLights;

    public float sunspeed = 0.1f;

    public bool AreStreetLightsOn = false;

    // Start is called before the first frame update
    void Start()
    {
        AllLampLights = GameObject.FindGameObjectsWithTag("LampLight");
    }

    void SetLampLights()
    {
        foreach (GameObject lamplight in AllLampLights)
        {
            lamplight.SetActive(AreStreetLightsOn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().Rotate(Vector3.right, sunspeed);



        if (GetComponent<Transform>().eulerAngles.x < 200 && GetComponent<Transform>().eulerAngles.x > 0)
        {

            AreStreetLightsOn = false;
            SetLampLights();
        }
        else
        {

            AreStreetLightsOn = true;
            SetLampLights();
        }


    }
}
