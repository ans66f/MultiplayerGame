using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressetextscript : MonoBehaviour
{
    public GameObject ThePressEText;
    float pressetimer = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetPressETextActive(bool t)
    {
        if (t)
        {
            ThePressEText.SetActive(true);
            pressetimer = 0.1f;
        }
        else
        {
            pressetimer = 0;
            ThePressEText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        pressetimer -= Time.deltaTime;
        if(pressetimer <= 0)
        {
            ThePressEText.SetActive(false);
        }
    }
}
