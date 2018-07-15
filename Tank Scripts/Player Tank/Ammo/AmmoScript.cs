using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Intend to attach to an ammo. Whatever ammo this script is attached to, it will destroy itself on collision of any colliders and then generate an explosion effect
/// </summary>
public class AmmoScript : MonoBehaviour {

    protected GameObject hitObject;
    public GameObject explosionEffect;
    protected GameObject explode;
	// Use this for initialization
	void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
        collisionDetect();
    }

    public void Init()
    {
        hitObject = null;
        explode = null;
    }

    public void collisionDetect()
    {
        if (hitObject != null)
        {
            transform.localScale = Vector3.zero;
            Vector3 originalPos = transform.position; 
            transform.Translate(0, -1000, 0); //When creates explosion, it keeps detecting the ammo, so I move the ammo out of the way so that it won't detect it.
            Object.Destroy(this.gameObject);
            explode = (GameObject)Instantiate(explosionEffect);
            explode.transform.position = originalPos;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        hitObject = collision.gameObject;
    }
}
