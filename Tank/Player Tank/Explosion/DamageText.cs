using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Intend to attach to a pop up text, text will destroy itself after moving more than 4 unit distance from its original location
/// </summary>
public class DamageText : MonoBehaviour {
    private float height;
	// Use this for initialization
	void Start () {
        height = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        height += .04f;
        if(height < 4.0f)
        {
            transform.Translate(0, .04f, 0, Space.World);
            transform.LookAt(Camera.main.transform);
            transform.Rotate(new Vector3(0, 180, 0), Space.World);
            transform.localScale += new Vector3(.01f, .01f, .01f);
        }
        else
        {
            Object.Destroy(this.gameObject);
        }
	}
}
