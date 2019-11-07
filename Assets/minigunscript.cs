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

    public bool IsSpunUp;

    // Start is called before the first frame update
    void Start()
    {

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
        }
        else
        {
            cylinderspeed -= Time.deltaTime * 10;
        }

        if(cylinderspeed > 8)
        {
            IsSpunUp = true;
        }
        else
        {
            IsSpunUp = false;
        }

        miniguncylinder.GetComponent<Transform>().Rotate(miniguncylinder.GetComponent<Transform>().forward, cylinderspeed);

        Debug.Log(cylinderspeed);
    }
}
