using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RackManager : MonoBehaviour
{
    public GameObject rail;
    public Transform player;
    public List<Transform> racks;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.position.x, -2, player.position.z);
        //disabled for now because of text input when searching
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    addRack();
        //}
    }

    //Adds a new rack where the player is facing
    public void addRack(){
        Vector3 rotation = player.rotation.eulerAngles;
        rotation = new Vector3(rotation.x, rotation.y-90, rotation.z);

        racks.Add(Instantiate(rail, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(rotation)).GetComponent<Transform>());
    }

    //Deletes a given rack from the space
    public void deleteRack(Transform rack)
    {
        rack.gameObject.SetActive(false);
        racks.Remove(rack);
    }
}
