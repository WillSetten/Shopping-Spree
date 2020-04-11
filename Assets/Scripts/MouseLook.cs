using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity;
    public Transform playerBody;
    float xRotation = 0f;
    public float distanceToSee;
    public 

    // Start is called before the first frame update
    void Start()
    {
        // Lockdown mouse pointer to centre of the screen, but GUI interaction needs to be updated
        Cursor.lockState = CursorLockMode.Locked;
        // Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);

        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        Debug.DrawRay(this.transform.position, this.transform.forward * distanceToSee, Color.magenta);
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
        if(Physics.Raycast(ray, out hit)) {
            if(hit.collider != null) {
                Debug.Log(hit.collider.gameObject.name);
            }
        }
    }


}
