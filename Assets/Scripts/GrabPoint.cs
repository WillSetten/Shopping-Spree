using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPoint : MonoBehaviour
{
    public RackMovement rack;
    public string mode;

    private void OnMouseDown()
    {
        rack.moveRack(mode);
    }

    private void OnMouseUp()
    {
        rack.stopRack();
    }
}
