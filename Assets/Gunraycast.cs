using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunraycast : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        Debug.DrawRay(gameObject.transform.position, (gameObject.transform.forward * 100), Color.red, 1);



        int i = 0;
        Vector3[] linepositions;
        linepositions = new Vector3[10];
        for(i = 0; i < 10; i++)
        {
            linepositions[i] = Vector3.Lerp(gameObject.transform.position, (gameObject.transform.forward * 100), i);
            
        }
        GetComponent<LineRenderer>().SetPositions(linepositions);

    }
}
