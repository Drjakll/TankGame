using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Intend to attach to an empty sub GameObject of the barrel. Script is to fire the ammo. Intend for player only. 
/// </summary>
public class Fire : MonoBehaviour {
    public GameObject Ammo; 
    protected GameObject toBeFire;
    protected bool spaceBarDown = false;
    protected Vector3 force;
    protected float magnitudeForce; 
    protected float accTime; //Determine how long the player has been holding the spacebar, will stop player holding space bar for more than 2 sec
    private Vector2 startPt; //Starting point for power bar
    private Vector2 size; //Size of the power bar
    Texture2D texture; //Texture use to display player's current strength of projection of the ammo
    public bool turnOn; //A switch to allow player's to shoot or not. Will turn on when player's turn, and turn off after the player shoots
    // Use this for initialization
    void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
        if (turnOn)
        {
            forceDetect();
        }
	}

    public void Init()
    {
        spaceBarDown = false;
        force = Vector3.zero;
        magnitudeForce = 0;
        accTime = 0.0f;
        texture = new Texture2D(0, 55, TextureFormat.RGBA32, true);
        turnOn = true;
        startPt = new Vector2(Camera.main.pixelWidth / 2 - 300.0f, Camera.main.pixelHeight - 100.0f); //Starting point for power bar
        size = new Vector2(600.0f, 60.0f); //Size of the power bar
    }

    public void forceDetect()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceBarDown = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            magnitudeForce *= 50.0f;
            toBeFire = (GameObject)Instantiate(Ammo);
            toBeFire.transform.position = transform.position;
            toBeFire.transform.eulerAngles = transform.eulerAngles;
            Vector3 components = toBeFire.transform.position - transform.parent.parent.transform.position;
            float Magnitude = components.magnitude;
            force.x = magnitudeForce * (components.x / Magnitude);
            force.y = magnitudeForce * (components.y / Magnitude);
            force.z = magnitudeForce * (components.z / Magnitude);
            toBeFire.GetComponent<ConstantForce>().force = new Vector3(force.x, force.y, force.z);
            StartCoroutine(applyForce());
            magnitudeForce = 0;
            accTime = 0.0f;
            UpdateBar(1);
            spaceBarDown = false;
            turnOn = false;
            GetComponentInParent<TankMovement>().turnOn = false;
        }
        if (spaceBarDown)
        {
            accTime += Time.deltaTime;
            magnitudeForce = 300.0f * accTime;
            UpdateBar((int)magnitudeForce);
            if (accTime > 2.0f)
            {
                spaceBarDown = false;
            }
        }
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.imagePosition = ImagePosition.ImageLeft;
        GUI.Box(new Rect(startPt, size), "Power Bar");
        GUI.Box(new Rect(startPt, size), texture, style);
    }

    public void UpdateBar(int Width)
    {
        texture.Resize(Width, 55, TextureFormat.RGBA32, true);
        Color32[] colorToFill = texture.GetPixels32();
        Color32 colorFill = new Color32(255, 0, 0, 128);
        for (int i = 0; i < colorToFill.Length; i++)
        {
            colorToFill[i] = colorFill;
        }
        texture.SetPixels32(colorToFill);
        texture.Apply();
    }

    protected IEnumerator applyForce()
    {
        yield return new WaitForSeconds(.07f);
        toBeFire.GetComponent<ConstantForce>().force = Vector3.zero;
    }
}
