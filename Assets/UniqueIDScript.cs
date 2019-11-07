using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueIDScript : Photon.MonoBehaviour
{
    GameObject[] OtherPlayers;
    public int UniqueID = -1;
    // Start is called before the first frame update

    [PunRPC]
    void UpdateUniqueID(int id)
    {
        UniqueID = id;
    }

    void Start()
    {
        OtherPlayers = GameObject.FindGameObjectsWithTag("Player");
        if (PhotonNetwork.isMasterClient)
        {
            UniqueID = OtherPlayers.Length;
            photonView.RPC("UpdateUniqueID", PhotonTargets.OthersBuffered, UniqueID);
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.name = "Player " + UniqueID;
    }
}
