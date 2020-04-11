using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    private Canvas canvas;
    private GameObject myCamera;

    // Start is called before the first frame update
    void Start()
    {
        canvas = this.GetComponentInChildren<Canvas>();
        myCamera = GameObject.Find("Main Camera");
        canvas.worldCamera = myCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonClick() {
        Debug.Log("BUTTON CLICKED");
    }
}
