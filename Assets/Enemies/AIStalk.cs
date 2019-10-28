using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStalk : MonoBehaviour
{
    Transform destination;

    GameObject[] players;
    public float EnemySpeed;


    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearestplayer = null;
        float closest = 1000000.0f;

        foreach (GameObject player in players)
        {
            Vector3 disp = player.transform.position - GetComponent<Transform>().position;
            if (disp.magnitude < closest)
            {
                closest = disp.magnitude;
                nearestplayer = player;
            }
        }



        if (nearestplayer != null)
        {
            Vector3 disptonearest = nearestplayer.transform.position - GetComponent<Transform>().position;
            if (disptonearest.magnitude > 5)
            {
                

                float speed = EnemySpeed * Time.deltaTime;

                transform.LookAt(nearestplayer.transform);
                Vector3 v = new Vector3(transform.forward.x * speed, 0, transform.forward.z * speed);

                GetComponent<Rigidbody>().velocity = v;
                //transform.Translate(transform.forward * 5 * Time.deltaTime);
            }
            else
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
        }

    }
    
}
