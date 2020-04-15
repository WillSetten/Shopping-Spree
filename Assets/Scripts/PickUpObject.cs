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
    public float zOffset;
    public static GameObject player;
    ClothesDetails details;

    private void Awake()
    {
        if (!player) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        holdingObject = false;
        displayActive = false;
        details = this.GetComponent<ClothesDetails>();
        myDestination = GameObject.Find("GrabDestination").GetComponent<Transform>();
    }

    private void Update()
    {
        if (holdingObject)
        {
            this.transform.position = myDestination.position;
            this.transform.LookAt(transform.parent.parent.transform);

            //hacky fix for jeans and folded t-shirts
            if (details.itemType == "FoldedTshirt") {
                this.transform.Rotate(90, 0, 0);
                this.transform.Translate(0.6f, 0, 0.7f);
            }
            if (details.itemType == "Jeans")
            {
                this.transform.Rotate(-90, 180, 0);
                this.transform.Translate(-0.25f, 0.35f, 0);
            }
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
        transform.Find("Item Details").gameObject.SetActive(true);
        displayActive = true;
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
            if (c.gameObject.transform.parent.name == "Basket Frame" && !gameObject.name.Equals("Basket"))
            {
                nearBasket = true;
                break;
            }
        }
        //If the user has let go of the item near the basket, add it to the basket
        if (holdingObject && nearBasket)
        {
            //Debug.Log("Item in basket!");
            GameObject.Find("Basket").GetComponent<Basket>().addItem(this.GetComponent<ClothesDetails>());
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
