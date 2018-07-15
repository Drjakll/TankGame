using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Intend to attach to a rock GameObject. Will be detectable by AI tank when AI tank is close enough. 
/// </summary>
public class RockIdentifier : Identifier {

    public override byte Identify(byte b, GameObject self)
    {
        Reaction(self);
        return b |= 2;
    }

    public override void Reaction(GameObject self)
    {
        TankControlAI TCA = self.GetComponent<TankControlAI>();
        Ray forward = new Ray(self.transform.position, self.transform.forward);
        Ray backward = new Ray(self.transform.position, -self.transform.forward);
        RaycastHit hitObject;
        //Will command the AI tank to turn left or right depends whether the center of the rock is more of the left side or the right side of the AI tank
        if (Physics.SphereCast(forward, 2.0f, out hitObject, 8.0f) || Physics.SphereCast(backward, 2.0f, out hitObject, 8.0f))
        {
            Vector3 difference = hitObject.transform.position - self.transform.position;
            TCA.rotation = Mathf.Abs(self.transform.forward.z) > Mathf.Abs(self.transform.forward.x) ?
                -(difference.z)*(difference.x) : (difference.x)*(difference.z);
            return;
        }
        TCA.rotation = 0;
    }
}
