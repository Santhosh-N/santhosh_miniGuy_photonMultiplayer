using Cinemachine;
using System.Collections;
using UnityEngine;

public class GetPlayerInCinemachine : MonoBehaviour
{
    public static GetPlayerInCinemachine instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
        if (player != null)
        {

            //gameObject.GetComponent<CinemachineVirtualCamera>().LookAt = player.transform;
            gameObject.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
        }

    }
    
}
