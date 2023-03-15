using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviourPunCallbacks
{

    private Transform target;

    [Header("Attributes")]
    [SerializeField]
    private float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [SerializeField]
    private float turnSpeed = 10f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    public GameObject bulletPrefab;
    public GameObject rocketBulletPrefab;
    public GameObject miniGunBulletPrefab;
    public Transform firePoint;

    private GameObject shootButton;
    private GameObject miniGunShootButton;
    private GameObject rocketGunShootButton;

    private GameObject pistalSelectButton;
    private GameObject miniSelectButton;
    private GameObject rocketSelectButton;

    public Weapons weaponsScript;

    public GameObject enemyMarkEffect;
    private GameObject spawnEnemyMarkEffect;

    public GameObject giftBox;

    public GameObject pistalSkin;
    public GameObject miniGunSkin;
    public GameObject rocketLauncherSkin;

    public GameObject[] playerSkin;

    private bool onMiniGun = false;



    private void OnCollisionEnter(Collision collision)
    {
        if (photonView.IsMine)
        {
            Coin coin = collision.gameObject.GetComponent<Coin>();
            if (coin != null)
            {
                MatchManager.instance.UpdateStatsSend(PhotonNetwork.LocalPlayer.ActorNumber, 0, 1);
                SoundManager.PlaySound("PickUp");
              
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {

            InvokeRepeating("UpdateTarget", 0f, 0.5f);

            AssignUIButtons();
            InvokeRepeating("SpawnGift", 0f, 10f);

            this.GetComponent<PhotonView>().RPC("SetPlayerSkin", RpcTarget.AllBuffered, GameManager.instance.playerSkinValue);

        }


            

    }


    void SpawnGift()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(giftBox.name, PlayerSpawner.instance.SpawnPoints[Random.Range(0, PlayerSpawner.instance.SpawnPoints.Length)].position, Quaternion.identity);
        }
    }
    

    [PunRPC]
    public void SetPlayerSkin(int value)
    {
        DisableAllPlayerSkin();
        playerSkin[value].SetActive(true);
    }

    void DisableAllPlayerSkin()
    {
        for(int i = 0; i<playerSkin.Length;i++)
        {
            playerSkin[i].SetActive(false);
        }
    }

    void DisableAllGunSkin()
    {
        pistalSkin.SetActive(false);
        miniGunSkin.SetActive(false);
        rocketLauncherSkin.SetActive(false);
    }

    void AssignUIButtons()
    {
        shootButton = GameObject.FindGameObjectWithTag("ShootButton").gameObject;
        miniGunShootButton = GameObject.FindGameObjectWithTag("MiniGunShootButton").gameObject;
        rocketGunShootButton = GameObject.FindGameObjectWithTag("RocketGunShootButton").gameObject;
        pistalSelectButton = GameObject.FindGameObjectWithTag("PistalSelectButton").gameObject;
        miniSelectButton = GameObject.FindGameObjectWithTag("MiniSelectButton").gameObject;
        rocketSelectButton = GameObject.FindGameObjectWithTag("RocketSelectButton").gameObject;

        pistalSelectButton.GetComponent<Button>().onClick.AddListener(delegate { EnableGunButton("pistal"); });
        miniSelectButton.GetComponent<Button>().onClick.AddListener(delegate { EnableGunButton("mini"); });
        rocketSelectButton.GetComponent<Button>().onClick.AddListener(delegate { EnableGunButton("rocket"); });


        shootButton.GetComponent<Button>().onClick.AddListener(Shoot);
        rocketGunShootButton.GetComponent<Button>().onClick.AddListener(RocketShoot);

        EventTrigger triggerDown = miniGunShootButton.gameObject.AddComponent<EventTrigger>();
        var pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((e) => MiniGunShoot());
        triggerDown.triggers.Add(pointerDown);

        EventTrigger triggerUp = miniGunShootButton.gameObject.AddComponent<EventTrigger>();
        var pointerUp = new EventTrigger.Entry();
        pointerUp.eventID = EventTriggerType.PointerUp;
        pointerUp.callback.AddListener((e) => MiniGunShootUp());
        triggerUp.triggers.Add(pointerUp);
    }



    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            if (enemy == gameObject)
            {

            }
            else
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;

                  
                }
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            if (spawnEnemyMarkEffect != null)
            {
                Destroy(spawnEnemyMarkEffect);
                spawnEnemyMarkEffect = null;
            }
            if (spawnEnemyMarkEffect == null)
            {
                spawnEnemyMarkEffect = Instantiate(enemyMarkEffect, target.transform.position + new Vector3(0,0.2f,0),Quaternion.Euler(-90, 0, 0));
                spawnEnemyMarkEffect.transform.parent = target;
            }
            PlayerJoystickController.canRotate = false;

           

        }
        else
        {
            if (spawnEnemyMarkEffect != null)
            {
                Destroy(spawnEnemyMarkEffect);
               spawnEnemyMarkEffect = null;
          
            }
            target = null;
            PlayerJoystickController.canRotate = true;
        }


       
    }

    public void Shoot()
    {
        if (PlayerJoystickController.canRotate == false)
        {
            if (weaponsScript.pistalBulletCount > 0)
            {
                weaponsScript.pistalBulletCount -= 1;
                UIManager.instance.PistalBulletCountTextUpdate(weaponsScript.pistalBulletCount);
                SoundManager.PlaySound("PistalBulletSound");
                GameObject bulletGO = PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position, firePoint.rotation);
                Destroy(bulletGO, 5f);
            }
            else
            {
                StartCoroutine(callInstruction("Need Ammo"));
            }
        }
        else
        {
            if (weaponsScript.pistalBulletCount <= 0)
            {
                StartCoroutine(callInstruction("Need Ammo"));
            }
            else
                StartCoroutine(callInstruction("No Target Found"));
        }
        
    }

    public void RocketShoot()
    {
        if (PlayerJoystickController.canRotate == false)
        {
            if (weaponsScript.rocketGunBulletCount > 0)
            {
                weaponsScript.rocketGunBulletCount -= 1;
                UIManager.instance.RocketGunBulletCountTextUpdate(weaponsScript.rocketGunBulletCount);
                SoundManager.PlaySound("RocketLauncher");
                GameObject bulletGO = PhotonNetwork.Instantiate(rocketBulletPrefab.name, firePoint.position, firePoint.rotation);
                Destroy(bulletGO, 5f);
            }
            else
            {
                StartCoroutine(callInstruction("Need Ammo"));
            }
        }
        else
        {
            if(weaponsScript.rocketGunBulletCount <=0)
            {
                StartCoroutine(callInstruction("Need Ammo"));
            }
            else
            StartCoroutine(callInstruction("No Target Found"));
        }

    }

    public void MiniGunShoot()
    {
         onMiniGun = true;
        if (PlayerJoystickController.canRotate == false)
        {

            if (weaponsScript.miniGunBulletCount > 0)
            {

                weaponsScript.miniGunBulletCount -= 1;
                UIManager.instance.MiniGunBulletCountTextUpdate(weaponsScript.miniGunBulletCount);
                SoundManager.PlaySound("PistalBulletSound");
                GameObject bulletGO = PhotonNetwork.Instantiate(miniGunBulletPrefab.name, firePoint.position, firePoint.rotation);
                Destroy(bulletGO, 5f);
            }
            else
            {
                StartCoroutine(callInstruction("Need Ammo"));
            }
        }
        else
        {
            if (weaponsScript.miniGunBulletCount <= 0)
            {
                StartCoroutine(callInstruction("Need Ammo"));
            }
            else
                StartCoroutine(callInstruction("No Target Found"));
        }

    }

    public void MiniGunShootUp()
    {
        onMiniGun = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (target == null) return;

            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            if (onMiniGun)
            {
                if (fireCountdown <= 0f)
                {
                    MiniGunShoot();
                    fireCountdown = 0.1f / fireRate;
                }
                fireCountdown -= Time.deltaTime;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


    public void DisableAllGunButton()
    {
        shootButton.GetComponent<Button>().enabled = false;
        shootButton.transform.GetChild(0).gameObject.SetActive(false);
        miniGunShootButton.GetComponent<Button>().enabled = false;
        miniGunShootButton.transform.GetChild(0).gameObject.SetActive(false);
        rocketGunShootButton.GetComponent<Button>().enabled = false;
        rocketGunShootButton.transform.GetChild(0).gameObject.SetActive(false);
    }

    [PunRPC]
    public void DisplayGun(string gunName)
    {
        DisableAllGunSkin();
        switch (gunName)
        {
            case "pistal":

                pistalSkin.SetActive(true); break;
            case "mini":
                miniGunSkin.SetActive(true); break;
            case "rocket":
                rocketLauncherSkin.SetActive(true); break;
            default: break;
        }
    }

    public void EnableGunButton(string gunName)
    {
        DisableAllGunButton();
        DisableAllGunSkin();
        SoundManager.PlaySound("ItemPickUp");
        this.GetComponent<PhotonView>().RPC("DisplayGun", RpcTarget.AllBuffered, gunName);
        switch (gunName)
        {
            case "pistal":                
                shootButton.GetComponent<Button>().enabled = true;
                shootButton.transform.GetChild(0).gameObject.SetActive(true); break;
            case "mini":
                miniGunShootButton.GetComponent<Button>().enabled = true;
                miniGunShootButton.transform.GetChild(0).gameObject.SetActive(true); break;
            case "rocket":
                rocketGunShootButton.GetComponent<Button>().enabled = true;
                rocketGunShootButton.transform.GetChild(0).gameObject.SetActive(true); break;
            default:break;
        }
    }

    IEnumerator callInstruction(string data)
    {
        UIManager.instance.instructionText.gameObject.SetActive(true);
        UIManager.instance.instructionText.text = data;
        yield return new WaitForSeconds(2f);
        UIManager.instance.instructionText.gameObject.SetActive(false);
    }

}
