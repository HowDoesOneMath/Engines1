using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMovement : EdittingMode
{
    int DESTROY_BUTTON;

    public EnableMovement(DirtyFlagController controller) : base(controller)
    {
        DESTROY_BUTTON = TheManager.TM.RIGHT_MOUSE;
    }

    public override bool Update()
    {
        if (!Input.GetMouseButton(DESTROY_BUTTON))
        {
            //Debug.Log("DESTROYED");
            return false;
        }

        

        return true;
    }
}
