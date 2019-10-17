using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Player : Photon.MonoBehaviour
{
    public float speed = 10f;

    [Header("Objects")]
    public GameObject PlayerCam;
    public GameObject PlayerStuff;
    public GameObject pistol;

    bool jumpbool;

    [Header("Health")]
    public int maxHealth = 100;
    public int currHealth;
    public Text currHealthLabel;

    GameObject healthbar;
    float healthbarwidth;



    public bool isDead
    {
        get { return currHealth == 0; }
    }

    float Horizontalaxis;
    float Verticalaxis;
    float MouseX;
    float MouseY;

    private void Start()
    {
        healthbar = GameObject.FindGameObjectWithTag("HealthBar");
        healthbarwidth = healthbar.GetComponent<RawImage>().rectTransform.rect.width;





        jumpbool = false;
        currHealth = maxHealth/2;
        if (photonView.isMine) {
            currHealthLabel = GameObject.FindGameObjectWithTag("healthLabel").GetComponent<Text>();
        }
        UpdateGUI();
    }

    private void OnCollisionEnter(Collision collision)
    {
        jumpbool = false;
    }



// Update is called once per frame

void Update()

    {
        float f = (float)maxHealth / (float)currHealth;
        healthbar.GetComponent<RawImage>().rectTransform.sizeDelta = new Vector2(healthbarwidth / f, healthbar.GetComponent<RawImage>().rectTransform.rect.height);
       // Debug.Log(maxHealth + " " + currHealth + " " + healthbarwidth + " " + f);


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

        if (Input.GetKeyDown(KeyCode.Space) && !jumpbool)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1000, 0));
            jumpbool = true;
        }

    }

    void UpdateGUI()
    {
        if (currHealthLabel != null)
            currHealthLabel.text = currHealth.ToString();
    }

    public void modifyHealth(int amount)
    {
        currHealth += amount;
        currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
        checkIfDead();
        UpdateGUI();
    }

    private void checkIfDead()
    {
        if (isDead)
        {
            // what happens when the player is dead ? TODO
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

    public void DoForceThing()
    {
        photonView.RPC("AddForceToPlayer", PhotonTargets.All);
    }




    [PunRPC]
    void AddForceToPlayer()
    {
        //Debug.Log("Viewid given" + ownerId + " ViewID needed " + GetComponent<PhotonView>().ownerId);
        //if (ownerId == GetComponent<PhotonView>().ownerId)
        {
            Debug.Log("given force");
            gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.up * 100);
        }
    }




    public void HitChangeColour(Vector3 color)
    {
        photonView.RPC("ChangeColorTo", PhotonTargets.All, color);
    }

    [PunRPC]
    void ChangeColorTo(Vector3 color)

    {
        GetComponent<Renderer>().material.color = new Color(color.x, color.y, color.z, 1f);
        if (photonView.isMine)
        {
            photonView.RPC("ChangeColorTo", PhotonTargets.All, color);
        }

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