using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Intend to attach to the GameObject that contain a group of wheels of AI's tank
/// </summary>
public class WheelsAI : MonoBehaviour {

    public float wheelRotateSpeed;
    protected MovementAI moveAI;
    protected bool stickToGround;
	// Use this for initialization
	void Start () {
        wheelRotateSpeed = 120.0f;
        moveAI = GetComponentInParent<MovementAI>();
        stickToGround = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (moveAI.getVerticalDir() != 0)
        {
            float move = moveAI.getVerticalDir() * wheelRotateSpeed * Time.deltaTime;
            Transform[] wheels = GetComponentsInChildren<Transform>();
            for(int i = 1; i < wheels.Length; i++)
            {
                wheels[i].Rotate(new Vector3(move, 0, 0), Space.Self);
            }
            if (transform.position.y > 1.3)
            {
                stickToGround = false;
            }
            else
            {
                stickToGround = true;
            }
        }
	}

    public bool isStickToGround()
    {
        return stickToGround;
    }
}
