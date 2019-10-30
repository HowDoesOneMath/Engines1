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
        objectOnScreen.z = 0f;
        finalRotation = startRot;

        //Debug.Log(objectOnScreen);

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

        float sin = Mathf.Asin(Vector3.Dot(Vector3.Cross((mPosInitial - objectOnScreen).normalized, (Input.mousePosition - objectOnScreen).normalized), Vector3.forward)) * Mathf.Rad2Deg;
        sin = Vector3.Dot((Input.mousePosition - objectOnScreen), (mPosInitial - objectOnScreen)) > 0 ? sin : 180f - sin;

        finalRotation = startRot;
        Quaternion qTemp = dcf.worldCam.transform.rotation;
        finalRotation = qTemp * Quaternion.Euler(0, 0, sin) * Quaternion.Inverse(qTemp) * finalRotation;

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
