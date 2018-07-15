using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A tree is nearby. Intend to attach to a tree prefab. Will be called the method Identity when AI tank starts detecting the surrounding for obstacles.
/// </summary>
public class TreeIdentifier : Identifier {

    public override byte Identify(byte b, GameObject self)
    {
        Reaction(self);
        return b |= 4;
    }

    public override void Reaction(GameObject self)
    {
        TankControlAI TCA = self.GetComponent<TankControlAI>();
        Ray forward = new Ray(self.transform.position, self.transform.forward);
        Ray backward = new Ray(self.transform.position, -self.transform.forward);
        RaycastHit hitObject;
        if (Physics.SphereCast(forward, 2.0f, out hitObject, 8.0f) || Physics.SphereCast(backward, 2.0f, out hitObject, 8.0f))
        {
            Vector3 difference = hitObject.transform.position - self.transform.position;
            TCA.rotation = Mathf.Abs(self.transform.forward.z) > Mathf.Abs(self.transform.forward.x) ?
                -(difference.z) * (difference.x) : (difference.x) * (difference.z);
            return;
        }
        TCA.rotation = 0;
    }
}
