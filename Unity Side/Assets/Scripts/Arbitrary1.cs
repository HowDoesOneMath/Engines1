using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbitrary1 : Thingy
{
    public override void PseudoAwake()
    {
        tot = TypeOfThingy.Test1;
        base.PseudoAwake();
    }
}
