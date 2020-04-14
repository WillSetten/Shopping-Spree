using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesDetails : MonoBehaviour
{
    public string itemName, day, month, year;
    public string colour;
    public string[] size;
    public float price;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getName() {
        return itemName;
    }

    public System.DateTime getDT() {
        return System.Convert.ToDateTime(day + "/" + month + "/" + year);
    }

    public string getColour() {
        return colour;
    }

    public string[] getSize() {
        return size;
    }

    public float getPrice() {
        return price;
    }
}
