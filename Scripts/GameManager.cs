using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerSkinValue = 0;



    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
