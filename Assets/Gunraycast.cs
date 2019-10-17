using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunraycast : Photon.MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void AddForceToPlayer()
    {

    }

    // Update is called once per frame
    void Update()
    {


        Debug.DrawRay(gameObject.transform.position, (gameObject.transform.forward * 100), Color.red, 1);


        if (photonView.isMine)
        {
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
                    Debug.Log("Player Hit" + hit.collider.gameObject.name);

                    Vector3 p = new Vector3((-hit.collider.gameObject.transform.forward * 50).x, (-hit.collider.gameObject.transform.forward * 50).y, (-hit.collider.gameObject.transform.forward * 50).z);


                    int ownerId = hit.collider.gameObject.GetComponent<PhotonView>().ownerId;
                    player.GetComponent<Player>().DoForceThing(ownerId);

                    

                    // hit.collider.gameObject.GetComponent<Player>().


                }
            }
        }



        int i = 0;

        Vector3[] linepositions = new Vector3[2];
        linepositions[0] = gameObject.transform.position;
        linepositions[1] = (gameObject.transform.forward * 100) + gameObject.transform.position;

        GetComponent<LineRenderer>().SetPositions(linepositions);

    }
}
