using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A controller that have access to all other parts of the tank. Will be used by TankAI to control the tank. Meant to attach to the AItank GameObject/
/// </summary>
public class TankControlAI : MonoBehaviour {

    public float vertical = 0;
    public float rotation = 0;
    public float barrelRotate = 0;
    public float turrentRotate = 0;
    private MovementAI MvmentAI; //Responsible for tank's moving forward, backward, and rotate left or right
    private TurrentMoveAI[] TurrentAI;
    private BarrelMoveAI[] BarrelAI;
    // Use this for initialization
    void Start()
    {
        MvmentAI = GetComponent<MovementAI>();
        TurrentAI = GetComponentsInChildren<TurrentMoveAI>();
        BarrelAI = GetComponentsInChildren<BarrelMoveAI>();
    }

    // Update is called once per frame
    void Update()
    {
        MvmentAI.moveVertical(vertical);
        MvmentAI.moveHorizontal(rotation);
        foreach (TurrentMoveAI T in TurrentAI) //I do this because the AI tank has different layers of turrent depending on the distance of the camera. So I move every layer
        {
            T.rotate(turrentRotate);
        }
        foreach (BarrelMoveAI B in BarrelAI) //Read comment above this comment. Same for barrel
        {
            B.rotate(barrelRotate);
        }
    }
}
