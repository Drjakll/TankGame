using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Not sure if it's used or not
/// </summary>
public class TextFaceCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(new Vector3(0, 180, 0), Space.Self);
	}
}
