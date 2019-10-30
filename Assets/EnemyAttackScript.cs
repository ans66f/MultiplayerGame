﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public float attacktimer = 1.0f;
    float currattacktimer = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            currattacktimer -= Time.deltaTime;

            if(currattacktimer <= 0)
            {
                currattacktimer = attacktimer;

                other.gameObject.GetComponent<Player>().DoModifyHealth(other.gameObject.GetComponent<Player>().currHealth - 10);
            }
        }
    }
}
