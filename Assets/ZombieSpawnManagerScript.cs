using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManagerScript : MonoBehaviour
{

    public List<GameObject> Gates;
    GameObject[] EnemySpawners;
    

    // Start is called before the first frame update
    void Start()
    {
        EnemySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
    



    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
