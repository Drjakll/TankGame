using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This meant to attach to an AI tank as to move forward, backward, turn left, turn right. Will be controlled by TankControllerAI class. Meant to attach to AItank GameObject.
/// </summary>
public class MovementAI : MonoBehaviour
{
    protected float verticalSpeed;
    protected float rotationSpeed;
    protected WheelsAI[] wheels;
    protected float verDir; //Direction of the vertical movement, -1 or 1
    protected float horDir; //Direction of the rotation movement, -1 or 1

    // Use this for initialization
    void Start()
    {
        wheels = gameObject.GetComponentsInChildren<WheelsAI>();
        verticalSpeed = 5.0f;
        rotationSpeed = 30.0f;
        horDir = 0;
        verDir = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (verDir != 0 && wheels[0].isStickToGround())
        {
            transform.Translate(new Vector3(0, 0, verDir * verticalSpeed * Time.deltaTime), Space.Self);
            if (horDir != 0)
            {
                transform.Rotate(new Vector3(0, horDir * rotationSpeed * Time.deltaTime, 0), Space.Self);
            }
        }
    }

    public void moveVertical(float dir)
    {
        if(dir != 0)
        {
            verDir = dir / Mathf.Abs(dir);
        }
        else
        {
            verDir = 0.0f;
        }
    }

    public void moveHorizontal( float dir)
    {
        if(dir != 0)
        {
            horDir = dir / Mathf.Abs(dir);
        }
        else
        {
            horDir = 0.0f;
        }
    }

    public float getVerticalDir()
    {
        return verDir;
    }

    public float getHorizontalDir()
    {
        return horDir;
    }
}
