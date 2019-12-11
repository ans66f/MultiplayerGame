using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : Photon.MonoBehaviour
{
    [SerializeField] private int _healing;
    public int healing
    {
        get { return _healing; }
        set { _healing = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {    
            Player player = other.GetComponent<Player>();
            

            if (player != null)
            {
                if (player.currHealth != player.maxHealth)
                {
                    player.DoModifyHealth(player.currHealth + healing);
                    // play an audio TODO
                    DoDeactivateHealthPack();
                }
            }
        }
    }

    [PunRPC]
    void deactivateHealthPack()
    {
        HealthPackGenerator associatedGenerator = GetComponentInParent<HealthPackGenerator>();
        gameObject.SetActive(false);
        if (associatedGenerator != null)
        {
            associatedGenerator.ReloadPack();
        }
    }

    void DoDeactivateHealthPack()
    {
        photonView.RPC("deactivateHealthPack", PhotonTargets.AllBuffered);
    }
}
