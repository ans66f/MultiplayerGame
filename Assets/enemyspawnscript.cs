using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawnscript : MonoBehaviour
{
    public GameObject EnemyTemplate;
    public int GateNum;

    public bool IsSpawnerActive;

    GameObject ZombieSpawnManager;

    public Material Red;
    public Material Green;

    float spawntimer;

    // Start is called before the first frame update
    void Start()
    {
        ZombieSpawnManager = GameObject.FindGameObjectWithTag("ZombieSpawnManager");

        spawntimer = 2.0f; 
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSpawnerActive)
        {
            GetComponent<Renderer>().material = Green;

            spawntimer -= Time.deltaTime;

            if (spawntimer <= 0)
            {
                spawntimer = Random.Range(5.0f, 20.0f);

                if (ZombieSpawnManager.GetComponent<ZombieSpawnManagerScript>().TrySpawnZombie())
                {
                    PhotonNetwork.Instantiate(EnemyTemplate.name, GetComponent<Transform>().position, Quaternion.identity, 0);
                }
            }
        }
        else
        {
            GetComponent<Renderer>().material = Red;
        }
    }
}
