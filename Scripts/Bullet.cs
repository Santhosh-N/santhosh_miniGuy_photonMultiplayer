using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPunCallbacks
{

    public float Speed = 70f;
    public GameObject impactEffect;


    private void OnCollisionEnter(Collision collision)
    {
        IDamageable idamageable = collision.gameObject.GetComponent<IDamageable>();
        if (idamageable != null)
        {
            idamageable.Damage(1);
        }
        HitTarget();
    }


    // Update is called once per frame
    void Update()
    {
            transform.Translate(Vector3.forward * 30* Time.deltaTime, Space.Self);
    }

    void HitTarget()
    {
       
        GameObject effectIns = PhotonNetwork.Instantiate(impactEffect.name, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        Destroy(gameObject);
    }
}
