using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesDisplay : MonoBehaviour
{
  public GameObject[] clothesArray;
  public Transform clothesStorage;
  public float clothesSpacing;
  public Vector3 start = new Vector3(0, 5, 0), end = new Vector3(2, 5, 0);

    // Start is called before the first frame update
    void Start()
    {
        clothesArray = Resources.LoadAll<GameObject>("Clothing_Pack/Prefabs");
        clothesSpacing = 1.1f;
        int i = 0;
        while(i<=9*clothesSpacing){
          GameObject temp = Instantiate(clothesArray[i], new Vector3( 0, clothesArray[i].transform.position.y, 0), clothesStorage.rotation * clothesArray[i].transform.rotation);
          temp.transform.parent = clothesStorage;
          temp.transform.localPosition = new Vector3(0, clothesArray[i].transform.position.y,(float)i/clothesSpacing);
          i += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            clothesStorage.transform.localPosition = new Vector3(clothesStorage.transform.localPosition.x, clothesStorage.transform.localPosition.y, clothesStorage.transform.localPosition.z+ 2*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            clothesStorage.transform.localPosition = new Vector3(clothesStorage.transform.localPosition.x, clothesStorage.transform.localPosition.y, clothesStorage.transform.localPosition.z - 2*Time.deltaTime);
        }
    }
}
