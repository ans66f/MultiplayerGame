using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunraycast : Photon.MonoBehaviour
{

    public GameObject player;
    GameObject blockmanager;

    bool ToggleCreateMode = false;

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
        if(blockmanager != null)
        {

        }
        else
        {
            blockmanager = GameObject.FindGameObjectWithTag("GameManager");
        }


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

                if (hit.collider.gameObject.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<AIStalk>().DoModifyHealth(hit.collider.gameObject.GetComponent<AIStalk>().currHealth - 10);
                }


                    if (hit.collider.gameObject.tag == "Player")
                {
                    hit.collider.gameObject.GetComponent<Player>().DoModifyHealth(hit.collider.gameObject.GetComponent<Player>().currHealth - 10);
                }


                if (hit.collider.gameObject.tag == "block")
                {

                    Vector3 blockpos = hit.collider.gameObject.transform.position;
                    Debug.Log("HitBlock " + blockpos);



                    Vector3 pos = hit.collider.gameObject.transform.position;

                    if (ToggleCreateMode)
                    {
                        Vector3 disp = hit.collider.gameObject.GetComponent<Transform>().position - hit.point;

                        Debug.Log(disp.x);

                        if (disp.x > 0.4f) disp.x = 1;
                        else if (disp.x < -0.4f) disp.x = -1;
                        else disp.x = 0;


                        if (disp.y > 0.4f) disp.y = 1;
                        else if (disp.y < -0.4f) disp.y = -1;
                        else disp.y = 0;


                        if (disp.z > 0.4f) disp.z = 1;
                        else if (disp.z < -0.4f) disp.z = -1;
                        else disp.z = 0;

                        Vector3 newblockpos = hit.collider.gameObject.GetComponent<Transform>().position - disp;
                        blockmanager.GetComponent<GameManager>().CallCreateBlock(newblockpos);

                    }
                    else
                    {

                        blockmanager.GetComponent<GameManager>().CallDestroyBlock(hit.collider.gameObject.GetComponent<blockscript>().blockid);
                    }

                }
                if (hit.collider.gameObject.tag == "Player")
                {
                    //  hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(-hit.collider.gameObject.transform.forward * 50);
                    Debug.Log("Player Hit" + hit.collider.gameObject.name);

                    Vector3 p = new Vector3((-hit.collider.gameObject.transform.forward * 50).x, (-hit.collider.gameObject.transform.forward * 50).y, (-hit.collider.gameObject.transform.forward * 50).z);


                    //int ownerId = hit.collider.gameObject.GetComponent<PhotonView>().ownerId;
                    hit.collider.gameObject.GetComponent<Player>().DoForceThing();
                    hit.collider.gameObject.GetComponent<Player>().HitChangeColour(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));


                    // hit.collider.gameObject.GetComponent<Player>().


                }
            }
        }


        if(Input.GetKeyDown(KeyCode.F)) {
            ToggleCreateMode = !ToggleCreateMode;
        }



        int i = 0;

        Vector3[] linepositions = new Vector3[2];
        linepositions[0] = gameObject.transform.position;
        linepositions[1] = (gameObject.transform.forward * 100) + gameObject.transform.position;

        GetComponent<LineRenderer>().SetPositions(linepositions);

    }
}
