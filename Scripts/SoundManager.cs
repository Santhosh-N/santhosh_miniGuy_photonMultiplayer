using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static AudioSource audSrc;
    public static AudioClip pistalBulletSound, jumpSound, shotGunBulletSound, bulletDrop, enemyBlast, pickup, dropItem, powerUp, buttonClick, itemPickUp, rocketLauncher;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        pistalBulletSound = Resources.Load<AudioClip>("PistalBulletSound");
        jumpSound = Resources.Load<AudioClip>("Jump");
        shotGunBulletSound = Resources.Load<AudioClip>("ShotGunBulletSound");
        bulletDrop = Resources.Load<AudioClip>("BulletDrop");
        enemyBlast = Resources.Load<AudioClip>("EnemyBlast");
        pickup = Resources.Load<AudioClip>("PickUp");
        dropItem = Resources.Load<AudioClip>("DropItem");
        powerUp = Resources.Load<AudioClip>("PowerUp");
        buttonClick = Resources.Load<AudioClip>("ButtonClick");
        itemPickUp = Resources.Load<AudioClip>("ItemPickUp");
        rocketLauncher = Resources.Load<AudioClip>("RocketLauncher");
        audSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "PistalBulletSound":
                audSrc.PlayOneShot(pistalBulletSound);
                break;
            case "Jump":
                audSrc.PlayOneShot(jumpSound);
                break;
            case "ShotGunBulletSound":
                audSrc.PlayOneShot(shotGunBulletSound);
                break;
            case "BulletDrop":
                audSrc.PlayOneShot(bulletDrop);
                break;
            case "EnemyBlast":
                audSrc.PlayOneShot(enemyBlast);
                break;
            case "PickUp":
                audSrc.PlayOneShot(pickup);
                break;
            case "DropItem":
                audSrc.PlayOneShot(dropItem);
                break;
            case "PowerUp":
                audSrc.PlayOneShot(powerUp);
                break;
            case "ButtonClick":
                audSrc.PlayOneShot(buttonClick);
                break;
            case "ItemPickUp":
                audSrc.PlayOneShot(itemPickUp);
                break;
            case "RocketLauncher":
                audSrc.PlayOneShot(rocketLauncher);
                break;
            default:
                break;
        }

    }
}
