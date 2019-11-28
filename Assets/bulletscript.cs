using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : Photon.MonoBehaviour
{

    public GameObject player;
    int damage = 0;
    int GunType;

    float speed = 50.0f;


    //AudioSource audioPistol;
    //AudioSource audioGatling;

    float DespawnTimer = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Play(0);
        player.GetComponent<Player>().bulletsShot += 1;
    }

    public void SetValues(int d, GameObject p, int gtype)
    {
        damage = d;
        player = p;
        GunType = gtype;
    }

    // Update is called once per frame
    void Update()
    {

        //gameObject.GetComponent<Transform>().LookAt(GetComponent<Rigidbody>().velocity.normalized);

        DespawnTimer -= Time.deltaTime;
        if(DespawnTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().DoModifyHealth(other.gameObject.GetComponent<Player>().currHealth - damage);
            player.GetComponent<Player>().DoModifyMoney(player.GetComponent<Player>().currMoney - 1);
        }
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("HitEnemy");
            if (other.gameObject.GetComponent<AIStalk>().currHealth - damage <= 0)
            {
                player.GetComponent<Player>().DoModifyMoney(player.GetComponent<Player>().currMoney + other.gameObject.GetComponent<AIStalk>().MoneyWorth);
            }
            other.gameObject.GetComponent<AIStalk>().DoModifyHealth(other.gameObject.GetComponent<AIStalk>().currHealth - damage);
            if (other.gameObject.GetComponent<AIStalk>().currHealth <= 0)
            {
                player.GetComponent<Player>().nbOfKills += 1;
            }
        }
        if (other.gameObject.tag != "AttackBox")
        {
            Destroy(gameObject);
        }
    }


    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().DoModifyHealth(collision.gameObject.GetComponent<Player>().currHealth - damage);
            player.GetComponent<Player>().DoModifyMoney(player.GetComponent<Player>().currMoney - 1);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("HitEnemy");
            if (collision.gameObject.GetComponent<AIStalk>().currHealth - damage <= 0)
            {
                player.GetComponent<Player>().DoModifyMoney(player.GetComponent<Player>().currMoney + collision.gameObject.GetComponent<AIStalk>().MoneyWorth);
            }
            collision.gameObject.GetComponent<AIStalk>().DoModifyHealth(collision.gameObject.GetComponent<AIStalk>().currHealth - damage);
        }
        Destroy(gameObject);
    }
    */

}
