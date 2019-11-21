using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : Photon.MonoBehaviour
{

    public GameObject Wall;
    public int WallCost;
    public GameObject CostText;


    GameObject PressETextObject;

    // Start is called before the first frame update
    void Start()
    {
        PressETextObject = GameObject.FindGameObjectWithTag("PressETextObject");
    }

    // Update is called once per frame
    void Update()
    {
        CostText.GetComponent<Text>().text = "Cost: " + WallCost;
    }

    void DoDestroyWallAndThis()
    {
        photonView.RPC("DestroyWallAndThis", PhotonTargets.AllBuffered);
    }

    [PunRPC]
    void DestroyWallAndThis()
    {
        Wall.SetActive(false);
        GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y - 1000, GetComponent<Transform>().position.z);
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
                        
                        DoDestroyWallAndThis();
                        
                    }
                    else
                    {
                        Debug.Log("Not enough money skrub, need: " + (WallCost - other.gameObject.GetComponent<Player>().currMoney) + " more");
                    }
                }


                PressETextObject.GetComponent<pressetextscript>().SetPressETextActive(true);
            }
        }       
    }

    private void OnTriggerExit(Collider other)
    {
        PressETextObject.GetComponent<pressetextscript>().SetPressETextActive(false);
    }

}
