using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMovement : SingularAction
{
    public CMovement(Thingy puppet, Vector3 position) : base(puppet)
    {
        positionBefore = puppet.transform.position;
        positionAfter = position;
    }

    Vector3 positionAfter;
    Vector3 positionBefore;

    protected override void PerformUndo()
    {
        go.transform.position = positionBefore;
    }

    protected override void PerformRedo()
    {
        go.transform.position = positionAfter;
    }
}
