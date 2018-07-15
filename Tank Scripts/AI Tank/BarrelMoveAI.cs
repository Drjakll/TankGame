using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI side of the barrel movement. Meant to attach to the barrel of a tank.
/// </summary>
public class BarrelMoveAI : MonoBehaviour {

    protected float currentAngle;
    protected float maxAngle;
    protected float minAngle;
    protected float verRotDir;
    protected float rotSpeed;
	// Use this for initialization
	void Start () {
        currentAngle = 0;
        maxAngle = 25.0f;
        minAngle = -65.0f;
        verRotDir = 0.0f;
        rotSpeed = 50.0f;
	}
	
	// Update is called once per frame
	void Update () {
        currentAngle = currentAngle + Time.deltaTime * verRotDir * rotSpeed;
        currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);
        transform.localEulerAngles = new Vector3(currentAngle, 0, 0);
	}

    public void rotate(float rotation)
    {
        if(rotation != 0)
        {
            verRotDir = rotation / Mathf.Abs(rotation);
        }
        else
        {
            verRotDir = 0.0f;
        }
    }

    public float getCurrentAngle()
    {
        return currentAngle;
    }
}
