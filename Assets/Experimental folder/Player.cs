using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.MonoBehaviour {
    public float speed = 10f;

    public GameObject PlayerCam;
    public GameObject PlayerStuff;
    public GameObject pistol;

    bool jumpbool;


    float Horizontalaxis;
    float Verticalaxis;
    float MouseX;
    float MouseY;

    private void Start()
    {
        jumpbool = false;
    }

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

    private void OnCollisionEnter(Collision collision)
    {
        jumpbool = false;
    }

    private void InputMovement()
    {

        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");

        Horizontalaxis = Input.GetAxisRaw("Horizontal");
        Verticalaxis = Input.GetAxisRaw("Vertical");


        PlayerStuff.GetComponent<CamMoveScript>().RotateCam();
        pistol.GetComponent<Lerptoaimposition>().LerpUpdate();


        gameObject.transform.Rotate(new Vector3(0, MouseX * speed, 0));





        if(Input.GetKeyDown(KeyCode.Space) && !jumpbool)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1000, 0));
            jumpbool = true;
        }



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
