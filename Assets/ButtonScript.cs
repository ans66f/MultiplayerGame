using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : Photon.MonoBehaviour
{

    public GameObject Wall;
    public int WallCost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (Wall.GetActive())
        {
            if (other.tag == "Player")
            {

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (other.gameObject.GetComponent<Player>().currMoney >= WallCost)
                    {
                        other.gameObject.GetComponent<Player>().DoModifyMoney(other.gameObject.GetComponent<Player>().currMoney - WallCost);
                        Wall.SetActive(false);
                        Destroy(gameObject);
                    }
                    else
                    {
                        Debug.Log("Not enough money skrub, need: " + (WallCost - other.gameObject.GetComponent<Player>().currMoney) + " more");
                    }
                }
            }
        }
    }

}
