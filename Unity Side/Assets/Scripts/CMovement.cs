using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMovement : BaseCommand
{
    public CMovement(GameObject puppet, Vector3 position) : base(puppet)
    {
        positionBefore = puppet.transform.position;
        puppet.transform.position = position;
        positionAfter = puppet.transform.position;

        Done = true;
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
