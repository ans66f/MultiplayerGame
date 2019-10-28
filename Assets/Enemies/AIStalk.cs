using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStalk : MonoBehaviour
{
    Transform destination;

    GameObject[] players;


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
                transform.LookAt(nearestplayer.transform);
                transform.Translate(transform.forward * 5 * Time.deltaTime);
            }
        }

    }
    
}
