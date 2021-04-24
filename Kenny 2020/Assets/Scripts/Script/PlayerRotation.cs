using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{

    Ray cameraRay;                // The ray that is cast from the camera to the mouse position
    RaycastHit cameraRayHit;    // The object that the ray hits
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FaceMouse();
    }

    void FaceMouse()
    {
        // Cast a ray from the camera to the mouse cursor
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // If the ray strikes an object...
        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {
            // ...and if that object is the ground...
           // if (cameraRayHit.transform.tag == "Ground")
           // {
                // ...make the cube rotate (only on the Y axis) to face the ray hit's position 
                Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                transform.LookAt(targetPosition);
           // }
        }

    }

}
