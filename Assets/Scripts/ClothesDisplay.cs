using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothesDisplay : MonoBehaviour
{
    public GameObject[] basicClothesArray, filteredClothesArray;
    public Transform clothesStorage;
    private MouseLook mouse;
    private bool scrolling;
    public float clothesSpacing;
    public List<Transform> clothesPositions;
    public List<GameObject> clothesList, displayList, tempList;
    public float clothesLimit;

    public Dropdown sortDropdown, sizeDropdown, colourDropdown;

    public string sortField = "Default";
    public string colourField = "Default";
    public string sizeField = "Default";
    public string searchField = "";

    // Start is called before the first frame update
    void Start()
    {
        mouse = FindObjectOfType<MouseLook>();
        scrolling = false;

        basicClothesArray = Resources.LoadAll<GameObject>("Clothing_Pack/Prefabs");
        clothesSpacing = 1.8f;

        //Sort the basic array to a default setting that can be filtered later
        //filteredClothesArray = SortArrayByDate(basicClothesArray, "Date - Newest to Oldest");

        int i = 0;
        while(i < basicClothesArray.Length){
            GameObject temp = Instantiate(basicClothesArray[i], new Vector3( 0, basicClothesArray[i].transform.position.y, 0), clothesStorage.rotation * basicClothesArray[i].transform.rotation);
            temp.transform.parent = clothesStorage;
            temp.transform.localPosition = new Vector3(0, basicClothesArray[i].transform.position.y, ((float)i*clothesSpacing) +  basicClothesArray[i].transform.position.z);
            temp.GetComponent<PickUpObject>().zOffset = basicClothesArray[i].transform.position.z;
            clothesList.Add(temp);
            clothesPositions.Add(temp.transform);
            i += 1;
        }
        clothesLimit = (float)i*clothesSpacing;
        foreach (Transform t in clothesPositions)
        {
            t.GetComponentInChildren<Canvas>().gameObject.SetActive(false);
        }
        SortRack();
    }

    //Called after all filtering and sorting is done
    public void UpdateRack()
    {
        //this block is essentially copied from your code above
        int i = 1;
        foreach (GameObject GO in clothesList)
        {
            //displayList is the filtered list and clothes list is the sorted list, so only show clothes that are in both lists
            if (displayList.IndexOf(GO) != -1)
            {
                //from line 40
                GO.transform.localPosition = new Vector3(0, GO.transform.localPosition.y, ((float)i * clothesSpacing) + GO.GetComponent<PickUpObject>().zOffset);
                //Debug.Log(basicClothesArray[i].name + "," + GO.name);
                i += 1;
            }
            else
            {
                //to turn off the filtered out clothes
                GO.SetActive(false);
            }
        }
        //the clothes limit needs to be updated if clothes were removed from the display list
        clothesLimit = (float)i * clothesSpacing;
    }

    //This is for filtering the colours and sizes of clothes
    //put into the displayList
    public void FilterRack()
    {
        colourField = colourDropdown.options[colourDropdown.value].text;
        sizeField = sizeDropdown.options[sizeDropdown.value].text;

        ClothesDetails details;
        foreach (GameObject GO in clothesList)
        {
            details = GO.GetComponent<ClothesDetails>();
            if (colourField == details.getColour() || colourField == "Default") {
                if (sizeField == "Default") {
                    displayList.Add(GO);
                }
                int i = 0;
                foreach (string s in details.getSize()) {
                    if (s == sizeField) {
                        i = 1;
                    }
                }
                if (i == 1) {
                    displayList.Add(GO);
                }
            }
            displayList.Add(GO);
        }
        UpdateRack();
    }

    //for permuting the clothesList
    public void SortRack()
    {
        tempList = new List<GameObject>();
        //Choice of sorting by date or price
        if (sortDropdown.options[sortDropdown.value].text.Substring(0, 4) == "Date")
        {
            SortArrayByDate(sortDropdown.options[sortDropdown.value].text);
        }
        else
        {
            SortArrayByPrice(sortDropdown.options[sortDropdown.value].text);
        }

        FilterRack();
    }

    private void SortArrayByDate(string str) {
        if (sortField != str) {
            sortField = str;
            ClothesDetails details;
            //finds the index where each item of clothing needs to go
            foreach (GameObject GO in clothesList) {
                details = GO.GetComponent<ClothesDetails>();
                //if list empty just add the first item
                if (tempList.Count == 0)
                {
                    tempList.Add(GO);
                }
                //if at any point can't find a lesser item in temp then return and add to the end of the list
                else if (FindObjDatePosition(details, str) == -1) {
                    tempList.Add(GO);
                }
                //Found a position where found a lesser or equal item, inserts it and shifts the  list along 1
                else {
                    int i = FindObjDatePosition(details, str);
                    tempList.Insert(i, GO);
                }
            }
            clothesList = new List<GameObject>(tempList);
        }
    }

    private int FindObjDatePosition(ClothesDetails objDetails, string str)
    {
        //tempList can only be as long or smaller than clothesList
        for (int i = 0; i < clothesList.Count; i++)
        {
            if (str == "Date - Oldest to Newest")
            {
                //checks if run out of space
                if (tempList.Count == i)
                {
                    return -1;
                }
                //compares the position's date against the current items date
                if (System.DateTime.Compare(objDetails.getDT(), tempList[i].GetComponent<ClothesDetails>().getDT()) <= 0)
                {
                    return i;
                }
            }
            if (str == "Date - Newest to Oldest")
            {
                if (tempList.Count == i)
                {
                    return -1;
                }
                if (System.DateTime.Compare(objDetails.getDT(), tempList[i].GetComponent<ClothesDetails>().getDT()) >= 0)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    private void SortArrayByPrice(string str)
    {
        if (sortField != str)
        {
            sortField = str;
            ClothesDetails details;
            foreach (GameObject GO in clothesList)
            {
                details = GO.GetComponent<ClothesDetails>();
                if (tempList.Count == 0)
                {
                    tempList.Add(GO);
                }
                else if (FindObjPricePosition(details, str) == -1)
                {
                    tempList.Add(GO);
                }
                else
                {
                    int i = FindObjDatePosition(details, str);
                    tempList.Insert(i, GO);
                }
            }
            clothesList = new List<GameObject>(tempList);
        }
    }

    //find where the clothing belongs in an array based on price
    private int FindObjPricePosition(ClothesDetails objDetails, string str)
    {
        for (int i = 0; i < clothesList.Count; i++)
        {
            if (str == "Price - Highest to Lowest")
            {
                if (tempList.Count == i)
                {
                    return -1;
                }
                if (objDetails.getPrice() >= tempList[i].GetComponent<ClothesDetails>().getPrice())
                {
                    return i;
                }
            }
            if (str == "Price - Lowest to Highest")
            {
                if (tempList.Count == i)
                {
                    return -1;
                }
                if (objDetails.getPrice() <= tempList[i].GetComponent<ClothesDetails>().getPrice())
                {
                    return i;
                }
            }
        }
        return -1;
    }

    private void OnMouseDown() {
        scrolling = true;
    }

    private void OnMouseUp() {
        scrolling = false;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform t in clothesPositions)
        {
            if (t.GetComponent<PickUpObject>().holdingObject)
            {
                continue;
            }
            //OPACITY
            //Set the opacity of this object to the appropriate value based on how far it is along the rack
            //0 if it is greater than 5.5 or lower than 0.5
            OpacityChanger objectOpacity = t.GetComponent<OpacityChanger>();
            if (t.localPosition.z > 11f || t.localPosition.z < 1f)
            {
                //Set opacity to 0
                objectOpacity.changeOpacity(0);
            }
            //If the object is in the middle units of the rack
            else if (t.localPosition.z > 2f && t.localPosition.z < 10f)
            {
                //Set opacity to 1
                objectOpacity.changeOpacity(1);
            }
            //Else fade object out depending on it's z
            else
            {
                if (t.localPosition.z > 1f && t.localPosition.z < 2f)
                {
                    objectOpacity.changeOpacity(t.localPosition.z-1);
                }
                else if (t.localPosition.z > 10f && t.localPosition.z < 11f)
                {
                    objectOpacity.changeOpacity(t.localPosition.z);
                }
            }

            //POSITIONING
            if (scrolling)
            {
                //If position of this piece of clothing is too far to the right, teleport item to the left side of the rack
                if (t.localPosition.z < 0)
                {
                    //Debug.Log(t.localPosition.z);
                    //Debug.Log("Teleporting " + t.gameObject.name + " to the left side of the rack");
                    t.localPosition = new Vector3(0, t.localPosition.y, clothesLimit+t.localPosition.z);
                }
                //If the position of this piece of clothing is too far to the left, teleport item to the right side of the rack
                else if (t.localPosition.z > clothesLimit)
                {
                    //Debug.Log(t.localPosition.z);
                    //Debug.Log("Teleporting " + t.gameObject.name + " to the right side of the rack");
                    t.localPosition = new Vector3(0, t.localPosition.y, t.localPosition.z-clothesLimit);
                }
                t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, t.localPosition.z + Input.GetAxis("Mouse X") * mouse.mouseSensitivity / 5 * Time.deltaTime);
            }
        }
    }
}