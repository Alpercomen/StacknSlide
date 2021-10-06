using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float speed = 1.0f;
    public float xLimit = 1.0f;
    public float yLimit = 1.0f;
    private float gForce = 0.0f;
    public float downwardForce = 0.1f;
    public float jumpForce = 2.0f;
    private bool onGround = true;

    void FixedUpdate()
    {
        ApplyForce();
    }

    private void Update()
    {
        ApplyJump();
    }


    private void ApplyForce()
    {
        float xInput = Input.GetAxis("Horizontal");
        if (onGround)
        {
            if(Mathf.Abs(transform.localPosition.x) < xLimit)
            {
                transform.localPosition += new Vector3(xInput * speed, 0, 0);
            }

            if (xInput < 0 && transform.localPosition.x > 0)
            {
                transform.localPosition += new Vector3(xInput * speed, 0, 0);
            }

            if (xInput > 0 && transform.localPosition.x < 0)
            {
                transform.localPosition += new Vector3(xInput * speed, 0, 0);
            }
        }
    }

    private void ApplyJump()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {
            transform.localPosition += new Vector3(0, jumpForce * Time.deltaTime, 0);
            onGround = false;
        } 

        if(!onGround)
        {
            transform.localPosition += new Vector3(0, (jumpForce + gForce) * Time.deltaTime, 0);
            gForce -= downwardForce;
        }

        if(transform.localPosition.y <= 0 && !onGround)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 0, 0);
            onGround = true;
            gForce = 0.0f;
        }
    }

}
