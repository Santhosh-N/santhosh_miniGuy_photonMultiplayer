using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class MiniEnemy : MonoBehaviourPunCallbacks,IDamageable
{

    public int health = 5;
    [SerializeField]
    private GameObject enemyExplosion;


    public void Damage(int value)
    {
        health -= value;
        Debug.Log(health);
        if(health <= 0)
        {

            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        Instantiate(enemyExplosion,transform.position + new Vector3(0,- transform.position.y,0),transform.rotation);
        Destroy(this.gameObject);
        
    }

  
}
