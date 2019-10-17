using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockscript : Photon.MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CallRemoveBlock()
    {
        photonView.RPC("RemoveBlock", PhotonTargets.All);
    }

    [PunRPC]
    void RemoveBlock()
    {
        Debug.Log("Destroyed block :" + gameObject.transform.position);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
