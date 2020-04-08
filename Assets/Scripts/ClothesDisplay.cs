using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesDisplay : MonoBehaviour
{
    public GameObject[] clothesArray;
    public Transform clothesStorage;
    private MouseLook mouse;
    private bool scrolling;
    public float clothesSpacing;
    public List<Transform> clothesPositions;
    public float clothesLimit;

    // Start is called before the first frame update
    void Start()
    {
        mouse = FindObjectOfType<MouseLook>();
        scrolling = false;

        clothesArray = Resources.LoadAll<GameObject>("Clothing_Pack/Prefabs");
        clothesSpacing = 1.8f;

        int i = 0;
        while(i < clothesArray.Length){
          GameObject temp = Instantiate(clothesArray[i], new Vector3( 0, clothesArray[i].transform.position.y, 0), clothesStorage.rotation * clothesArray[i].transform.rotation);
          temp.transform.parent = clothesStorage;
          temp.transform.localPosition = new Vector3(0, clothesArray[i].transform.position.y, ((float)i*clothesSpacing) + clothesArray[i].transform.position.z);
          clothesPositions.Add(temp.transform);
          i += 1;
        }
        clothesLimit = (float)i*clothesSpacing;
        foreach (Transform t in clothesPositions)
        {
            t.GetComponentInChildren<Canvas>().gameObject.SetActive(false);
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