using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float speed = 1.0f;
    public float xLimit = 1.0f;

    private void Start()
    {

    }

    void FixedUpdate()
    {
        ApplyForce();
    }

    private void Update()
    {
        
    }


    private void ApplyForce()
    {
        float xInput = Input.GetAxis("Horizontal");
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
