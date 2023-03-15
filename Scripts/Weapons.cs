using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Weapons : MonoBehaviourPunCallbacks
{

    public  int pistalBulletCount = 0;
    public int miniGunBulletCount = 0;
    public int rocketGunBulletCount = 0;

    [SerializeField]
    PlayerController playerAutoAim;

    private void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.gameObject.GetComponent<Pistal>())
            {
                if (pistalBulletCount < 60)
                {
                    SoundManager.PlaySound("ItemPickUp");
                    pistalBulletCount = 60;
                    UIManager.instance.PistalBulletCountTextUpdate(pistalBulletCount);
                    Destroy(other.gameObject);
                }

            }
            else if (other.gameObject.GetComponent<MiniGun>())
            {
                if (miniGunBulletCount < 120)
                {
                    SoundManager.PlaySound("ItemPickUp");
                    miniGunBulletCount = 120;
                    UIManager.instance.MiniGunBulletCountTextUpdate(miniGunBulletCount);
                    Destroy(other.gameObject);
                }

            }
            else if (other.gameObject.GetComponent<RocketGun>())
            {
                if (miniGunBulletCount < 120)
                {
                    SoundManager.PlaySound("ItemPickUp");
                    rocketGunBulletCount = 5;
                    UIManager.instance.RocketGunBulletCountTextUpdate(rocketGunBulletCount);
                    Destroy(other.gameObject);
                }

            }
        }
    }
}
