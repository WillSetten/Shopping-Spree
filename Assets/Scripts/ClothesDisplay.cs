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
        clothesSpacing = 1.1f;

        int i = 0;
        while(i < clothesArray.Length){
          GameObject temp = Instantiate(clothesArray[i], new Vector3( 0, clothesArray[i].transform.position.y, 0), clothesStorage.rotation * clothesArray[i].transform.rotation);
          temp.transform.parent = clothesStorage;
          temp.transform.localPosition = new Vector3(0, clothesArray[i].transform.position.y, (float)i/clothesSpacing);
          clothesPositions.Add(temp.transform);
          i += 1;
        }
        clothesLimit = (float)i/clothesSpacing;
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
        if (scrolling)
        {
            foreach (Transform t in clothesPositions)
            {
                //If position of this piece of clothing is too far to the right, teleport item to the left side of the rack
                if (t.localPosition.z < 0)
                {
                    Debug.Log(t.localPosition.z);
                    Debug.Log("Teleporting " + t.gameObject.name + " to the left side of the rack");
                    t.localPosition = new Vector3(0, t.localPosition.y, clothesLimit + 0.4f);
                }
                //If the position of this piece of clothing is too far to the left, teleport item to the right side of the rack
                else if (t.localPosition.z > clothesLimit + 0.5f)
                {
                    Debug.Log(t.localPosition.z);
                    Debug.Log("Teleporting " + t.gameObject.name + " to the right side of the rack");
                    t.localPosition = new Vector3(0, t.localPosition.y, 0);
                }
                else
                {
                    t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, t.localPosition.z - Input.GetAxis("Mouse X") * mouse.mouseSensitivity / 10 * Time.deltaTime);
                }
            }
        }
    }
}