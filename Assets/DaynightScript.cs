using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaynightScript : Photon.MonoBehaviour
{

    public GameObject[] AllLampLights;

    float sunspeed = 10f;

    public bool AreStreetLightsOn = false;
    GameObject ThisPlayer;


    float sunupdatetimer = 1.0f;

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


    [PunRPC]
    void UpdateSunPos(Quaternion q)
    {
        GetComponent<Transform>().rotation = q;
    }
    void DoUpdateSunPos()
    {
        photonView.RPC("UpdateSunPos", PhotonTargets.OthersBuffered, GetComponent<Transform>().rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (ThisPlayer == null)
        {
            GameObject[] allplayers = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject p in allplayers)
            {
                if (p.GetPhotonView().isMine)
                {
                    ThisPlayer = p;
                }

            }
        }
        if(PhotonNetwork.isMasterClient)
        {
            sunupdatetimer -= Time.deltaTime;
            if (sunupdatetimer <= 0)
            {
                sunupdatetimer = 1;
                DoUpdateSunPos();
            }
        }




        GetComponent<Transform>().Rotate(Vector3.right, sunspeed * Time.deltaTime);



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
