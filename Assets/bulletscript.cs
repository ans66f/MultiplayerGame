﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : Photon.MonoBehaviour
{

    public GameObject player;
    int damage = 0;

    float DespawnTimer = 100.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetValues(int d, GameObject p)
    {
        damage = d;
        player = p;
    }

    // Update is called once per frame
    void Update()
    {

        gameObject.GetComponent<Transform>().LookAt(GetComponent<Rigidbody>().velocity.normalized);


        DespawnTimer -= Time.deltaTime;
        if(DespawnTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().DoModifyHealth(collision.gameObject.GetComponent<Player>().currHealth - damage);
            player.GetComponent<Player>().DoModifyMoney(player.GetComponent<Player>().currMoney - 1);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("HitEnemy");
            if (collision.gameObject.GetComponent<AIStalk>().currHealth - damage <= 0)
            {
                player.GetComponent<Player>().DoModifyMoney(player.GetComponent<Player>().currMoney + collision.gameObject.GetComponent<AIStalk>().MoneyWorth);
            }
            collision.gameObject.GetComponent<AIStalk>().DoModifyHealth(collision.gameObject.GetComponent<AIStalk>().currHealth - damage);
        }
        Destroy(gameObject);
    }

}
