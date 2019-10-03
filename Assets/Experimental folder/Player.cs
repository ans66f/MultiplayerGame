using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Photon.MonoBehaviour {
    public float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            InputMovement();
            InputColorChange();
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
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position
                + Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position
                - Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position
                + Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position
                - Vector3.right * speed * Time.deltaTime);
        }
    }
}
