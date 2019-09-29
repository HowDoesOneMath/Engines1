using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EdittingMode
{
    Vector3 mousePosition;

    protected EdittingMode(DirtyFlagController controller)
    {
        dcf = controller;
        mousePosition = Input.mousePosition;
        //Debug.Log(mousePosition);
    }

    DirtyFlagController dcf;

    public abstract bool Update();
}
