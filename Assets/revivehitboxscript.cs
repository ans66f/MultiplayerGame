﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revivehitboxscript : Photon.MonoBehaviour
{

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
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player.isDead)
            {
                if (photonView.isMine)
                {
                    if (Input.GetKey(KeyCode.Q))
                    {
                        player.DoModifyHealth(player.maxHealth / 4);
                        player.isDead = false;

                        //if (keypress)
                        //{
                        //    keypress = false;
                        //    timePressed = Time.time;
                        //}
                        //else
                        //{
                        //    if (Time.time - timePressed > 3.0f)
                        //    {
                        //        keypress = true;
                        //        player.ModifyHealth(maxHealth / 4);
                        //    }
                        //}
                    }
                }
            }
        }
    }
}