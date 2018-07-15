using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Intend for attaching to an ammo prefab. Once an ammo hits a collider, it will then generate this explosion graphic effect. It has other sub classes inherit this class.
/// </summary>
public class Explosion : MonoBehaviour {
    // Use this for initialization
    protected float shrinkSpeed;
    protected float maxDamage;
    protected float minDamage;
    protected float shrink;
    public GameObject floatingText;

	void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
        countDown();
	}

    protected void Init()
    {
        shrinkSpeed = 0.01f;
        maxDamage = 20.0f;
        minDamage = 16.0f;
    }

    //Explosion will deal damage in a sphere radius, it only look for GameObjects with TankBaseStats attached to it and deducts health from it
    public void dealDamage(float minDmg, float maxDmg)
    {
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, transform.localScale.z);
        foreach (Collider hitObj in hitObjects)
        {
            TankBaseStats tank = hitObj.gameObject.GetComponentInChildren<TankBaseStats>();
            if (tank != null)
            {
                float damage = Random.Range(minDmg, maxDmg);
                tank.RecieveDamage(damage);
                GameObject dmgText = (GameObject)Instantiate(floatingText);
                dmgText.transform.position = tank.transform.position + new Vector3(0, tank.transform.localScale.y * 2, 0);
                dmgText.GetComponent<TextMesh>().text = ((int)tank.recentDmgRecieved).ToString();
            }
        }
    }

    //Explosion will shrink over time
    protected void countDown()
    {
        shrink = shrink + shrinkSpeed * Time.deltaTime;
        transform.localScale = transform.localScale - new Vector3(shrink, shrink, shrink);
        if (transform.localScale.x < 0 || transform.localScale.y < 0 || transform.localScale.z < 0)
        {
            GameObject.Find("GrassField").GetComponent<Referee>().nextTankTurn();
            transform.localScale = Vector3.zero;
            Object.Destroy(this.gameObject);
        }
    }
}
