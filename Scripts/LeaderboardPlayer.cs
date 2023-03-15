using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardPlayer : MonoBehaviour
{

    public TMP_Text playerNameText, coinsText;

    public void SetDetails(string name,int coins)
    {
        playerNameText.text = name;
        coinsText.text = coins.ToString();
    }
}
