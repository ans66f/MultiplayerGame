using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Player : Photon.MonoBehaviour
{
    [Header("Player attributes")]
    public float speed = 10f;
    public float jumpstrength;
    bool jumpbool;


    [Header("Name and ID")]
    public int playerid = -1;
    public GameObject PlayerNameText;
    public GameObject PlayerNameTextWorld;
    public GameObject UniqueIDObject;
    

    [Header("Movement")]
    float Horizontalaxis;
    float Verticalaxis;
    float MouseX;
    float MouseY;


    [Header("Stats")]
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


    [Header("Health")]
    public int maxHealth = 200;
    public int currHealth;
    public Text currHealthLabel;
    public Text currHealthLabelWorldspace;

    public GameObject healthbar;
    public GameObject healthbarworldspace;
    float healthbarwidth;


    [Header("Death")]
    public bool isDead;
    bool spectating;
    bool gameIsDone;
    public int deathCountdown = 15;
    public int currCountdown;
    public Text currCountdownLabel;
    public GameObject DeathCanvas;


    [Header("Money")]
    public int startMoney = 50;
    public int currMoney;
    public Text currMoneyLabel;
    public Text currMoneyLabelWorldSpace;


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


    [Header("Diverse Objects")]
    public GameObject PlayerCam;
    public GameObject PlayerCamPosition;
    public GameObject PlayerStuff;
    public GameObject MinimapCam;

    public GameObject LocalCanvas;
    public GameObject DbControllerManager;
    GameObject[] spawnpoints;
    GameObject[] OtherPlayers;

    private List<Player> players = new List<Player>();
    private Player spectatedPlayer;


    private void Start()
    {
        isDead = false;
        currCountdown = deathCountdown;
        spawnpoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        currMoney = startMoney;
        jumpbool = false;
        currHealth = maxHealth;

        if (photonView.isMine)
        {
            LocalCanvas.SetActive(true);
        }
        else
        {
            GetComponent<Rigidbody>().useGravity = false;
            LocalCanvas.SetActive(false);
            MinimapCam.SetActive(false);
        }

        healthbarwidth = healthbar.GetComponent<RawImage>().rectTransform.rect.width;
        healthbarworldspace.GetComponent<RawImage>().rectTransform.sizeDelta = healthbar.GetComponent<RawImage>().rectTransform.sizeDelta;

        StartCoroutine(Timing()); // time played
        UpdateGUI();        

    }

    void Update()
    {
        float f = (float)maxHealth / (float)currHealth;
        healthbar.GetComponent<RawImage>().rectTransform.sizeDelta = new Vector2(healthbarwidth / f, healthbar.GetComponent<RawImage>().rectTransform.rect.height);
        healthbarworldspace.GetComponent<RawImage>().rectTransform.sizeDelta = healthbar.GetComponent<RawImage>().rectTransform.sizeDelta;
        // Debug.Log(maxHealth + " " + currHealth + " " + healthbarwidth + " " + f);


        if (UniqueIDObject.GetComponent<UniqueIDScript>().UniqueID != -1)
        {
            if (photonView.isMine)
            {
                PlayerNameText.GetComponent<Text>().text = DataHandler.username;
                PlayerNameTextWorld.GetComponent<Text>().text = DataHandler.username;

                photonView.RPC("GetPlayerUsername", PhotonTargets.OthersBuffered, DataHandler.username);
            }

        }

        if (currMoney <= 0)
        {
            currMoney = 0;
        }

        if (isDead && !spectating)
        {
            if (currCountdown <= 0)
            {
                if (photonView.isMine)
                {
                    EnterSpectate();
                    photonView.RPC("HidePlayer", PhotonTargets.All);
                    StartCoroutine(Spectate());                    
                }
            }
        }
        else if (!isDead)
        {
            if (photonView.isMine)
            {
                InputMovement();
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

        if(spectating && photonView.isMine)
        {            
            TrackSpectatedPlayer();
        }
    }


    public bool IsCurrentGunNotMaxStorageAmmo(int weapontype)
    {
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

    private void OnCollisionEnter(Collision collision)
    {
        jumpbool = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "ReviveHitbox")
        {
            Player otherPlayer = other.GetComponent<Player>();
            if (otherPlayer.isDead)
            {
                if (photonView.isMine)
                {
                    if (Input.GetKey(KeyCode.Q))
                    {
                        otherPlayer.DoModifyHealth(otherPlayer.maxHealth / 4);
                        otherPlayer.isDead = false;
                    }
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
            StartCoroutine(DecreaseCountdown());
        }
        else
        {
            StopCoroutine(DecreaseCountdown());
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
            }

            if (Verticalaxis < 0)
            {
                gameObject.GetComponent<Transform>().Translate((-Vector3.forward) * speed * Time.deltaTime);
            }

            if (Horizontalaxis > 0)
            {

                gameObject.GetComponent<Transform>().Translate(Vector3.right * speed * Time.deltaTime);
            }

            if (Horizontalaxis < 0)
            {
                gameObject.GetComponent<Transform>().Translate((-Vector3.right) * speed * Time.deltaTime);
            }

        }
    }

    public void Respawn()
    {
        if (photonView.isMine)
        {
                       
        if (isDead)
            {
                StopCoroutine(Spectate());
                ExitSpectate();
                //photonView.RPC("UnHidePlayer", PhotonTargets.All, this);
                currCountdownLabel.gameObject.SetActive(false);
                isDead = false;
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
                GetComponent<Transform>().position = new Vector3(0f, 5f, 0f);
            }
        }
    }

    
    IEnumerator Spectate()
    {
        int nbOfPlayersAlive = 3;
        //GameObject spectatedPlayer = null;
        while (nbOfPlayersAlive > 0)
        {
            Debug.Log(DataHandler.username + "Spectating");
            if (photonView.isMine)
            {
                Debug.Log(DataHandler.username + " : " + nbOfPlayersAlive);
                nbOfPlayersAlive = 0;
                //spectatedPlayer = null;
                OtherPlayers = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject player in OtherPlayers)
                {
                    if (!player.GetComponent<Player>().isDead)
                    {
                        nbOfPlayersAlive++;
                        //spectatedPlayer = player;
                    }
                }
                //if (!(spectatedPlayer == null))
                //{
                //    PlayerCam.tag = "Untagged";
                //    PlayerCam.SetActive(false);
                //    spectatedPlayer.GetComponent<Player>().PlayerCam.tag = "MainCamera";
                //    spectatedPlayer.GetComponent<Player>().PlayerCam.SetActive(true);
                //}
                yield return new WaitForSeconds(2);
            }
        }
        EndOfGame();
    }

    public void EnterSpectate()
    {
        if (photonView.isMine)
        {
            spectating = true;
            players.AddRange(GameObject.FindObjectsOfType<Player>()); // get every other player in the scene
            players.Remove(this); // remove my own spectate
            SetSpectatingPlayer(players[0]);
        }
    }

    public void SetSpectatingPlayer(Player player)
    {
        spectatedPlayer = player; // set to new spectating player
        TrackSpectatedPlayer();
    }

    public void TrackSpectatedPlayer()
    {
        //Vector3 posBefore = PlayerCam.transform.position;
        PlayerCam.transform.position = spectatedPlayer.PlayerCamPosition.transform.position;
        PlayerCam.transform.rotation = spectatedPlayer.PlayerCamPosition.transform.rotation;
        //Debug.Log("PosBefore: " + posBefore + "\nPosAfter: " + PlayerCam.transform.position);
    }

    public void ExitSpectate()
    {
        if (photonView.isMine)
        {
            spectating = false;
            SetSpectatingPlayer(this);
        }
    }

    void EndOfGame()
    {
        Debug.Log("End of game");
        if (photonView.isMine)
        {
            gameIsDone = true;
            totalScore += 8 * nbOfKills;
            for (int i = 0; i < nbOfWavesCompleted; i++)
            {
                if (i < 5)
                {
                    totalScore += 5;
                }
                else
                {
                    totalScore += 10;
                }
            }
            totalScore += currMoney;
            // When the game ends, you save stuff to the database
            DbControllerManager.GetComponent<dbController>().SaveScores(DataHandler.username, this.totalScore); // save game score (for highscores)
            DbControllerManager.GetComponent<dbController>().UpdateStats(DataHandler.username, 1, timePlayed, nbOfKills, totalScore, bulletsShot); // update user stats
        }
        DeathCanvas.SetActive(true); //display death screen
        currCountdownLabel.gameObject.SetActive(false);
        if (photonView.isMine)
        {
            ScoreText.GetComponent<Text>().text = totalScore.ToString(); //display player score in this game
        }
        

        StartCoroutine(GetHighScores()); // get high scores
        StartCoroutine(Finish());
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

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("MenuScene");
    }

    IEnumerator Timing()
    {
        while (!gameIsDone)
        {
            yield return new WaitForSeconds(1);
            timePlayed += 1;
        }
    }


    [PunRPC]
    public void GetPlayerUsername(string username)
    {
        PlayerNameText.GetComponent<Text>().text = username;
        PlayerNameTextWorld.GetComponent<Text>().text = username;
    }

    //[PunRPC]
    //public void HidePlayer(GameObject player)
    //{
    //    player.GetComponent<Renderer>().enabled = false;
    //}

    [PunRPC]
    public void HidePlayer()
    {
        gameObject.transform.position = new Vector3(0f, -10f, 0f);
        gameObject.GetComponent<Rigidbody>().position = new Vector3(0f, -10f, 0f);
        //player.GetComponent<Renderer>().enabled = true;
    }
}