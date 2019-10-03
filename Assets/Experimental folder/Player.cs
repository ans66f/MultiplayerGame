﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.MonoBehaviour {
    public float speed = 10f;

    public GameObject PlayerCam;


    float Horizontalaxis;
    float Verticalaxis;
    float MouseX;
    float MouseY;

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            InputMovement();
            InputColorChange();

            PlayerCam.tag = "MainCamera";
            PlayerCam.SetActive(true);
        }
        else
        {
            PlayerCam.tag = "Untagged";
            PlayerCam.SetActive(false);
        }
    }

    private void InputColorChange()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeColorTo(new Vector3(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f),
                UnityEngine.Random.Range(0f, 1f)));
        }
    }

    [PunRPC] void ChangeColorTo(Vector3 color)
    {
        GetComponent<Renderer>().material.color = new Color(color.x, color.y, color.z, 1f);

        if (photonView.isMine)
        {
            photonView.RPC("ChangeColorTo", PhotonTargets.OthersBuffered, color);
        }
    }

    private void InputMovement()
    {

        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");

        Horizontalaxis = Input.GetAxisRaw("Horizontal");
        Verticalaxis = Input.GetAxisRaw("Vertical");

        Debug.Log(MouseX + " " + MouseY);


        gameObject.transform.Rotate(new Vector3(0, MouseX * speed, 0));


        if (Verticalaxis > 0)
        {

            gameObject.GetComponent<Transform>().Translate(Vector3.forward * speed * Time.deltaTime);


            //GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + Vector3.forward * speed * Time.deltaTime);
        }
        if (Verticalaxis < 0)
        {

            gameObject.GetComponent<Transform>().Translate((-Vector3.forward) * speed * Time.deltaTime);

            //GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - Vector3.forward * speed * Time.deltaTime);
        }
        if (Horizontalaxis > 0)
        {
            gameObject.GetComponent<Transform>().Translate(Vector3.right * speed * Time.deltaTime);

            //GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position+ Vector3.right * speed * Time.deltaTime);
        }
        if (Horizontalaxis < 0)
        {
            gameObject.GetComponent<Transform>().Translate((-Vector3.right) * speed * Time.deltaTime);

            //GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position- Vector3.right * speed * Time.deltaTime);
        }
    }
}