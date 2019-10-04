using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMainMenu : EdittingMode
{
    public void SetEnableMainMenu(DirtyFlagController controller, EdittingMode prev, List<GameObject> setToActive)
    {
        SetEdittingMode(controller, prev, setToActive);
    }

    public override bool PseudoFixedUpdate()
    {
        return base.PseudoFixedUpdate();
    }

    public override bool PseudoUpdate()
    {
        if (!ButtonReceiving())
        {
            return false;
        }

        return base.PseudoUpdate();
    }

    public override bool PseudoLateUpdate()
    {
        return base.PseudoLateUpdate();
    }

    public override bool PseudoOnPreRender()
    {
        return base.PseudoOnPreRender();
    }

    public override bool PseudoOnPostRender()
    {
        return base.PseudoOnPostRender();
    }

    bool ButtonReceiving()
    {
        if (Input.GetMouseButton(TheManager.TM.LEFT_MOUSE))
        {

        }
        if (Input.GetMouseButton(TheManager.TM.RIGHT_MOUSE))
        {
            //EdittingMode move = new EnableMovement(dcf, TheManager.TM.RIGHT_MOUSE, dcf.transform, this);
            EnableMovement move = gameObject.AddComponent<EnableMovement>();
            move.SetEnableMovement(dcf, TheManager.TM.RIGHT_MOUSE, transform, this, dcf.modes.move.activeOnSwitch, dcf.modes.move.moveSets);
            return false;
        }
        if (Input.GetMouseButton(TheManager.TM.MIDDLE_MOUSE))
        {

        }

        return true;
    }
}
