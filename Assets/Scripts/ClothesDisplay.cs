using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesDisplay : MonoBehaviour
{
    public GameObject[] basicClothesArray, filteredClothesArray;
    public Transform clothesStorage;
    private MouseLook mouse;
    private bool scrolling;
    public float clothesSpacing;
    public List<Transform> clothesPositions;
    public float clothesLimit;

    public string dateField = "Default";
    public string priceField = "Default";
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
        basicClothesArray = SortArrayByDate(basicClothesArray);

        int i = 0;
        while(i < basicClothesArray.Length){
          GameObject temp = Instantiate(basicClothesArray[i], new Vector3( 0, basicClothesArray[i].transform.position.y, 0), clothesStorage.rotation * basicClothesArray[i].transform.rotation);
          temp.transform.parent = clothesStorage;
          temp.transform.localPosition = new Vector3(0, basicClothesArray[i].transform.position.y, -((float)i*clothesSpacing) + basicClothesArray[i].transform.position.z);
          clothesPositions.Add(temp.transform);
          i += 1;
        }
        clothesLimit = (float)i*clothesSpacing;
        foreach (Transform t in clothesPositions)
        {
            t.GetComponentInChildren<Canvas>().gameObject.SetActive(false);
        }
    }

    private GameObject[] SortArrayByDate(GameObject[] arr) {
        GameObject[] tempArr = new GameObject[arr.Length];
        if (dateField == "Default") {
            dateField = "Newest to Oldest";
            foreach (GameObject obj in arr) {
                ClothesDetails objDetails = obj.GetComponent<ClothesDetails>();
                //find where the obj belongs in the array and insert
                InsertObjIntoArray(obj, tempArr, FindObjDatePosition(objDetails, tempArr));
            }            
        }
        return tempArr;
    }

    //find where the clothing belongs in an array based on latest date
    private int FindObjDatePosition(ClothesDetails objDetails, GameObject[] arr) {
        for (int i = 0; i < arr.Length; i++) {
            //if empty space or the given date is earlier than the ith date then return that space position
            if (arr[i] == null ) {
                
                return i;
            }
            if (System.DateTime.Compare(objDetails.getDT(), arr[i].GetComponent<ClothesDetails>().getDT()) >= 0)
            {
                return i;
            }
        }

        return -1;
    }

    //insert obj into the given Object array "arr" in the nth position
    private void InsertObjIntoArray(GameObject obj, GameObject[] arr, int n) {
        if (arr[n] == null)
        {
            arr[n] = obj;
        }
        else
        {
            //shift all items n onwards to the right
            for (int i = arr.Length - 1; i > n; i--)
            {
                 arr[i] = arr[i - 1];
            }
            //insert the obj
            arr[n] = obj;
        }
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
                t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, t.localPosition.z - Input.GetAxis("Mouse X") * mouse.mouseSensitivity / 5 * Time.deltaTime);
            }
        }
    }
}