using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// AI tank will identify a wall and the script is intended to be attach to a wall. 
/// </summary>
public class WallIdentifier : Identifier
{
    public override byte Identify(byte b, GameObject self)
    {
        Reaction(self);
        return b |= 8;
    }

    //How the AI tank will react to this obstacle.
    public override void Reaction(GameObject self)
    {
        TankControlAI TCA = self.GetComponent<TankControlAI>();
        Ray forward = new Ray(self.transform.position, self.transform.forward);
        Ray backward = new Ray(self.transform.position, -self.transform.forward);
        RaycastHit hitObject;
        if (Physics.SphereCast(forward, 2.0f, out hitObject, 8.0f) || Physics.SphereCast(backward, 2.0f, out hitObject, 8.0f))
        {
            TCA.rotation = 1.0f;
            return;
        }
        TCA.rotation = 0;
    }
}
