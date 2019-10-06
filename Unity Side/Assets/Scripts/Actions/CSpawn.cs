using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpawn : SingularAction
{
    public CSpawn(Thingy puppet, bool MakeOwnThing = false) : base(MakeOwnThing ? PoolQueue.PQ.RequestThing(puppet.tot) : puppet)
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
