using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableStartPos : EdittingMode
{
    Vector3 startPos;
    float dist;
    Thingy editee;
    Vector3 finalPosition;

    public void SetEnableStartPos(DirtyFlagController controller, EdittingMode prev, List<GameObject> setToActive, Thingy toBeEditted)
    {
        SetEdittingMode(controller, prev, setToActive);

        startPos = toBeEditted.transform.localPosition;
        dist = Vector3.Dot(dcf.worldCam.transform.forward, (startPos - dcf.worldCam.transform.position));
        finalPosition = startPos;

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

        editee.transform.localPosition = startPos;

        if (Input.GetMouseButtonUp(TheManager.TM.LEFT_MOUSE))
        {
            BaseCommand command = new BaseCommand();
            command.AddAction(new CMovement(editee, finalPosition));
            command.Do();
            GoBack();

            return false;
        }

        if (Input.GetMouseButtonUp(TheManager.TM.RIGHT_MOUSE))
        {
            GoBack();
            return false;
        }

        finalPosition = dcf.worldCam.ScreenPointToRay(Input.mousePosition).direction;
        finalPosition = finalPosition * dist / Vector3.Dot(finalPosition, dcf.worldCam.transform.forward) + dcf.worldCam.transform.position;
        //Debug.Log(finalPosition);

        return base.PseudoUpdate();
    }

    public override bool PseudoLateUpdate()
    {
        editee.transform.localPosition = finalPosition;

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
                editee.transform.localPosition = startPos;
            GoBack();
            return false;
        }
        return true;
    }
}
