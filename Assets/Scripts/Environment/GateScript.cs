using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateScript : Photon.MonoBehaviour
{

    public int GateCost;
    public GameObject CostText;

    public bool IsGateOpen = false;

    public int GateNum;

    public GameObject OpenBoxCollider;
    public GameObject ClosedBoxCollider;

    GameObject ZombieSpawnManager;

    GameObject PressETextObject;

    GameObject ThisPlayer;

    // Start is called before the first frame update
    void Start()
    {
        PressETextObject = GameObject.FindGameObjectWithTag("PressETextObject");

        ZombieSpawnManager = GameObject.FindGameObjectWithTag("ZombieSpawnManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (ThisPlayer == null)
        {
            GameObject[] allplayers = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject p in allplayers)
            {
                if (p.GetPhotonView().isMine)
                {
                    ThisPlayer = p;
                }

            }
        }

        CostText.GetComponent<Text>().text = "Cost: " + GateCost;

        if (GetComponent<Animator>().GetBool("IsGateOpen"))
        {
            OpenBoxCollider.SetActive(true);

            ClosedBoxCollider.SetActive(false);
            GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            OpenBoxCollider.SetActive(false);

            ClosedBoxCollider.SetActive(true);
            GetComponent<BoxCollider>().enabled = true;

        }
    }

    void DoDestroyWallAndThis()
    {
        photonView.RPC("DestroyWallAndThis", PhotonTargets.AllBuffered);
    }

    [PunRPC]
    void DestroyWallAndThis()
    {
        IsGateOpen = true;
        GetComponent<Animator>().SetBool("IsGateOpen", IsGateOpen);

    }

    private void OnTriggerStay(Collider other)
    {
        // if (Wall.GetActive())
        {
            if (other.tag == "Player")
            {

                if(other.gameObject == ThisPlayer) {

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (other.gameObject.GetComponent<Player>().currMoney >= GateCost)
                        {
                            other.gameObject.GetComponent<Player>().DoModifyMoney(other.gameObject.GetComponent<Player>().currMoney - GateCost);

                            DoDestroyWallAndThis();

                        }
                        else
                        {
                            Debug.Log("Not enough money skrub, need: " + (GateCost - other.gameObject.GetComponent<Player>().currMoney) + " more");
                        }

                    }
                        PressETextObject.GetComponent<pressetextscript>().SetPressETextActive(true);
                    
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (photonView.isMine)
        {
            PressETextObject.GetComponent<pressetextscript>().SetPressETextActive(false);
        }
    }

}
