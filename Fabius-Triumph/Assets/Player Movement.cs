using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float w_speed, wb_speed, run_speed, ro_speed;
    private float olw_speed;
    public bool walking;
    public Transform playerTrans;

    void Start()
    {
        olw_speed = w_speed;
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection = transform.forward * w_speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirection = -transform.forward * wb_speed;
        }

        playerRigid.velocity = moveDirection * Time.deltaTime;

        if (moveDirection == Vector3.zero)
        {
            playerRigid.velocity = Vector3.zero;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerAnim.SetBool("walk", true);
            playerAnim.SetBool("walkback", false);
            playerAnim.SetBool("run", false);
            playerAnim.SetBool("idle", false);
            walking = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerAnim.SetBool("walk", false);
            playerAnim.SetBool("walkback", true);
            playerAnim.SetBool("run", false);
            playerAnim.SetBool("idle", false);
            walking = true;
        }
        else
        {
            playerAnim.SetBool("walk", false);
            playerAnim.SetBool("walkback", false);
            playerAnim.SetBool("run", false);
            playerAnim.SetBool("idle", true);
            walking = false;
        }

        if (walking && Input.GetKey(KeyCode.LeftShift))
        {
            w_speed = olw_speed + run_speed;
            playerAnim.SetBool("run", true);
            playerAnim.SetBool("walk", false);
        }
        else if (walking)
        {
            w_speed = olw_speed;
            playerAnim.SetBool("run", false);
            playerAnim.SetBool("walk", true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            playerTrans.Rotate(0, -ro_speed * Time.deltaTime, 0);
            Debug.Log("Rotating Left: " + playerTrans.rotation.eulerAngles);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerTrans.Rotate(0, ro_speed * Time.deltaTime, 0);
            Debug.Log("Rotating Right: " + playerTrans.rotation.eulerAngles);
        }
    }
}
