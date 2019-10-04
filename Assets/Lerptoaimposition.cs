using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerptoaimposition : MonoBehaviour
{
    public GameObject aimpositionobject;
    public GameObject holdpositionobject;


    float lerpamount = 0;
    float lerptime = 0.5f;
    float l = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LerpUpdate()
    {
        l = lerpamount / lerptime;


        if (lerpamount >= lerptime)
        {
            lerpamount = lerptime;
        }
        if (lerpamount <= 0)
        {
            lerpamount = 0;
        }

        if (Input.GetMouseButton(1))
        {
            lerpamount += Time.deltaTime;

        }
        else
        {
            lerpamount -= Time.deltaTime;
        }
        gameObject.transform.position = Vector3.Lerp(holdpositionobject.transform.position, aimpositionobject.transform.position, l);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
