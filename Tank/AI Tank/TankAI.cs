using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// AI tank's AI sequence. Should be attached to the GameObject AITank. Further explanation down below for each part.
/// </summary>
public class TankAI : MonoBehaviour {

    private byte obstacles; //Will hold information about which obstacles are nearby. For example, if a player tank is within the detectable radius, this value will be 1. And if a rock and a tank is detected, this value will be 3.
    private TankControlAI TankConAI; //Controller to control the computer's tank
    private TurrentDirection TD; //Attached to the tip of the barrel. It is use to determine the direction of the barrel
    private float verticalAngle; //It will generate a random angle each time computer shoots
    private float speed; //Speed of the ammo should fire in order to reach to target tank
    private GimTrim Anker; //Used to help determine the direction of the barrel. It's set between the barrel and the turrent body
    private float ammoMass; //Needed for speed calculation
    public GameObject lockedTarget; //Target tank
    public bool Move;
    public bool Aim;
    public bool Fire;

    // Use this for initialization
    void Start() {
        TankConAI = GetComponent<TankControlAI>();
        TD = GetComponentInChildren<TurrentDirection>();
        obstacles = 0;
        Move = false;
        Aim = false;
        verticalAngle = 0;
        speed = 0;
        Fire = false;
        Anker = GetComponentInChildren<GimTrim>();
        ammoMass = GetComponentInChildren<FireAI>().Ammo.GetComponent<Rigidbody>().mass;
    }

    // Update is called once per frame
    void Update() {
        if (Move)
        {
            //Scan the radius within 100 units for colliders
            Collider[] visibleObjects = Physics.OverlapSphere(transform.position, 100.0f);
            foreach (Collider VO in visibleObjects)
            {
                //Identifier is a parent base class which inherits by different other identifiers. 
                if (VO.gameObject.GetComponent<Identifier>() != null)
                {
                    //Each identifier has a method called Identify and variable "obstacles" will catch what type of obstacle it is. For instance, it might return an obstacles |= 2, which identifies as a rock.
                    obstacles = VO.gameObject.GetComponent<Identifier>().Identify(obstacles, this.gameObject);
                }
            }
            handleObstacles();
            obstacles = 0;
        }
        else if (Aim)
        {
            RotateAndAim();
        }
        else if (Fire)
        {
            calculateSpeed();
            TD.GetComponentInChildren<FireAI>().Fire(speed);
            speed = 0.0f;
            Fire = false;
        }
    }

    //This method takes care of how each obstacle should be handle. 
    private void handleObstacles()
    {
        //if obstacles is not 0000 0001 then it  didn't detect a tank nearby and therefore it will not move further.
        if ((obstacles & 1) == 0)
        {
            TankConAI.vertical = 0.0f;
            TankConAI.rotation = 0.0f;
            randomVerticalAngle();
            whichSideToTurn();
            Move = false;
            Aim = true;
        }
    }

    private void randomVerticalAngle()
    {
        verticalAngle = Random.Range(0, convertAngleToRadian(65));
    }

    private float convertAngleToRadian(float angle)
    {
        return (Mathf.PI * angle) / 180.0f;
    }

    private float convertRadianToAngle(float radian)
    {
        return (radian * 180.0f) / Mathf.PI;
    }

    //Since Unity only uses between 180 and -180 degrees, I intend to convert it to 360 degrees
    private float convertCosAngle(float CosAngle, float y)
    {
        return y < 0 ? 2 * Mathf.PI - CosAngle : CosAngle;
    }

    //When turning the turrent, it decide to turn to the closest rotation to the target tank.
    private void whichSideToTurn()
    {
        Vector2 barrelDirection = (new Vector2(TD.transform.position.x - Anker.transform.position.x, TD.transform.position.z - Anker.transform.position.z)).normalized;
        Vector2 targetDirection = (new Vector2(lockedTarget.transform.position.x - Anker.transform.position.x, lockedTarget.transform.position.z - Anker.transform.position.z)).normalized;
        float difference = convertCosAngle(Mathf.Acos(barrelDirection.x), barrelDirection.y) - convertCosAngle(Mathf.Acos(targetDirection.x), targetDirection.y);

        if (Mathf.Abs(difference) < 0.03f)
            return;

        TankConAI.turrentRotate = Mathf.Abs(difference) > Mathf.PI ? -difference : difference;
    }

    //Rotate turrent and barrel angle
    private void RotateAndAim()
    {
        Vector2 barrelDirection = (new Vector2(TD.transform.position.x - Anker.transform.position.x, TD.transform.position.z - Anker.transform.position.z)).normalized;
        Vector2 targetDirection = (new Vector2(lockedTarget.transform.position.x - Anker.transform.position.x, lockedTarget.transform.position.z - Anker.transform.position.z)).normalized;
        Vector2 difference = targetDirection - barrelDirection;
        if (difference.magnitude <= 0.03f)
        {
            TankConAI.turrentRotate = 0.0f;
            Vector3 barrelVerticalDirection = (TD.transform.position - Anker.transform.position).normalized;
            
            float currentBarrelAngle = Mathf.Asin(barrelVerticalDirection.y);
            TankConAI.barrelRotate = (currentBarrelAngle - verticalAngle > 0) ? 1 : -1;
            if (Mathf.Abs(currentBarrelAngle - verticalAngle) < .03f)
            {
                TankConAI.barrelRotate = 0.0f;
                Aim = false;
                Fire = true;
            }
            return;
        }
    }

    private void calculateSpeed()
    {
        Vector2 distanceVector = new Vector2(lockedTarget.transform.position.x - transform.position.x, lockedTarget.transform.position.z - transform.position.z);
        float distance = distanceVector.magnitude;
        speed = (distance * 9.8f) / (2 * Mathf.Sin(verticalAngle) * Mathf.Cos(verticalAngle));
        speed = Mathf.Sqrt(speed);
        float landDistance = speed * speed * 2.0f * Mathf.Cos(verticalAngle) * Mathf.Sin(verticalAngle) / 9.8f;
    }
}
