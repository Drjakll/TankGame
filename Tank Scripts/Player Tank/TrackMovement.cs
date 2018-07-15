using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMovement : WheelMovement {

	// Use this for initialization
	void Start () {
        base.Init();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            leftMouseDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            leftMouseDown = false;
        }
        if (Input.GetAxis("Horizontal") != 0 && !leftMouseDown)
        {
            trackAngle = trackAngle + Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            trackAngle = Mathf.Clamp(trackAngle, minTrackAngle, maxTrackAngle);
            transform.localEulerAngles = new Vector3(0, trackAngle, 0);
        }
        else
        {
            if (transform.localEulerAngles.y <= 10)
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
    }
}
