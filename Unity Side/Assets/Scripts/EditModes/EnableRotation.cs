using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRotation : EdittingMode
{
    Thingy editee;
    MeshCollider MC;
    MeshFilter[] mf;

    public ButtonBool backButton;

    public void SetEnableRotation(DirtyFlagController controller, EdittingMode prev, List<GameObject> setToActive, Thingy toBeEditted, MeshFilter[] thingyMeshes)
    {
        SetEdittingMode(controller, prev, setToActive);
        editee = toBeEditted;
        mf = thingyMeshes;

        backButton = dcf.modes.sRot.backButton;
    }

    public override bool PseudoFixedUpdate()
    {
        return base.PseudoFixedUpdate();
    }

    public override bool PseudoUpdate()
    {
        if (!CheckAboutActivity())
            return false;

        if (!ButtonHasBeenClicked())
            return false;

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
        if (Input.GetMouseButtonUp(TheManager.TM.LEFT_MOUSE))
        {
            EnableStartRot esr = gameObject.AddComponent<EnableStartRot>();
            DeactivateOnSwitch();
            esr.SetEnableStartRot(dcf, this, dcf.modes.esr.activeOnSwitch, editee);
            return false;
        }
        if (Input.GetMouseButton(TheManager.TM.RIGHT_MOUSE))
        {
            EnableMovement move = gameObject.AddComponent<EnableMovement>();
            DeactivateOnSwitch();
            move.SetEnableMovement(dcf, TheManager.TM.RIGHT_MOUSE, transform, this, dcf.modes.move.activeOnSwitch);
            return false;
        }
        if (Input.GetMouseButton(TheManager.TM.MIDDLE_MOUSE))
        {

        }

        return true;
    }

    bool ButtonHasBeenClicked()
    {
        if (!Input.GetMouseButtonUp(TheManager.TM.LEFT_MOUSE))
            return true;

        if (ButtonBool.AnyButtonClicked)
        {
            if (backButton.ButtonIsClicked)
            {
                GoBack();
                return false;
            }

            return false;
        }
        return true;
    }

    public bool CheckAboutActivity()
    {
        if (editee == null || !editee.gameObject.activeInHierarchy)
        {
            GoBack();
            return false;
        }
        return true;
    }
}
