using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    public List<BasketEntry> basketEntries;
    public GameObject basketEntry;
    public Transform entryContainer;
    public Text totalCost;
    public GameObject basketMenu;
    public GameObject player;

    private void Awake()
    {
        basketEntries = new List<BasketEntry>();
    }
    private void OnMouseDown()
    {
        
    }
    private void OnMouseUp()
    {
        
    }

    private void Update()
    {
        updateTotalCost();
        basketMenu.transform.LookAt(player.transform);
        basketMenu.transform.Rotate(new Vector3(0, 180, 0));
    }

    public void addItem(ClothesDetails newItem)
    {
        Debug.Log("Adding Item named " + newItem.getName());
        //Check if this item is already in the basket
        foreach (BasketEntry b in basketEntries)
        {
            if (b.isTheSameAs(newItem))
            {
                b.increaseQuantity();
                return;
            }
        }
        GameObject newEntry = Instantiate(basketEntry,new Vector3(0,0,0),Quaternion.Euler(0,0,0),entryContainer);
        Debug.Log(basketEntries.Count);
        newEntry.transform.localPosition = new Vector3(2,75+(-30 * basketEntries.Count), 0);
        newEntry.GetComponent<BasketEntry>().importDetails(newItem);
        basketEntries.Add(newEntry.GetComponent<BasketEntry>());
    }
    public void removeEntry(BasketEntry entry)
    {
        int entryNo = basketEntries.IndexOf(entry);
        basketEntries.Remove(entry);
        if (entryNo != basketEntries.Count - 1) {
            foreach (BasketEntry b in basketEntries.GetRange(entryNo, basketEntries.Count - entryNo))
            {
                b.transform.localPosition = new Vector3(b.transform.localPosition.x, b.transform.localPosition.y + 30, b.transform.localPosition.z);
            }
        }
        Destroy(entry.gameObject);
    }

    public void updateTotalCost()
    {
        float cost = 0;
        foreach (BasketEntry b in basketEntries)
        {
            cost = cost + b.getTotalCost();
        }
        totalCost.text = cost.ToString();
    }
}
