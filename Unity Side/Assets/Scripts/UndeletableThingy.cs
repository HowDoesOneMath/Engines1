using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeletableThingy : Thingy
{
    public override bool Deletable()
    {
        return false;
    }
}
