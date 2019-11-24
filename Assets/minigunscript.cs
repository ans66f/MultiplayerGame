using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class minigunscript : MonoBehaviour
{
    public GameObject gunbarrel;
    public GameObject miniguncylinder;
    float maxcylinderspeed = 10.0f;
    float cylinderspeed = 0.0f;

    public GameObject Camera;
    bool isdoingspeedup = false;
    bool hasdonespeedupsound = false;

    public bool IsSpunUp;

    public AudioClip gatlingpart1;    

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().clip = gatlingpart1;
    }

    public void SpeedUpCylinder()
    {
        cylinderspeed = Mathf.Lerp(cylinderspeed, maxcylinderspeed, 0.01f);
    }


    // Update is called once per frame
    void Update()
    {
        if (cylinderspeed <= 0) cylinderspeed = 0;

        if(gunbarrel.GetComponent<Gunraycast>().isleftclick)
        {
            SpeedUpCylinder();
            isdoingspeedup = true;
        }
        else
        {
            isdoingspeedup = false;
            hasdonespeedupsound = false;

            cylinderspeed -= Time.deltaTime * 10;
        }

        if(isdoingspeedup && !hasdonespeedupsound)
        {
            //play speedup sound once
            GetComponent<AudioSource>().Play(0);

            hasdonespeedupsound = true;
        }
        else
        {
            //GetComponent<AudioSource>().Stop();
        }



        if(cylinderspeed > 8)
        {
            IsSpunUp = true;
        }
        else
        {
            IsSpunUp = false;
        }

        miniguncylinder.GetComponent<Transform>().Rotate(0,0, cylinderspeed);

        Debug.Log(cylinderspeed);
    }
}
