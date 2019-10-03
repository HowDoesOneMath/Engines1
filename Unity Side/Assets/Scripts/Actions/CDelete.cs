using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDelete : SingularAction
{
    public CDelete(Thingy puppet) : base(puppet)
    {

    }

    protected override void PerformUndo()
    {
        go.PseudoAwake();
    }

    protected override void PerformRedo()
    {
        go.PseudoDestroy();
    }
}
