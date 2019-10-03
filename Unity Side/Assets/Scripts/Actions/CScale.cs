using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScale : SingularAction
{
    public CScale(Thingy puppet, Vector3 scale) : base(puppet)
    {
        scaleBefore = puppet.transform.localScale;
        scaleAfter = scale;
    }

    Vector3 scaleBefore;
    Vector3 scaleAfter;

    protected override void PerformUndo()
    {
        go.transform.localScale = scaleBefore;
    }

    protected override void PerformRedo()
    {
        go.transform.localScale = scaleAfter;
    }
}
