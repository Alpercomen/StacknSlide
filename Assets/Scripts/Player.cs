using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;


public class Player : MonoBehaviour
{
    public float horizontalSpeed = 1.0f;
    public float xLimit = 1.0f;
    public float downwardForce = 0.1f;
    public float jumpForce = 2.0f;
    public int ejectX = -125;
    public int ejectY = 125;
    public int ejectZ = 0;

    private Rigidbody rb;
    private float gForce = 0.0f;
    private bool onGround = true;
    private GameObject parent;
    private SplineFollower splineFollower;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        parent = GameObject.Find("Player");
        splineFollower = parent.gameObject.GetComponent<SplineFollower>();
    }

    void FixedUpdate()
    {
        //Move left right
        ApplyForce();
    }

    private void Update()
    {
        //Jump
        ApplyJump();
    }


    private void ApplyForce()
    {
        float xInput = Input.GetAxis("Horizontal");
        if (onGround) // If you want to be able to move in mid-air as well, simply remove this if condition.
        {

            //Check whether you are inside the xLimit
            if (Mathf.Abs(transform.localPosition.x) < xLimit)
            {
                transform.localPosition += new Vector3(xInput * horizontalSpeed, 0, 0);
            }

            //Help break free from the rightern limit
            if (xInput < 0 && transform.localPosition.x > 0)
            {
                transform.localPosition += new Vector3(xInput * horizontalSpeed, 0, 0);
            }

            //Help break free from the leftern limit
            if (xInput > 0 && transform.localPosition.x < 0)
            {
                transform.localPosition += new Vector3(xInput * horizontalSpeed, 0, 0);
            }
        }
    }

    private void ApplyJump()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {
            //Apply the initial vertical vector
            transform.localPosition += new Vector3(0, jumpForce * Time.deltaTime, 0);
            onGround = false;
        }

        if (!onGround)
        {
            //Continue to apply it, but gradually increase it and start to apply it on the negative vertical axis.
            transform.localPosition += new Vector3(0, (jumpForce + gForce) * Time.deltaTime, 0);
            gForce -= downwardForce;
        }

        if (transform.localPosition.y <= 0 && !onGround)
        {
            //Fix the spot of the player on exact y = 0 spot
            transform.localPosition = new Vector3(transform.localPosition.x, 0, 0);
            onGround = true;
            gForce = 0.0f; //Reset the gForce
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Finish"))
        {
            Eject();
        }
    }

    public void Eject()
    {
        
        
        float followerSpeed = splineFollower.followSpeed; //Store Spline Follower speed
        parent.GetComponent<Rigidbody>().velocity = new Vector3(ejectX, ejectY, ejectZ) * followerSpeed; //Apply vector on the given direction.
        Vector3 speed = parent.GetComponent<Rigidbody>().velocity; //Store it in speed Vector

        parent.transform.DetachChildren(); //Detach children, so that rigidbody can work as intended.

        if(this.gameObject.GetComponent<Rigidbody>() == null)
        {
            this.gameObject.AddComponent<Rigidbody>(); //Add rigidbody
            this.gameObject.GetComponent<Rigidbody>().AddForce(speed.x, speed.y, speed.z); //Add force to our Rigidbody
        }

    }
}

