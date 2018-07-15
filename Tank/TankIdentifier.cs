using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If a player's tank is within the detectable distance, it will either move forward or backward determine which location of the player's tank. Intend for AI use.
/// </summary>
public class TankIdentifier : Identifier {

    public override byte Identify(byte b, GameObject self)
    {
        if(self.name == gameObject.name) //So that it will not detect itself as an enemy tank
        {
            return b;
        }
        Reaction(self);
        return b |= 1;
    }

    public override void Reaction(GameObject self)
    {
        Vector3 facingDirection = (transform.position - self.transform.position).normalized;
        TankControlAI TCA = self.GetComponent<TankControlAI>();

        TCA.vertical = Mathf.Abs(self.transform.forward.z) > Mathf.Abs(self.transform.forward.x) ? 
            -(facingDirection.z * self.transform.forward.z) : -(facingDirection.x * self.transform.forward.x);
    }
}
