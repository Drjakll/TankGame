using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Use to fire ammo within the direction of the barrel. Should create an empty GameObject infront of the tank barrel and attach it to the empty GameObject.
/// </summary>
public class FireAI : MonoBehaviour {
    private Vector3 Force;
    private GimTrim Anker; //Needed to help determine the angle of the barrel. It's located between the turrent body and the barrel.
    private GameObject ToBeFire;
    public GameObject Ammo;
	// Use this for initialization
	void Start () {
        Anker = GetComponentInParent<TurrentMoveAI>().GetComponentInChildren<GimTrim>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fire(float speed)
    {
        ToBeFire = (GameObject)Instantiate(Ammo);
        ToBeFire.transform.position = this.gameObject.transform.position;
        ToBeFire.transform.eulerAngles = transform.eulerAngles;
        float magnitudeForce = speed * ToBeFire.GetComponent<Rigidbody>().mass / .09f;
        ConstantForce CF = ToBeFire.GetComponent<ConstantForce>();
        Vector3 AimDirection = (transform.position - Anker.transform.position).normalized;
        CF.force = magnitudeForce * AimDirection;
        StartCoroutine(applyForce(ToBeFire));
    }

    protected IEnumerator applyForce(GameObject toBeFire)
    {
        yield return new WaitForSeconds(.07f);
        toBeFire.GetComponent<ConstantForce>().force = Vector3.zero;
    }
}
