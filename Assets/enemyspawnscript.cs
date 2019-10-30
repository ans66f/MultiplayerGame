using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawnscript : MonoBehaviour
{
    public GameObject EnemyTemplate;

    float spawntimer;

    // Start is called before the first frame update
    void Start()
    {
        spawntimer = 2.0f; 
    }

    // Update is called once per frame
    void Update()
    {
        spawntimer -= Time.deltaTime;

        if(spawntimer <= 0)
        {
            spawntimer = Random.Range(5.0f, 20.0f);

           PhotonNetwork.Instantiate(EnemyTemplate.name, GetComponent<Transform>().position, Quaternion.identity, 0);
        }
        
    }
}
