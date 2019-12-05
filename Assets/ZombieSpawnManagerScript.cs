using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ZombieSpawnManagerScript : MonoBehaviour
{

    public List<GameObject> Gates;
    GameObject[] EnemySpawners;


    GameObject ThisPlayer;

    public GameObject ZombieTextObject;

    /* // Optimisation ?? maybe
    public int CurrentZombie = 0;
    public int MaxCurrentZombie = 10;
    */

    int InitialZombieNum = 5;

    int CurrentZombieStorage = 5;
    int Wavenum = 1;


    // Start is called before the first frame update
    void Start()
    {
        EnemySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
    



    }

    public bool TrySpawnZombie()
    {

        if((CurrentZombieStorage - 1) >= 0)
        {
            CurrentZombieStorage -= 1;

            return true;
        }
        else
        {
            return false;
        }
    } 

    // Update is called once per frame
    void Update()
    {
        if (ThisPlayer == null)
        {
            GameObject[] allplayers = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject p in allplayers)
            {
                if (p.GetPhotonView().isMine)
                {
                    ThisPlayer = p;
                }

            }
        }



        int i = 0;
        int j = 0;

        for (i = 0; i < Gates.Count; i++)
        {
            for (j = 0; j < EnemySpawners.Length; j++)
            {
                if (Gates[i].GetComponent<GateScript>().GateNum == EnemySpawners[j].GetComponent<enemyspawnscript>().GateNum)
                {
                    if (Gates[i].GetComponent<GateScript>().IsGateOpen)
                    {
                        EnemySpawners[j].GetComponent<enemyspawnscript>().IsSpawnerActive = true;
                    }
                }
            }
        }


        GameObject[] AllZombies = GameObject.FindGameObjectsWithTag("Enemy");
        ZombieTextObject.GetComponent<Text>().text = "Zombies Left: " + (CurrentZombieStorage + AllZombies.Length) + " | Wave " + Wavenum;



        
        




        if(CurrentZombieStorage <= 0 && AllZombies.Length <= 0)
        {
            Wavenum += 1;

            InitialZombieNum += 5;
            CurrentZombieStorage = InitialZombieNum;

            if(!ThisPlayer.GetComponent<Player>().isDead)
            {
                ThisPlayer.GetComponent<Player>().DoModifyMoney((10 + InitialZombieNum));
            }
            else
            {
                ThisPlayer.GetComponent<Player>().isDead = false;
            }
        }
    }
}
