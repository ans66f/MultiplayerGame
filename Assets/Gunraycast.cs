using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunraycast : Photon.MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        Debug.DrawRay(gameObject.transform.position, (gameObject.transform.forward * 100), Color.red, 1);

        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = new Ray(gameObject.transform.position, gameObject.transform.forward);
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit))
            {

            }


            if (hit.collider.gameObject.tag == "block")
            {
                Destroy(hit.collider.gameObject);

            }
            if (hit.collider.gameObject.tag == "Player")
            {
              //  hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(-hit.collider.gameObject.transform.forward * 50);


                Vector3 p = new Vector3((-hit.collider.gameObject.transform.forward * 50).x, (-hit.collider.gameObject.transform.forward * 50).y, (-hit.collider.gameObject.transform.forward * 50).z);


                if (photonView.isMine)
                {
                    photonView.RPC("AddForceToPlayer", PhotonTargets.OthersBuffered, p);
                }
               // hit.collider.gameObject.GetComponent<Player>().


            }
        }



        int i = 0;

        Vector3[] linepositions = new Vector3[2];
        linepositions[0] = gameObject.transform.position;
        linepositions[1] = (gameObject.transform.forward * 100) + gameObject.transform.position;

        GetComponent<LineRenderer>().SetPositions(linepositions);

    }
}
