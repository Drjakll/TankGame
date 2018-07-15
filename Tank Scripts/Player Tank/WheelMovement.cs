using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMovement : MonoBehaviour {

    public float rotationSpeed;
    protected float trackAngle;
    protected float minTrackAngle = -2.0f;
    protected float maxTrackAngle = 2.0f;
    protected float wheelRotateSpeed = 120.0f;
    protected bool leftMouseDown = false;
    protected Transform[] wheelTransform;
    // Use this for initialization
    void Start () {
        Init();
    }
	
	// Update is called once per frame
	void Update () {
        rotateWheel();
    }

    public void Init()
    {
        rotationSpeed = 60.0f; //Track rotation speed
        wheelRotateSpeed = 120.0f;
        trackAngle = transform.localEulerAngles.y;
        minTrackAngle = -2.0f;
        maxTrackAngle = 2.0f;
        wheelTransform = gameObject.GetComponentsInChildren<Transform>();
    }

    public void rotateWheel()
    {
        float vertical = Input.GetAxis("Vertical") * Time.deltaTime * wheelRotateSpeed;
        
        if (Input.GetMouseButtonDown(0))
        {
            leftMouseDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            leftMouseDown = false;
        }

        if(Input.GetAxis("Horizontal") != 0 && !leftMouseDown)
        {
            trackAngle = trackAngle + Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            trackAngle = Mathf.Clamp(trackAngle, minTrackAngle, maxTrackAngle);
            transform.localEulerAngles = new Vector3(0, trackAngle, 0);
        }
        else
        {
            if(transform.localEulerAngles.y <= 10)
            {
                transform.Rotate(0, -Time.deltaTime * transform.localEulerAngles.y, 0);
                trackAngle = trackAngle - Time.deltaTime * transform.localEulerAngles.y;
            }
            else
            {
                transform.Rotate(0, -Time.deltaTime * (transform.localEulerAngles.y - 360.0f), 0);
                trackAngle = trackAngle - Time.deltaTime * (transform.localEulerAngles.y - 360.0f);
            }
        }

        if (vertical != 0 && !leftMouseDown)
        {
            for(int i = 1; i < wheelTransform.Length; i++) 
            {
                wheelTransform[i].Rotate(vertical, 0, 0);
            }
        }
    }
}
