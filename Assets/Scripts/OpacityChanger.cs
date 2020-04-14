using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityChanger : MonoBehaviour
{
    public Renderer[] meshes;

    public void changeOpacity(float opacity)
    {
        foreach (Renderer r in meshes)
        {
            if (opacity == 0)
            {
                r.enabled = false;
                GetComponent<BoxCollider>().enabled = false;
                transform.Find("Item Details").gameObject.SetActive(false);
            }
            else
            {
                if (opacity!=1)
                {
                    //Debug.Log(opacity);
                }
                r.enabled = true;
                GetComponent<BoxCollider>().enabled = true;
                foreach (Material mat in r.materials)
                {
                    Color color = mat.color;
                    color.a = opacity;
                    mat.color = color;
                }
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            changeOpacity(0);
        }
    }
}
