using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour
{

    public GameObject UsernameTextObject;
    public GameObject PasswordTextObject;

    public GameObject LoginButtonObject;
    public GameObject CreateAccountButtonObject;

    public GameObject LoadingText;
    public GameObject UsernameTakenText;
    public GameObject WrongUsernameText;
    public GameObject WrongPasswordText;


    string usernametext;
    string passwordtext;

    string log;

    public GameObject DbControllerManager;

    public int buttontype;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

    }

    void TaskOnClick()
    {


        if (buttontype == 1) // can we play the game without being connected?
        {
            Debug.Log("Start Button Pressed");

            SceneManager.LoadScene("GarethTestscene");

        }
        else
        {
            UsernameTakenText.SetActive(false);
            WrongPasswordText.SetActive(false);
            WrongUsernameText.SetActive(false);
        }
        if (buttontype == 2) // create account
        {
            Debug.Log("Create Account Button Pressed");
            StartCoroutine(CreateAccount());
        }
        if (buttontype == 3) // login
        {
            Debug.Log("Login Account Button Pressed");
            StartCoroutine(Login());
        }
        if (buttontype == 4)
        {
            Debug.Log("Exit Button Pressed");

            Application.Quit();
        }
    }

    IEnumerator CreateAccount()
    {
        DbControllerManager.GetComponent<dbController>().CreateUser(usernametext, DbControllerManager.GetComponent<dbController>().GetSha1(passwordtext));

        LoadingText.SetActive(true);
        yield return new WaitForSeconds(1);
        log = DbControllerManager.GetComponent<dbController>().log;
        LoadingText.SetActive(false);

        if (log.Equals("user_already_exists"))
        {
            UsernameTakenText.SetActive(true);
        }
        else
        {
            DataHandler.username = usernametext;
            SceneManager.LoadScene("GarethTestscene");
        }
    }

    IEnumerator Login()
    {
        DbControllerManager.GetComponent<dbController>().CheckUser(usernametext, DbControllerManager.GetComponent<dbController>().GetSha1(passwordtext));

        LoadingText.SetActive(true);
        yield return new WaitForSeconds(1);
        log = DbControllerManager.GetComponent<dbController>().log;
        LoadingText.SetActive(false);

        if (log.Equals("wrong_password"))
        {
            WrongPasswordText.SetActive(true);
        } else if (log.Equals("no_such_username")) {
            WrongUsernameText.SetActive(true);
        } else
        {
            DataHandler.username = usernametext;
            SceneManager.LoadScene("GarethTestscene"); // we need to pass on the username here...
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (LoginButtonObject != null && CreateAccountButtonObject != null)
        {
            if (usernametext != "" && passwordtext != "")
            {
                LoginButtonObject.GetComponent<Button>().interactable = true;
                CreateAccountButtonObject.GetComponent<Button>().interactable = true;
            } else
            {
                LoginButtonObject.GetComponent<Button>().interactable = false;
                CreateAccountButtonObject.GetComponent<Button>().interactable = false;
            }
        }

        if (UsernameTextObject != null && PasswordTextObject != null)
        {
            usernametext = UsernameTextObject.GetComponent<InputField>().text;
            passwordtext = PasswordTextObject.GetComponent<InputField>().text;
        }
    }
    
}
