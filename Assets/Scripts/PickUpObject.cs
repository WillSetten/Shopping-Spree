using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public Transform myDestination;
    public Transform previousParent;
    public bool holdingObject;
    Vector3 oldLocalPosition;
    Quaternion oldRotation;
    bool displayActive;

    private void Awake()
    {
        holdingObject = false;
        displayActive = false;
        myDestination = GameObject.Find("GrabDestination").GetComponent<Transform>();
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
        if (Input.GetMouseButtonDown(1))
        {
            if (!displayActive)
            {
                transform.Find("Item Details").gameObject.SetActive(true);
                displayActive = true;
            }
            else
            {
                transform.Find("Item Details").gameObject.SetActive(false);
                displayActive = false;
            }
        }
    }

    public void toggleDisplay()
    {
        if (!displayActive)
        {
            transform.Find("Item Details").gameObject.SetActive(true);
            displayActive = true;
        }
        else
        {
            transform.Find("Item Details").gameObject.SetActive(false);
            displayActive = false;
        }
    }

    private void OnMouseDown()
    {
        if (GetComponent<Rigidbody>()) {
            GetComponent<Rigidbody>().useGravity = false;
        }
        oldLocalPosition = transform.localPosition;
        if (transform.parent != null)
        {
            previousParent = transform.parent;
        }
        oldRotation = this.transform.localRotation;
        this.transform.position = myDestination.position;
        this.transform.parent = myDestination.transform;
        holdingObject = true;
        if (!displayActive)
        {
            toggleDisplay();
        }
    }
    private void OnMouseUp()
    {
        bool nearBasket = false;
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 1);
        foreach (Collider c in hitColliders)
        {
            if (c.gameObject.transform.parent==null)
            {
                continue;
            }
            if (c.gameObject.transform.parent.name == "Basket Frame")
            {
                nearBasket = true;
                break;
            }
        }
        //If the user has let go of the item near the basket, add it to the basket
        if (holdingObject && nearBasket)
        {
            Debug.Log("Item in basket!");
            restoreObjectLocation();
        }
        //If the user has just let go of the item
        else if (holdingObject) {
            restoreObjectLocation();
        }
    }

    private void restoreObjectLocation()
    {
        this.transform.parent = previousParent;
        this.transform.localRotation = oldRotation;
        transform.localPosition = oldLocalPosition;
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
        holdingObject = false;
    }
}
