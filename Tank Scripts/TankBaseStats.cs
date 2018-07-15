using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Intend to attach to a plane as a representation of a tank's health. Plane should be really small and rotated 90 degrees around the X axis.
/// </summary>
public class TankBaseStats : MonoBehaviour {

    public float Health = 1000.0f;
    public float Armor = 0.25f;
    public float recentDmgRecieved;
    private float originalScaleOfX; //Needed the original X scale of the HealthBar for length of the health bar calculation 

	void Start () {
        Health = 1000.0f;
        Armor = 0.25f;
        originalScaleOfX = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform); //Will always look at the camera
        transform.Rotate(-90.0f, 0, 0); //Then rotate -90 degrees so that the visible side is showing
	}

    public void RecieveDamage(float Damage)
    {
        recentDmgRecieved = Damage * (1.0f - Armor);
        Health = Health - recentDmgRecieved;
        transform.localScale = new Vector3(originalScaleOfX * (Health / 1000.0f), transform.localScale.y, transform.localScale.z);
        if(Health <= 0.0f)
        {
            Object.Destroy(this.gameObject);
        }
    }
}
