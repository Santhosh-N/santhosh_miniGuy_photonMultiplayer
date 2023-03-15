using UnityEngine;
using Photon.Pun;

public class PlayerControllerKeyboard : MonoBehaviourPunCallbacks
{
    private int speed = 5;

    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            transform.position += Movement * speed * Time.deltaTime;

            if (PlayerJoystickController.canRotate)
            {
                Vector3 point = Vector3.RotateTowards(transform.forward, Movement, 10 * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(point);
            }
            

            if (Movement == new Vector3(0, 0, 0))
            {
                anim.SetBool("isMoving", false);
            }
            else
            {
                anim.SetBool("isMoving", true);
            }
        }
    }
}
