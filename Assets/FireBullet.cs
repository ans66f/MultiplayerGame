using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public GameObject bulletSpawn;
    public GameObject BulletTemplate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateBullet();
        }
    }

    void CreateBullet()
    {
        //Ray r = new Ray(bulletSpawn.transform.position, bulletSpawn.transform.forward);
        Vector3 bulletDirection = bulletSpawn.transform.forward * 100f * Time.deltaTime;

        print(bulletSpawn.transform.position);
        GameObject b = Instantiate(BulletTemplate, bulletSpawn.transform.position, bulletSpawn.transform.rotation, null);
        //b.transform.LookAt(transform.position + bulletDirection);

        b.GetComponent<Rigidbody>().velocity = bulletDirection;
    }
}
