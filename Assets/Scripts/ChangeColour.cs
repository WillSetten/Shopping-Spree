using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{

    // make a public material as an array
    public Material[] material;

    // make public or private renderer
    public Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bluebox")
        {
            rend.sharedMaterial = material[1];

        }

        if (collision.gameObject.tag == "redbox")
        {
            rend.sharedMaterial = material[2];

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
