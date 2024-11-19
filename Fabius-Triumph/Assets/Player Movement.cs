using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float w_speed, wb_speed, run_speed, ro_speed;
    private float olw_speed;  // original walking speed
    public bool walking;
    public Transform playerTrans;

    void Start()
    {
        // Store the original walking speed in olw_speed
        olw_speed = w_speed;
    }

    void FixedUpdate()
    {
        // Move forward
        if (Input.GetKey(KeyCode.W))
        {
            playerRigid.velocity = transform.forward * w_speed * Time.deltaTime;
        }
        // Move backward
        else if (Input.GetKey(KeyCode.S))
        {
            playerRigid.velocity = -transform.forward * wb_speed * Time.deltaTime;
        }
        else
        {
            // Stop sliding by setting velocity to zero when no movement keys are pressed
            playerRigid.velocity = Vector3.zero;
        }
    }

    void Update()
    {
        // Walking forward
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");
            walking = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.ResetTrigger("walk");
            playerAnim.SetTrigger("idle");
            walking = false;
        }

        // Walking backward
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetTrigger("walkback");
            playerAnim.ResetTrigger("idle");
            walking = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("walkback");
            playerAnim.SetTrigger("idle");
            walking = false;
        }

        // Rotating left
        if (Input.GetKey(KeyCode.A))
        {
            playerTrans.Rotate(0, -ro_speed * Time.deltaTime, 0);
        }

        // Rotating right
        if (Input.GetKey(KeyCode.D))
        {
            playerTrans.Rotate(0, ro_speed * Time.deltaTime, 0);
        }

        // Running logic
        if (walking)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                w_speed = olw_speed + run_speed;  // Increase speed for sprint
                playerAnim.SetTrigger("run");
                playerAnim.ResetTrigger("walk");
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                w_speed = olw_speed;  // Reset to normal walking speed
                playerAnim.ResetTrigger("run");
                playerAnim.SetTrigger("walk");
            }
        }
    }
}
