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
          temp.transform.localPosition = new Vector3(0, clothesArray[i].transform.position.y,(float)i/clothesSpacing);
          i += 1;
        }
    }

    private void OnMouseDown() {
        Debug.Log("MOUSEDOWN");
        scrolling = true;
    }

    private void OnMouseUp() {
        scrolling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(scrolling) {
            Debug.Log("MOUSESCROLLING");
            clothesStorage.localPosition = new Vector3(clothesStorage.localPosition.x, clothesStorage.localPosition.y , clothesStorage.localPosition.z - Input.GetAxis("Mouse X")* mouse.mouseSensitivity/10 *Time.deltaTime);
        }
    }
}