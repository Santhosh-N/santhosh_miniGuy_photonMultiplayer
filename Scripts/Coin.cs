using Photon.Pun;
using UnityEngine;

public class Coin : MonoBehaviourPunCallbacks
{
    
    private void OnCollisionEnter(Collision collision)
    {
            if (collision.gameObject.tag == "Player")
            {
                Destroy(gameObject);

            }
        
    }

}
