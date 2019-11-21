using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponpickup : MonoBehaviour
{
    public int weapontype;
    GameObject PressETextObject;


    // Start is called before the first frame update
    void Start()
    {
        PressETextObject = GameObject.FindGameObjectWithTag("PressETextObject");
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.GetComponent<Player>().WeaponsObject.GetComponent<currentweaponscript>().ReloadWeapon(weapontype);
            }


            PressETextObject.GetComponent<pressetextscript>().SetPressETextActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PressETextObject.GetComponent<pressetextscript>().SetPressETextActive(false);
    }
}
