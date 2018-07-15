using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Intend to attach to a barrel of player's  tank.
/// </summary>
public class BarrelMovement : MonoBehaviour {
    protected bool leftMouseDown;
    protected float verticalRotation;
    protected float maxVerRot;
    protected float minVerRot;
    public float barrelSpeed;
    protected TextMesh TM; //Used for displaying the angle of the barrel
    // Use this for initialization
    void Start () {
        init();
	}
	
	// Update is called once per frame
	void Update () {
        movement();	
	}

    public void init()
    {
        barrelSpeed = 50.0f;
        leftMouseDown = false;
        maxVerRot = 25.0f;
        minVerRot = -65.0f;
        verticalRotation = transform.localEulerAngles.x;
        TM = GetComponentInChildren<TextMesh>();
    }

    public void movement()
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
            verticalRotation = verticalRotation - Input.GetAxis("Vertical") * barrelSpeed * Time.deltaTime;
            verticalRotation = Mathf.Clamp(verticalRotation, minVerRot, maxVerRot);
            transform.localEulerAngles = new Vector3(verticalRotation, 0, 0);
        }
        changeAngleDisplay();
    }

    //Text will rotate as the camera angle changes, it will always face the camera
    public void changeAngleDisplay()
    {
        TM.text = ((int)verticalRotation).ToString() + " Vertical";
        TM.gameObject.transform.LookAt(Camera.main.transform);
        TM.gameObject.transform.Rotate(new Vector3(0, 180, 0), Space.Self);
    }
}
