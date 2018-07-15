using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Tank movement for forward, backward, rotate left, rotate right. Intended for player use only. Should attach to the player's tank GameObject.
/// </summary>
public class TankMovement : MonoBehaviour {

    public float speed;
    public float rotationSpeed;
    protected float angles;
    protected bool leftMouseDown = false;
    public bool turnOn;
    private float distanceTraveled;
    private Vector2 rectStartgPt;
    private Vector2 rectSize;
    Texture2D texture; //Texture use to show how much movements does the player's tank has
    // Use this for initialization
    void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
        if(turnOn)
        {
            Move();
        }
        else
        {
            distanceTraveled = 0.0f;
        }
	}

    public void Init()
    {
        speed = 5.0f;
        rotationSpeed = 30.0f;
        angles = transform.localEulerAngles.y;
        leftMouseDown = false;
        turnOn = true;
        distanceTraveled = 0.0f;
        rectStartgPt = new Vector2(10, 10);
        rectSize = new Vector2(60.0f, 400.0f);
        texture = new Texture2D(55, 400, TextureFormat.RGBA32, true);
    }

    public void Move()
    {
        float vertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        if (Input.GetMouseButtonDown(0))
        {
            leftMouseDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            leftMouseDown = false;
        }
        if (vertical != 0 && !leftMouseDown) 
        {
            float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
            angles += horizontal;
            transform.localEulerAngles = new Vector3(0, angles, 0);
        }
        if(!leftMouseDown) //Will stop tank from moving forward if left mouse button is being push because when left mouse is being push, it should only allow barrel to move up and down.
        {
            transform.Translate(new Vector3(0, 0, vertical), Space.Self);
            distanceTraveled += Mathf.Abs(vertical);
            updateMovementBar((int)(420 - distanceTraveled * 10) + 1);
            //If player travel more than 42 units of distance, it will no longer allow player's tank to move for the remaining turn
            if (distanceTraveled >= 42.0f)
            {
                turnOn = false;
                distanceTraveled = 0.0f;
            }
        }
    }

    public void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.imagePosition = ImagePosition.ImageAbove;
        GUI.Box(new Rect(rectStartgPt, rectSize), texture);
    }

    private void updateMovementBar(int Height)
    {
        texture.Resize(55, Height, TextureFormat.RGBA32, true);
        Color32[] colorToFill = texture.GetPixels32();
        Color32 colorFill = new Color32(255, 0, 0, 128);
        for (int i = 0; i < colorToFill.Length; i++)
        {
            colorToFill[i] = colorFill;
        }
        texture.SetPixels32(colorToFill);
        texture.Apply();
    }
}
