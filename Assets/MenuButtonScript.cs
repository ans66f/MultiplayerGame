using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour
{

    public GameObject UsernameTextObject;
    public GameObject PasswordTextObject;


    string usernametext;
    string passwordtext;




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
        if (buttontype == 2) // create account
        {
            Debug.Log("Create Account Button Pressed");

            Debug.Log("Username: " + usernametext);
            Debug.Log("Password: " + passwordtext);

        }
        if (buttontype == 3) // login
        {
            Debug.Log("Login Account Button Pressed");

            Debug.Log("Username: " + usernametext);
            Debug.Log("Password: " + passwordtext);

        }
        if (buttontype == 4)
        {
            Debug.Log("Exit Button Pressed");

            Application.Quit();
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        if (UsernameTextObject != null && PasswordTextObject != null)
        {
            usernametext = UsernameTextObject.GetComponent<InputField>().text;
            passwordtext = PasswordTextObject.GetComponent<InputField>().text;
        }
    }
    
}
