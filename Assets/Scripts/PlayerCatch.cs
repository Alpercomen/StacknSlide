using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class PlayerCatch : MonoBehaviour
{

    [SerializeField] 
    public Transform stackPosition;
    public float stackGap = 0.5f;
    public float speedIncrease = 0.5f;
    public float jumpDecrease = 2.0f;
    public float fovIncrease = 2;
    [HideInInspector]
    public int peopleCount = 0;

    private Vector3 newPos;
    private SplineFollower spline;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        peopleCount = 0;
        spline = GameObject.Find("Player").GetComponent<SplineFollower>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            Transform parent = other.transform;
            Transform child = parent.Find("Dummy").gameObject.transform;
            
            //Set the heads position on the stack
            newPos.y = 0f;
            newPos.x = 0; 
            newPos.z -= stackGap; 
            stackPosition.localPosition = newPos;

            //Increase our speed, increase our field of view, increase people count
            spline.followSpeed += speedIncrease;
            camera.fieldOfView += fovIncrease;
            peopleCount++;

            //Make the "Dummy" inside our parent object a child of this stack position.
            child.SetParent(stackPosition);
            child.localPosition = new Vector3(newPos.x, -1.5f, newPos.z);
            child.localRotation = Quaternion.identity;
            child.localScale = new Vector3(1.25f, 1.25f, 1.25f);

            newPos = stackPosition.localPosition;

            //Disable the parent position
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            GameObject head = GameObject.Find("Head");
            
            if(head.transform.childCount == 0)
            {
                //Implement death mechanic here
                Debug.Log("You are dead");
            } 
            else
            {
                //Destroy the first child, reduce our speed, reduce field of view, reduce people count
                Destroy(head.transform.GetChild(0).gameObject);
                spline.followSpeed -= speedIncrease;
                camera.fieldOfView -= fovIncrease;
                peopleCount--;
            }
        }
    }
}
