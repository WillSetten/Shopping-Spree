using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketEntry : MonoBehaviour
{
    public Text ItemName;
    public Text ItemColour;
    public Text ItemSize;
    public Text ItemQuantity;
    int quantityValue;
    float price;
    public Text ItemCost;

    private void Awake()
    {
        quantityValue = 1;
    }

    public void importDetails(ClothesDetails details)
    {
        ItemName.text = details.getName();
        ItemColour.text = details.getColour();
        if (details.gameObject.transform.parent.gameObject.GetComponentInParent<ClothesDisplay>() != null)
        {
            ItemSize.text = details.gameObject.transform.parent.gameObject.GetComponentInParent<ClothesDisplay>().sizeField;
            if (details.gameObject.transform.parent.gameObject.GetComponentInParent<ClothesDisplay>().sizeField == "Default")
            {
                ItemSize.text = "Medium";
            }
        }
        else
        {
            ItemSize.text = "Medium";
        }
        ItemQuantity.text = quantityValue.ToString();
        price = details.getPrice();
        ItemCost.text = (price).ToString();
    }
    
    public void increaseQuantity()
    {
        quantityValue = quantityValue + 1;
        updateQuantity();
    }
    public void decreaseQuantity()
    {
        if (quantityValue>1)
        {
            quantityValue = quantityValue - 1;
            updateQuantity();
        }
        else
        {
            GetComponentInParent<Basket>().removeEntry(this);
        }
    }

    public void updateQuantity()
    {
        ItemQuantity.text = quantityValue.ToString();
        ItemCost.text = (price * quantityValue).ToString();
    }

    public bool isTheSameAs(ClothesDetails newItem)
    {
        if (!ItemName.text.Equals(newItem.getName()))
        {
            Debug.Log("new Item" + newItem.name + " has a different name to " + this.name);
            return false;
        }
        if (price!=newItem.getPrice())
        {
            Debug.Log("new Item" + newItem.name + " has a different price to " + this.name);
            return false;
        }
        if (!ItemColour.text.Equals(newItem.getColour()))
        {
            Debug.Log("new Item" + newItem.name + " has a different colour to " + this.name);
            return false;
        }
        string size = "Medium";
        if (newItem.gameObject.transform.parent.gameObject.GetComponentInParent<ClothesDisplay>()!=null)
        {
            size = newItem.gameObject.transform.parent.gameObject.GetComponentInParent<ClothesDisplay>().sizeField;
            if (size == "Default")
            {
                size = "Medium";
            }
        }
        if (!ItemSize.text.Equals(size))
        {
            Debug.Log("new Item" + newItem.name + " has a different size to " + this.name);
            return false;
        }
        Debug.Log("new Item" + newItem.name + " is identical to " + this.name);
        return true;
    }

    public float getTotalCost()
    {
        return quantityValue * price;
    }
    public int getQuantity()
    {
        return quantityValue;
    }
}
