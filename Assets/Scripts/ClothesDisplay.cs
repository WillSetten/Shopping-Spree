using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesDisplay : MonoBehaviour
{
  public GameObject[] clothesArray;
  public Vector3 start = new Vector3(0, 5, 0), end = new Vector3(2, 5, 0);

    // Start is called before the first frame update
    void Start()
    {
        clothesArray = Resources.LoadAll<GameObject>("Clothing_Pack/Prefabs");

        int i = 0;
        foreach(GameObject o in clothesArray) {
          Debug.Log(o);
          Instantiate(o, new Vector3( i, 0, 0), Quaternion.identity);
          i += 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
