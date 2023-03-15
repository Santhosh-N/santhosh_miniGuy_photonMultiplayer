using Photon.Pun;
using UnityEngine;

public class GiftBox : MonoBehaviourPun,IDamageable
{
    private int health =  5;

    [SerializeField]
    private GameObject coin;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Damage(int value)
    {
        health -= value;
        anim.SetTrigger("Bounce");
        if (health <= 0)
        {
            
            Instantiate(coin, transform.position, Quaternion.identity);
               
            
            Destroy(gameObject);
        }
    }
}
