using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RackMovement : MonoBehaviour
{
    public Transform rail;
    //True if the rack is being rotated along the rail
    public bool rotating;
    //True if the rack is being shifted forward or back
    public bool shifting;
    void Start()
    {
        rail = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotating)
        {
            rail.Rotate(new Vector3(0, Input.GetAxis("Mouse X")*Time.deltaTime*100, 0), Space.Self);
        }
        else if (shifting)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + Input.GetAxis("Mouse Y")* 3 *Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
        }
    }

    public void moveRack(string mode)
    {
        if (mode == "Rotating")
        {
            rotating = true;
        }
        else if (mode == "Shifting")
        {
            shifting = true;
        }
    }
    public void stopRack()
    {
        rotating = false;
        shifting = false;
    }
}
