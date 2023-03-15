using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    private TMP_Text pistalBulletCountText;
    [SerializeField]
    private TMP_Text miniGunBulletCountText;
    [SerializeField]
    private TMP_Text rocketGunBulletCountText;

    public TMP_Text instructionText;
    public TMP_Text coinLabelText;

    public GameObject leaderboard;
    public GameObject endScreen;


    public LeaderboardPlayer leaderboardPlayerDisplay;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PistalBulletCountTextUpdate(int value)
    {
        pistalBulletCountText.text = value.ToString();
    }

    public void MiniGunBulletCountTextUpdate(int value)
    {
        miniGunBulletCountText.text = value.ToString();
    }

    public void RocketGunBulletCountTextUpdate(int value)
    {
        rocketGunBulletCountText.text = value.ToString();
    }
}
