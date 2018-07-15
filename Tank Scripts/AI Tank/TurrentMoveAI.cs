using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// AI tank's turrent movement. Should attach to the turrent of the AI tank.
/// </summary>
public class TurrentMoveAI : MonoBehaviour {
    
    protected float rotDir;
    protected float rotSpeed;
	// Use this for initialization
	void Start () {
        rotDir = 0.0f;
        rotSpeed = 50.0f;
	}
	
	// Update is called once per frame
	void Update () {
        float delta = Time.deltaTime * rotSpeed * rotDir;
        transform.Rotate(new Vector3(0, 0, delta), Space.Self);
	}

    public void rotate(float Direction)
    {
        if(Direction != 0)
        {
            rotDir = Direction / Mathf.Abs(Direction);
        }
        else
        {
            rotDir = 0.0f;
        }
    }
}
