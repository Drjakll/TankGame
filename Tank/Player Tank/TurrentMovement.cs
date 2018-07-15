using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Intend to attach to player's tank's turrent.
/// </summary>
public class TurrentMovement : MonoBehaviour {
    public float rotationSpeed;
    protected bool leftMouseDown;
    protected float currentAngle;
    protected TextMesh TM; //Used to display the turrent's current angle
    // Use this for initialization
    void Start () {
        Init();
    }
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    public void Init()
    {
        rotationSpeed = 50.0f;
        leftMouseDown = false;
        currentAngle = 0.0f;
        TM = GetComponentInChildren<TextMesh>();
    }

    public void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            leftMouseDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            leftMouseDown = false;
        }
        if (leftMouseDown)
        {
            float horizontalRotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            currentAngle = currentAngle + horizontalRotation;
            transform.Rotate(0, 0, horizontalRotation, Space.Self);
        }
        changeAngleDisplay();
    }

    public void changeAngleDisplay()
    {
        currentAngle %= 360.0f;
        TM.text = ((int)currentAngle).ToString() + " Horizontal";
        TM.gameObject.transform.LookAt(Camera.main.transform); //Text will only face the camera
        TM.gameObject.transform.Rotate(new Vector3(0, 180, 0), Space.Self);
    }
}
