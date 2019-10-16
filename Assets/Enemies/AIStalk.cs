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
        transform.LookAt(target);
        transform.Translate(Vector3.forward * 5 * Time.deltaTime);
    }
}
