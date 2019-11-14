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
        Debug.Log("You have clicked the button!");
        if (buttontype == 1)
        {
            SceneManager.LoadScene("GarethTestscene");
        }
        if (buttontype == 2)
        {

        }  
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
