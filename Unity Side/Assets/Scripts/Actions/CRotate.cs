using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRotate : SingularAction
{
    public CRotate(Thingy puppet, Quaternion rotation) : base(puppet)
    {
        rotationBefore = puppet.transform.rotation;
        rotationAfter = rotation;
    }

    Quaternion rotationBefore;
    Quaternion rotationAfter;

    protected override void PerformUndo()
    {
        go.transform.rotation = rotationBefore;
    }

    protected override void PerformRedo()
    {
        go.transform.rotation = rotationAfter;
    }
}
