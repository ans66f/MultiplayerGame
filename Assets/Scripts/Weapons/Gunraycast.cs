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
    float BulletSpeed = 100.0f;
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


    [PunRPC]
    void SpawnBullet()
    {
        CurrentAmmo--;


        CurrentRateOfFireValue = RateOfFire;

        Ray r = new Ray(bulletSpawn.transform.position, bulletSpawn.transform.forward);;
        Vector3 bulletDirection = r.direction;

        GameObject b = Instantiate(BulletTemplate, bulletSpawn.transform.position, bulletSpawn.transform.rotation, null);

        b.GetComponent<Rigidbody>().velocity = bulletDirection.normalized * BulletSpeed;



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
        GetComponent<LineRenderer>().enabled = false;

    }
}
