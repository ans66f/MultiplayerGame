using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Player : Photon.MonoBehaviour
{
    public float speed = 10f;

    public int playerid = -1;

    private bool keypress = true;
    private float timePressed = 0f;

    public int timePlayed = 0;
    public int nbOfKills = 0;
    public int totalScore = 0;
    public int bulletsShot = 0;

    public int nbOfWavesCompleted = 0;

    [Header("Weapons")]
    public GameObject WeaponsObject;


    public GameObject pistol;
    public GameObject smg;
    public GameObject minigun;

    [Header("Objects")]
    public GameObject PlayerCam;
    public GameObject PlayerStuff;


    public float jumpstrength;

    bool jumpbool;
    public GameObject LocalCanvas;

    [Header("Health")]
    public int maxHealth = 200;
    public int currHealth;
    public Text currHealthLabel;
    public Text currHealthLabelWorldspace;

    public GameObject healthbar;
    public GameObject healthbarworldspace;
    float healthbarwidth;

    [Header("Death")]
    public int deathCountdown = 15;
    public int currCountdown;
    public Text currCountdownLabel;
    IEnumerator deathCo;

    public GameObject DeathCanvas;

    public GameObject UniqueIDObject;
    public GameObject PlayerNameText;
    public GameObject PlayerNameTextWorld;

    public GameObject DbControllerManager;

    [Header("Money")]
    public int startMoney = 50;
    public int currMoney;
    public Text currMoneyLabel;

    public Text currMoneyLabelWorldSpace;
    float moneybarwidth;

    GameObject[] spawnpoints;


    public bool isDead;
    bool donedeadcheck;
    bool gameIsDone;

    float Horizontalaxis;
    float Verticalaxis;
    float MouseX;
    float MouseY;

    [Header("HighScores")]
    public GameObject Username1Text;
    public GameObject Username2Text;
    public GameObject Username3Text;
    public GameObject Username4Text;
    public GameObject Username5Text;

    public GameObject Score1Text;
    public GameObject Score2Text;
    public GameObject Score3Text;
    public GameObject Score4Text;
    public GameObject Score5Text;

    public GameObject ScoreText;

    public bool IsCurrentGunNotMaxStorageAmmo(int weapontype)
    {
       // int currweapon = WeaponsObject.GetComponent<currentweaponscript>().currentgun;


            if (weapontype == 0)
            {
                if (WeaponsObject.GetComponent<currentweaponscript>().pistolbarrel.GetComponent<Gunraycast>().CurrentAmmoStorage < WeaponsObject.GetComponent<currentweaponscript>().pistolbarrel.GetComponent<Gunraycast>().MaxAmmoStorage)
                {
                    return true;
                }

            }
            if (weapontype == 1)
            {
                if (WeaponsObject.GetComponent<currentweaponscript>().smgbarrel.GetComponent<Gunraycast>().CurrentAmmoStorage < WeaponsObject.GetComponent<currentweaponscript>().smgbarrel.GetComponent<Gunraycast>().MaxAmmoStorage)
                {
                    return true;
                }
            }
            if (weapontype == 2)
            {
                if (WeaponsObject.GetComponent<currentweaponscript>().minigunbarrel.GetComponent<Gunraycast>().CurrentAmmoStorage < WeaponsObject.GetComponent<currentweaponscript>().minigunbarrel.GetComponent<Gunraycast>().MaxAmmoStorage)
                {
                    return true;
                }
            }
        
        return false;

    }

    private void Start()
    {
        isDead = false;
        donedeadcheck = false;
        currCountdown = deathCountdown;
        spawnpoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        currMoney = startMoney;

        if (photonView.isMine)
        {
            LocalCanvas.SetActive(true);
        }
        else
        {
            GetComponent<Rigidbody>().useGravity = false;
            LocalCanvas.SetActive(false);
        }

        //healthbar = GameObject.FindGameObjectWithTag("HealthBar");
        healthbarwidth = healthbar.GetComponent<RawImage>().rectTransform.rect.width;
        healthbarworldspace.GetComponent<RawImage>().rectTransform.sizeDelta = healthbar.GetComponent<RawImage>().rectTransform.sizeDelta;


        jumpbool = false;
        currHealth = maxHealth;
        if (photonView.isMine) {
            //currHealthLabel = GameObject.FindGameObjectWithTag("healthLabel").GetComponent<Text>();
        }

        StartCoroutine(Timing());

        UpdateGUI();
    }

    IEnumerator Timing()
    {
        while(!gameIsDone)
        {
            yield return new WaitForSeconds(1);
            timePlayed += 1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        jumpbool = false;

    }


    void Respawn()
    {
        currCountdownLabel.gameObject.SetActive(false);
        isDead = false;
        donedeadcheck = false;
        currMoney = 0;
        currHealth = maxHealth;
        UpdateGUI();

        GameObject randspawnpoint = spawnpoints[0];
        foreach (GameObject spawnpoint in spawnpoints)
        {
            if (UnityEngine.Random.Range(0, spawnpoints.Length) == spawnpoints.Length)
            {
                randspawnpoint = spawnpoint;
            }
        }
        GetComponent<Transform>().position = randspawnpoint.transform.position;
    }

// Update is called once per frame



    [PunRPC]
    public void GetPlayerUsername(string username)
    {
        PlayerNameText.GetComponent<Text>().text = username;
        PlayerNameTextWorld.GetComponent<Text>().text = username;
    }


    void Update()
    {
        float f = (float)maxHealth / (float)currHealth;
        healthbar.GetComponent<RawImage>().rectTransform.sizeDelta = new Vector2(healthbarwidth / f, healthbar.GetComponent<RawImage>().rectTransform.rect.height);
        healthbarworldspace.GetComponent<RawImage>().rectTransform.sizeDelta = healthbar.GetComponent<RawImage>().rectTransform.sizeDelta;
        // Debug.Log(maxHealth + " " + currHealth + " " + healthbarwidth + " " + f);

        if (gameIsDone)
        {

        }



        if (UniqueIDObject.GetComponent<UniqueIDScript>().UniqueID != -1)
        {
            //PlayerNameText.GetComponent<Text>().text = "Player: " + UniqueIDObject.GetComponent<UniqueIDScript>().UniqueID;
            //PlayerNameTextWorld.GetComponent<Text>().text = "Player: " + UniqueIDObject.GetComponent<UniqueIDScript>().UniqueID;


            if (photonView.isMine)
            {
                PlayerNameText.GetComponent<Text>().text = DataHandler.username;
                PlayerNameTextWorld.GetComponent<Text>().text = DataHandler.username;

                photonView.RPC("GetPlayerUsername", PhotonTargets.OthersBuffered, DataHandler.username);
            }

        }

        if(currMoney <= 0)
        {
            currMoney = 0;
        }


        if (isDead && !gameIsDone)
        {
            if (currCountdown <= 0)
            {
                gameIsDone = true;
                EndOfGame();
               // gameObject.SetActive(false);
            }
        }
        else if (!isDead)
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

            if (photonView.isMine)
            {
                if (Input.GetKeyDown(KeyCode.Space) && !jumpbool)
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpstrength, 0));
                    jumpbool = true;
                }
            }
        }
    }

    
    void UpdateGUI()
    {
        if (currHealthLabel != null)
            currHealthLabel.text = currHealth.ToString();

        if (currHealthLabelWorldspace != null)
            currHealthLabelWorldspace.text = currHealth.ToString();

        if (currMoneyLabel != null)
            currMoneyLabel.text = "$" + currMoney.ToString();

        if (currMoneyLabelWorldSpace != null)
            currMoneyLabelWorldSpace.text = "$" + currMoney.ToString();

    }



    public void DoModifyHealth(int amount)
    {
        photonView.RPC("ModifyHealth", PhotonTargets.AllBuffered, amount);
    }

    [PunRPC]
    public void ModifyHealth(int amount)
    {
        if (!isDead)
        {
            currHealth = Mathf.Clamp(amount, 0, maxHealth);
            CheckIfDead();
            UpdateGUI();
        }
    }


    public void DoModifyMoney(int amount)
    {
        photonView.RPC("ModifyMoney", PhotonTargets.AllBuffered, amount);
    }

    [PunRPC]
    public void ModifyMoney(int amount)
    {
        currMoney = amount;
        UpdateGUI();

    }

    private void CheckIfDead()
    {
        if (currHealth == 0 && !gameIsDone)
        {
            isDead = true;
            currCountdown = deathCountdown;
            currCountdownLabel.gameObject.SetActive(true);
            //deathCo = DecreaseCountdown();
            StartCoroutine(DecreaseCountdown());
        }
        else
        {
            StopCoroutine(DecreaseCountdown());
           // deathCo = null;
            currCountdownLabel.gameObject.SetActive(false);
        }
    }

    private IEnumerator DecreaseCountdown()
    {
        while (isDead && currCountdown > 0)
        {
            currCountdown -= 1;
            if (currCountdownLabel != null)
                currCountdownLabel.text = currCountdown.ToString();
            yield return new WaitForSeconds(1);
        }
    }


    private void InputColorChange()

    {
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.R))

            {
                ChangeColorTo(new Vector3(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f),
                UnityEngine.Random.Range(0f, 1f)));
            }
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

    }


    /*
    [PunRPC]
    void ChangeColorTo(Vector3 color)

    {
        GetComponent<Renderer>().material.color = new Color(color.x, color.y, color.z, 1f);
        if (photonView.isMine)
        {
            photonView.RPC("ChangeColorTo", PhotonTargets.All, color);
        }

    }
    */



    private void InputMovement()

    {
        if (!isDead)
        {
            MouseX = Input.GetAxis("Mouse X");
            MouseY = Input.GetAxis("Mouse Y");
            Horizontalaxis = Input.GetAxisRaw("Horizontal");
            Verticalaxis = Input.GetAxisRaw("Vertical");
            PlayerStuff.GetComponent<CamMoveScript>().RotateCam();


            pistol.GetComponent<Lerptoaimposition>().LerpUpdate();
            smg.GetComponent<Lerptoaimposition>().LerpUpdate();
            minigun.GetComponent<Lerptoaimposition>().LerpUpdate();


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

            if (Input.GetKeyDown(KeyCode.Z))
            {
                EndOfGame();
            }
        }
    }

    void EndOfGame()
    {
        totalScore += 8 * nbOfKills;
        for (int i = 0; i < nbOfWavesCompleted; i++)
        {
            if (i < 5)
            {
                totalScore += 5;
            } else
            {
                totalScore += 10;
            }
        }
        totalScore += currMoney;
        // When the game ends, you save stuff to the database
        DbControllerManager.GetComponent<dbController>().SaveScores(DataHandler.username, this.totalScore); // save game score (for highscores)
        DbControllerManager.GetComponent<dbController>().UpdateStats(DataHandler.username, 1, timePlayed, nbOfKills, totalScore, bulletsShot); // update user stats

        // display highscores and wait for all players to die
        DeathCanvas.SetActive(true);
        healthbarworldspace.SetActive(false);
        currHealthLabel.enabled = false;
        ScoreText.GetComponent<Text>().text = totalScore.ToString();
        
        StartCoroutine(GetHighScores());
    }

    IEnumerator GetHighScores()
    {
        DbControllerManager.GetComponent<dbController>().LoadScores();
        yield return new WaitForSeconds(1);
        String[] items = DbControllerManager.GetComponent<dbController>().items;

        Username1Text.GetComponent<Text>().text = DbControllerManager.GetComponent<dbController>().GetDataValue(items[0], "username:");
        Username2Text.GetComponent<Text>().text = DbControllerManager.GetComponent<dbController>().GetDataValue(items[1], "username:");
        Username3Text.GetComponent<Text>().text = DbControllerManager.GetComponent<dbController>().GetDataValue(items[2], "username:");
        Username4Text.GetComponent<Text>().text = DbControllerManager.GetComponent<dbController>().GetDataValue(items[3], "username:");
        Username5Text.GetComponent<Text>().text = DbControllerManager.GetComponent<dbController>().GetDataValue(items[4], "username:");

        Score1Text.GetComponent<Text>().text = DbControllerManager.GetComponent<dbController>().GetDataValue(items[0], "score:");
        Score2Text.GetComponent<Text>().text = DbControllerManager.GetComponent<dbController>().GetDataValue(items[1], "score:");
        Score3Text.GetComponent<Text>().text = DbControllerManager.GetComponent<dbController>().GetDataValue(items[2], "score:");
        Score4Text.GetComponent<Text>().text = DbControllerManager.GetComponent<dbController>().GetDataValue(items[3], "score:");
        Score5Text.GetComponent<Text>().text = DbControllerManager.GetComponent<dbController>().GetDataValue(items[4], "score:");
    }


    /*
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Player player = collision.collider.GetComponent<Player>();
            if (player.isDead)
            {
                if (Input.GetKey(KeyCode.Q))
                {
                    player.DoModifyHealth(maxHealth / 4);
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
    */
}