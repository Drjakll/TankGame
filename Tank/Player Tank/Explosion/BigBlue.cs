using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This explosion will push GameObjects with ConstantForce component attached to it. It will push away from the center of the explosion.
/// </summary>
public class BigBlue : Explosion {

    // Use this for initialization
    private float strength; //Multiplyer of the based damage
    private float timer; //How much damage is done per half second
	void Start () {
        Init();
        transform.localScale *= 3.0f; //Want the explosion prefab to be 3x bigger than the original
        timer = 0.0f;
        externalForce();
	}
	
	// Update is called once per frame
	void Update () {
        countDown();
        timer += Time.deltaTime;
        if(timer > .5f)
        {
            dealDamage(minDamage, maxDamage);
            timer = 0.0f;
        }
    }

    //Apply force to objects within sphere radius away from center of the explosion
    public void externalForce()
    {
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, transform.localScale.z);
        foreach(Collider hitObj in hitObjects)
        {
            GameObject GO = hitObj.gameObject;
            ConstantForce CF = GO.GetComponent<ConstantForce>();
            if(CF != null)
            {
                transform.LookAt(GO.transform.position);
                Vector3 directionalForce;
                directionalForce = 20000.0f * transform.forward;
                directionalForce.y *= -1;
                CF.force = CF.force + directionalForce;
                float Multiplier = Mathf.Abs(transform.forward.z);
                dealDamage(minDamage * 10.0f * Multiplier, maxDamage * 10.0f * Multiplier);
                StartCoroutine(applyForce(CF, directionalForce));
            }
        }
    }

    //If in future someone wants to edit the damage of this explosion, use this method
    public void setStrength(float newStr)
    {
        strength = newStr;
        minDamage *= strength;
        maxDamage *= strength;
    }

    //Will apply force for .3 sec and then detect the explosion force
    private IEnumerator applyForce(ConstantForce constF, Vector3 DirForce)
    {
        yield return new WaitForSeconds(0.3f);
        if (constF != null)
        {
            constF.force = constF.force - DirForce;
        }
    }
}
