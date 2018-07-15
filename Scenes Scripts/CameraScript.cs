using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Intend to attach to the main camera
/// </summary>
public class CameraScript : MonoBehaviour {
    private bool rightMouseDown = false;
    private bool middleMouseDown = false;
    public float speed = 100.0f;
    public float maxLimit = 45.0f; //Vertical rotation limit
    public float minLimit = -45.0f;
    public float zoomSpeed = 600.0f;
    public float originalHeight;
    public Vector3 rotationDelta;
    public GameObject parentObject; //Camera needs to chain and cannot travel more than 15 units radius away from the parent GameObject

	void Start () {
        speed = 100.0f;
        zoomSpeed = 600.0f;
        rotationDelta.x = transform.localEulerAngles.y;
        rotationDelta.y = transform.localEulerAngles.x;
        rotationDelta.z = 0;
        originalHeight = transform.position.y;
        if(transform.parent != null)
        {
            parentObject = transform.parent.gameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movementDelta;
        movementDelta = Vector3.zero;
        if (Input.GetMouseButtonDown(1))
        {
            rightMouseDown = true;
        }
        else if(Input.GetMouseButtonUp(1))
        {
            rightMouseDown = false;
        }

        if (Input.GetMouseButtonDown(2))
        {
            middleMouseDown = true;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            middleMouseDown = false;
        }

        if (rightMouseDown)
        {
            rotationDelta.y = rotationDelta.y - Time.deltaTime * speed * Input.GetAxis("Mouse Y");
            rotationDelta.y = Mathf.Clamp(rotationDelta.y, minLimit, maxLimit);
            rotationDelta.x = rotationDelta.x + Time.deltaTime * speed * Input.GetAxis("Mouse X");
            transform.localEulerAngles = new Vector3(rotationDelta.y, rotationDelta.x, 0);
        }

        if (middleMouseDown)
        {
            movementDelta.x = -Input.GetAxis("Mouse X");
        }

        movementDelta.z = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
        transform.Translate(movementDelta, Space.Self);
        if((transform.position - parentObject.transform.position).magnitude > 15.0f)
        {
            transform.Translate(-movementDelta, Space.Self);
        }

        if (transform.position.y != originalHeight)
        {
            transform.Translate(0, originalHeight - transform.position.y, 0, Space.Self);
        }
    }
}
