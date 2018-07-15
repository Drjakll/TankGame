using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This attach to GrassField GameObject as to take in charge whos turn it is. 
/// </summary>
public class Referee : MonoBehaviour {

    public GameObject playerTank;
    public GameObject[] AITank; //For now even the game has 1 AI tank, but if in future more AI tanks are to be inserted to the game, this can be an option
    private int numberOfTanks;
    private int whosTurn; //0 = player's turn, 1 and above means AI tanks' turn
    private string TurnDisplay;
	// Use this for initialization
	void Start () {
        numberOfTanks = AITank.Length + 1;
        whosTurn = 0;
        TurnDisplay = "Your Turn";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //This method is called when an explosion finished exploding.
    public void nextTankTurn()
    {
        whosTurn = (whosTurn + 1) % numberOfTanks;
        if (whosTurn == 0)
        {
            playerTank.GetComponent<TankMovement>().turnOn = true;
            playerTank.GetComponentInChildren<Fire>().turnOn = true;
            TurnDisplay = "Your Turn";
        }
        else
        {
            AITank[whosTurn - 1].GetComponent<TankAI>().Move = true;
            TurnDisplay = "Enemy Turn";
        }
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(Camera.main.pixelWidth / 2.0f, 10.0f, 100.0f, 40.0f), TurnDisplay);
    }
}
