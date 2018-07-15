using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Identifier : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public abstract byte Identify(byte b, GameObject self);
    public abstract void Reaction(GameObject self);
}
