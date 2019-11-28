using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gunraycast : Photon.MonoBehaviour
{

    public GameObject player;
    GameObject blockmanager;

    public GameObject bulletSpawn;
    public GameObject BulletTemplate;

    bool ToggleCreateMode = false;

    public float RateOfFire = 0.2f;
    float CurrentRateOfFireValue = 0.0f;

    public int Damage = 10;
    public float BulletSpeed = 10000.0f;
    public int GunType = -1;

    public bool isleftclick = false;

    public GameObject Minigun = null;
    public bool IsMinigun = false;
    bool isPaused = false;


    //public Transform target;


    public GameObject AmmoTextObject;
    public int CurrentAmmo = 0;
    public int MaxCurrentAmmo = 10;
    public int CurrentAmmoStorage = 40;
    public int MaxAmmoStorage = 50;

    public void DoReload()
    {
        int d = MaxCurrentAmmo - CurrentAmmo;


        if(CurrentAmmoStorage - d >= 0)
        {
            CurrentAmmoStorage -= d;
            CurrentAmmo += d;
        }
        else
        {       
            CurrentAmmo += CurrentAmmoStorage;
            CurrentAmmoStorage = 0;
        }
    }

    void SetAmmoLimits()
    {
        if(CurrentAmmo <= 0)
        {
            CurrentAmmo = 0;
        }
        if(CurrentAmmo >= MaxCurrentAmmo)
        {
            CurrentAmmo = MaxCurrentAmmo;
        }

        if(CurrentAmmoStorage <= 0)
        {
            CurrentAmmoStorage = 0;
        }
        if(CurrentAmmoStorage >= MaxAmmoStorage)
        {
            CurrentAmmoStorage = MaxAmmoStorage;
        }



        AmmoTextObject.GetComponent<Text>().text = CurrentAmmo + " / " + CurrentAmmoStorage;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    void AddForceToPlayer()
    {

    }

    [PunRPC]
    void SpawnBullet()
    {
        CurrentAmmo--;


        CurrentRateOfFireValue = RateOfFire;

        //Quaternion randomRotation = Random.rotation;

        //float angle;
        //Vector3 axis;

        //randomRotation.ToAngleAxis(out angle, out axis);

        //Quaternion rotation = Quaternion.AngleAxis(angle, axis);

        //To Euler
        // Vector3 inEuler = randomRotation.eulerAngles;
        //Back into Quaternion
        // Quaternion inQuaternion = Quaternion.Euler(inEuler);


        //Vector3 relativePos = target.position - target.position;
        //transform.rotation = Quaternion.LookRotation(relativePos);


        //Ray r = new Ray(gameObject.transform.position, gameObject.transform.forward);
        //Vector3 bulletDirection =  r.direction * BulletSpeed * Time.deltaTime;

        //GameObject b = Instantiate(BulletTemplate, gameObject.transform.position, Quaternion.identity, null);
        //b.transform.LookAt(transform.position + bulletDirection);

        //b.GetComponent<Rigidbody>().velocity = bulletDirection;

        Ray r = new Ray(bulletSpawn.transform.position, bulletSpawn.transform.forward);;
        Vector3 bulletDirection = r.direction * BulletSpeed * Time.deltaTime;

        GameObject b = Instantiate(BulletTemplate, bulletSpawn.transform.position, bulletSpawn.transform.rotation, null);
        //b.transform.LookAt(transform.position + bulletDirection);

        b.GetComponent<Rigidbody>().velocity = bulletDirection;



        if (!photonView.isMine) Damage = 0; //so that a bullet will only hit the target once and not a second time on the copy

        b.GetComponent<bulletscript>().SetValues(Damage, player, GunType);
        b.GetComponent<bulletscript>().player = player;

        if(GunType == 1)
        {
            b.GetComponent<AudioSource>().clip = player.GetComponent<AudioReferences>().pistolbullet;
        }
        if (GunType == 2)
        {
            b.GetComponent<AudioSource>().clip = player.GetComponent<AudioReferences>().pistolbullet;
        }
        if (GunType == 3)
        {
            b.GetComponent<AudioSource>().clip = player.GetComponent<AudioReferences>().gatlingbullet;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            UnityEditor.EditorApplication.isPaused = !isPaused;

        SetAmmoLimits();

        if(Input.GetKeyDown(KeyCode.R) && isleftclick == false)
        {
            DoReload();
        }


        if (blockmanager != null)
        {

        }
        else
        {
            blockmanager = GameObject.FindGameObjectWithTag("GameManager");
        }


        Debug.DrawRay(gameObject.transform.position, (gameObject.transform.forward * 100), Color.red, 1);

        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        if (photonView.isMine)
        {
            RaycastHit hit;
            if (Input.GetMouseButton(0))
            {
                isleftclick = true;
                Ray r = new Ray(gameObject.transform.position, gameObject.transform.forward);
                if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, Mathf.Infinity, layerMask))
                {

                }




                if (player.GetComponent<Player>().isDead == false)
                {

                    CurrentRateOfFireValue -= Time.deltaTime;
                    if (CurrentRateOfFireValue <= 0)
                    {

                        if (CurrentAmmo > 0)
                        {

                            if (IsMinigun)
                            {
                                if (Minigun.GetComponent<minigunscript>().IsSpunUp)
                                {
                                    photonView.RPC("SpawnBullet", PhotonTargets.All);

                                }
                            }
                            else
                            {

                                photonView.RPC("SpawnBullet", PhotonTargets.All);
                            }
                        }
                    }


                }

                /*
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    if(hit.collider.gameObject.GetComponent<AIStalk>().currHealth - player.GetComponent<Player>().ShootDamage <= 0)
                    {
                        player.GetComponent<Player>().DoModifyMoney(player.GetComponent<Player>().currMoney + hit.collider.gameObject.GetComponent<AIStalk>().MoneyWorth);
                    }
                    hit.collider.gameObject.GetComponent<AIStalk>().DoModifyHealth(hit.collider.gameObject.GetComponent<AIStalk>().currHealth - player.GetComponent<Player>().ShootDamage);

                }


                    if (hit.collider.gameObject.tag == "Player")
                {
                    hit.collider.gameObject.GetComponent<Player>().DoModifyHealth(hit.collider.gameObject.GetComponent<Player>().currHealth - player.GetComponent<Player>().ShootDamage);
                    player.GetComponent<Player>().DoModifyMoney(player.GetComponent<Player>().currMoney - 1);
                }
                

                if (hit.collider.gameObject.tag == "block")
                {

                    Vector3 blockpos = hit.collider.gameObject.transform.position;
                    Debug.Log("HitBlock " + blockpos);



                    Vector3 pos = hit.collider.gameObject.transform.position;

                    if (ToggleCreateMode)
                    {
                        Vector3 disp = hit.collider.gameObject.GetComponent<Transform>().position - hit.point;

                        Debug.Log(disp.x);

                        if (disp.x > 0.4f) disp.x = 1;
                        else if (disp.x < -0.4f) disp.x = -1;
                        else disp.x = 0;


                        if (disp.y > 0.4f) disp.y = 1;
                        else if (disp.y < -0.4f) disp.y = -1;
                        else disp.y = 0;


                        if (disp.z > 0.4f) disp.z = 1;
                        else if (disp.z < -0.4f) disp.z = -1;
                        else disp.z = 0;

                        Vector3 newblockpos = hit.collider.gameObject.GetComponent<Transform>().position - disp;
                        blockmanager.GetComponent<GameManager>().CallCreateBlock(newblockpos);

                    }
                    else
                    {

                        blockmanager.GetComponent<GameManager>().CallDestroyBlock(hit.collider.gameObject.GetComponent<blockscript>().blockid);
                    }

                }
                if (hit.collider.gameObject.tag == "Player")
                {
                    //  hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(-hit.collider.gameObject.transform.forward * 50);
                    Debug.Log("Player Hit" + hit.collider.gameObject.name);

                    Vector3 p = new Vector3((-hit.collider.gameObject.transform.forward * 50).x, (-hit.collider.gameObject.transform.forward * 50).y, (-hit.collider.gameObject.transform.forward * 50).z);


                    //int ownerId = hit.collider.gameObject.GetComponent<PhotonView>().ownerId;
                    hit.collider.gameObject.GetComponent<Player>().DoForceThing();
                    hit.collider.gameObject.GetComponent<Player>().HitChangeColour(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));


                    // hit.collider.gameObject.GetComponent<Player>().


                }
                */
            }
            else
            {
                isleftclick = false;
                CurrentRateOfFireValue = 0.0f;
            }
        }


        if(Input.GetKeyDown(KeyCode.F)) {
            ToggleCreateMode = !ToggleCreateMode;
        }



        int i = 0;

        Vector3[] linepositions = new Vector3[2];
        linepositions[0] = gameObject.transform.position;
        linepositions[1] = (gameObject.transform.forward * 100) + gameObject.transform.position;

     //   GetComponent<LineRenderer>().SetPositions(linepositions);

    }
}
