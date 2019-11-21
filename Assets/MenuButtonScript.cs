using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour
{
    public int buttontype;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
       
        if (buttontype == 1)
        {
            Debug.Log("Start Button Pressed");

            SceneManager.LoadScene("GarethTestscene");

        }
        if (buttontype == 2)
        {
            Debug.Log("Create Account Button Pressed");

        }
        if (buttontype == 3)
        {
            Debug.Log("Login Account Button Pressed");

        }
        if (buttontype == 4)
        {
            Debug.Log("Exit Button Pressed");

            Application.Quit();
        }
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
