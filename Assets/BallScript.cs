using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        while (GetComponent<Rigidbody>().velocity.magnitude < 4)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "topwall" || other.gameObject.tag == "bottomwall")
        {

            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, -GetComponent<Rigidbody>().velocity.y, 0);
        }
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "leftwall" || other.gameObject.tag == "rightwall")
        {
            GetComponent<Rigidbody>().velocity = new Vector3(-GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 0);
        }
    }
}
