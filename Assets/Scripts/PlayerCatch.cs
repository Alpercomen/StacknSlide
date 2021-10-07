using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatch : MonoBehaviour
{
    private static int peopleCount = 0;
    private Vector3 newPos;
    [SerializeField] Transform stackPosition;

    // Start is called before the first frame update
    void Start()
    {
        
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


            stackPosition.localPosition = newPos;

            child.SetParent(stackPosition);
            child.localPosition = new Vector3(newPos.x, -1.5f, newPos.z);
            child.localRotation = Quaternion.identity;
            child.localScale = new Vector3(1.25f, 1.25f, 1.25f);



            newPos = stackPosition.localPosition;
            newPos.y = 0f;
            newPos.x = 0;
            newPos.z -= 0.5f;


            
        }
    }
}
