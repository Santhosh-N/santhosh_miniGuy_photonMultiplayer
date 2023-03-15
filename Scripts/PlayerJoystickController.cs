using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerJoystickController : MonoBehaviourPunCallbacks
{
    private FixedJoystick moveJoystick;


    public static bool canRotate = true;

    public Animator anim;


    private void Start()
    {
        if (photonView.IsMine)
        {
            moveJoystick = GameObject.FindGameObjectWithTag("JoyStick").GetComponent<FixedJoystick>();
            GetPlayerInCinemachine.instance.FindPlayer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            float horizontal = moveJoystick.Horizontal;
            float vertical = moveJoystick.Vertical;

            if(horizontal == 0 || vertical == 0)
            {
                anim.SetBool("isMoving", false);
            }
            else
            {
                anim.SetBool("isMoving", true);
            }

            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
            if (canRotate)
            {

                Vector3 point = Vector3.RotateTowards(transform.forward, direction, 10 * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(point);
            }
#if PLATFORM_ANDROID
            transform.Translate(direction * 0.3f, Space.World);
#endif

             transform.Translate(direction * 0.02f, Space.World);
        }
    }
}
