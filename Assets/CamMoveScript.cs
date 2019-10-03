using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveScript : MonoBehaviour
{

    public float LookSpeed = 10;

    float MouseX;
    float MouseY;

    // Start is called before the first frame update
    void Start()
    {

            Cursor.lockState = CursorLockMode.Locked;
        

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }


            MouseX = Input.GetAxis("Mouse X");
            MouseY = Input.GetAxis("Mouse Y");

            gameObject.transform.Rotate(new Vector3((-MouseY * LookSpeed), 0, 0));

    }
}
