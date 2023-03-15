using Photon.Pun;
using UnityEngine;

public class RocketBullet : MonoBehaviour
{

    public float speed = 30f;

    public GameObject explosion;


    private void OnCollisionEnter(Collision collision)
    {
        IDamageable idamageable = collision.gameObject.GetComponent<IDamageable>();
        if (idamageable != null)
        {
            idamageable.Damage(10);
            PhotonNetwork.Instantiate(explosion.name, transform.position, transform.rotation);
            SoundManager.PlaySound("EnemyBlast");
        }

        HitTarget();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    void HitTarget()
    {
        Destroy(gameObject);
    }
}
