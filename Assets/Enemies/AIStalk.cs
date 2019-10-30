using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIStalk : Photon.MonoBehaviour
{
    Transform destination;

    GameObject[] players;
    public float EnemySpeed;


    public int MoneyWorth = 10;

    [Header("Health")]

    public int maxHealth = 200;
    public int currHealth;
    public Text currHealthLabelworldspace;
    public GameObject healthbarworldspace;
    float healthbarwidth;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        healthbarwidth = healthbarworldspace.GetComponent<RawImage>().rectTransform.rect.width;
    }



    void UpdateGUI()
    {
        if (currHealthLabelworldspace != null)
            currHealthLabelworldspace.text = currHealth.ToString();

        if (currHealthLabelworldspace != null)
            currHealthLabelworldspace.text = currHealth.ToString();
    }

    public void DoModifyHealth(int amount)
    {
        photonView.RPC("ModifyHealth", PhotonTargets.AllBuffered, amount);
    }

    [PunRPC]
    public void ModifyHealth(int amount)
    {
        currHealth = amount;
        UpdateGUI();

    }

    // Update is called once per frame
    void Update()
    {
        float f = (float)maxHealth / (float)currHealth;
        healthbarworldspace.GetComponent<RawImage>().rectTransform.sizeDelta = new Vector2(healthbarwidth / f, healthbarworldspace.GetComponent<RawImage>().rectTransform.rect.height);

        if(currHealth <= 0)
        {
            Destroy(gameObject);
        }

        players = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearestplayer = null;
        float closest = 1000000.0f;

        foreach (GameObject player in players)
        {
            Vector3 disp = player.transform.position - GetComponent<Transform>().position;
            if (disp.magnitude < closest)
            {
                closest = disp.magnitude;
                nearestplayer = player;
            }
        }



        if (nearestplayer != null)
        {
            Vector3 disptonearest = nearestplayer.transform.position - GetComponent<Transform>().position;
            if (disptonearest.magnitude > 2)
            {
                

                float speed = EnemySpeed * Time.deltaTime;

                transform.LookAt(nearestplayer.transform);
                Vector3 v = new Vector3(transform.forward.x * speed, 0, transform.forward.z * speed);

                GetComponent<Rigidbody>().velocity = v;
                //transform.Translate(transform.forward * 5 * Time.deltaTime);
            }
            else
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
        }

    }
    
}
