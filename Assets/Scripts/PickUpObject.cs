using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public Transform myDestination;
    public Transform previousParent;
    bool holdingObject;

    private void Start()
    {
        holdingObject = false;
    }

    private void Update()
    {
        if (holdingObject)
        {
            this.transform.position = myDestination.position;
        }
    }

    void OnMouseDown()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        if (transform.parent!=null)
        {
            previousParent = transform.parent;
        }
        this.transform.position = myDestination.position;
        this.transform.parent = myDestination.transform;
        holdingObject = true;
    }

    private void OnMouseUp()
    {
        this.transform.parent = previousParent;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;
        holdingObject = false;

    }
}
