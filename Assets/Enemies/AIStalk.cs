using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStalk : MonoBehaviour
{

    public Transform target;
    public Transform destination;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {

            Vector3 disp = target.transform.position - GetComponent<Transform>().position;

            if (disp.magnitude > 5)
            {

                transform.LookAt(target);
                transform.Translate(transform.forward * 5 * Time.deltaTime);
            }
        }
        else
        {
            if(GameObject.FindGameObjectWithTag("Player")) target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
