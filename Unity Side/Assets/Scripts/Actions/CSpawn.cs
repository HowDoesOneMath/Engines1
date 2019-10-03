using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpawn : SingularAction
{
    public CSpawn(Thingy puppet) : base(puppet)
    {

    }

    protected override void PerformUndo()
    {
        go.PseudoDestroy();
    }

    protected override void PerformRedo()
    {
        go.PseudoAwake();
    }
}
