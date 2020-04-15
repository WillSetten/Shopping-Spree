using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBasket : MonoBehaviour
{
    public Transform myDestination;
    public Transform previousParent;
    public bool holdingObject;

    Vector3 positionStart, positionDiff;

    private void Awake()
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
    private void OnMouseOver()
    {

    }

    private void OnMouseDown()
    {
        if (transform.parent != null)
        {
            previousParent = transform.parent;
        }
        this.transform.position = myDestination.position;
        this.transform.parent = myDestination.transform;
        holdingObject = true;
    }
    private void OnMouseUp()
    {
        if (holdingObject)
        {
            dropBasket();
        }
    }

    private void dropBasket()
    {
        this.transform.parent = previousParent; 
        holdingObject = false;
        this.transform.position = new Vector3(this.transform.position.x, 2.05f, this.transform.position.z);
    }
}
