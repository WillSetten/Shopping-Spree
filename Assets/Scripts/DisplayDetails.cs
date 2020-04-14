using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDetails : MonoBehaviour
{
    public Text itemNameText, dateText, priceText, sizeText, colourText;
    private ClothesDetails parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.GetComponent<ClothesDetails>();
        itemNameText.text = parent.getName();
        dateText.text = parent.day + "/" + parent.month + "/" + parent.year;
        priceText.text = "£" + parent.getPrice().ToString();
        string tempSizeText = "";

        foreach (string size in parent.getSize()) {
            if (tempSizeText.Length == 0)
            {
                tempSizeText += size.Substring(0, 1);
            }
            else {
                tempSizeText += "/" + size.Substring(0, 1);
            }
        }

        sizeText.text = tempSizeText;
        colourText.text = parent.getColour();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
