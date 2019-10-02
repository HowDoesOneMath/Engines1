using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbitrary2 : Thingy
{
    public override void PseudoAwake()
    {
        tot = TypeOfThingy.Test2;
        base.PseudoAwake();
    }
}
