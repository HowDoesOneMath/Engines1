using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableStartRot : EdittingMode
{
    Quaternion startRot;
    Vector3 mPosInitial;
    Vector3 objectOnScreen;
    Thingy editee;
    Quaternion finalRotation;

    public void SetEnableStartRot(DirtyFlagController controller, EdittingMode prev, List<GameObject> setToActive, Thingy toBeEditted)
    {
        SetEdittingMode(controller, prev, setToActive);
        startRot = toBeEditted.transform.localRotation;
        mPosInitial = Input.mousePosition;
        objectOnScreen = dcf.worldCam.WorldToScreenPoint(toBeEditted.transform.position);
        finalRotation = startRot;

        editee = toBeEditted;
    }

    public override bool PseudoFixedUpdate()
    {
        return base.PseudoFixedUpdate();
    }

    public override bool PseudoUpdate()
    {
        if (!CheckAboutActivity())
            return false;

        if (ButtonBool.AnyButtonClicked)
        {
            return false;
        }

        editee.transform.localRotation = startRot;

        if (Input.GetMouseButtonUp(TheManager.TM.LEFT_MOUSE))
        {
            BaseCommand command = new BaseCommand();
            command.AddAction(new CRotate(editee, finalRotation));
            command.Do();
            GoBack();

            return false;
        }

        if (Input.GetMouseButtonUp(TheManager.TM.RIGHT_MOUSE))
        {
            GoBack();
            return false;
        }

        finalRotation = startRot;
        finalRotation = Quaternion.FromToRotation(
            (dcf.worldCam.transform.rotation) * (mPosInitial - objectOnScreen).normalized,
            (dcf.worldCam.transform.rotation) * (Input.mousePosition - objectOnScreen).normalized
            ) * finalRotation;

        return base.PseudoUpdate();
    }

    public override bool PseudoLateUpdate()
    {
        editee.transform.localRotation = finalRotation;

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

    public bool CheckAboutActivity()
    {
        if (editee == null || !editee.gameObject.activeInHierarchy)
        {
            if (editee != null)
                editee.transform.localRotation = startRot;
            GoBack();
            return false;
        }
        return true;
    }
}
