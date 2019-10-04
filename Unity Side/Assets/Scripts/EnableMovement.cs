using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct EditMovementSettings
{
    public float MoveSpeed;
    public float TurnSpeed;

    public char FORWARD;
    public char LEFT;
    public char BACKWARD;
    public char RIGHT;
    public char UP;
    public char DOWN;

    public Vector2 mouseSensitivityAxes;
}

public class EnableMovement : EdittingMode
{
    int DESTROY_BUTTON;

    public EditMovementSettings moveSets;

    Transform MoveThingy;
    
    public void SetEnableMovement(DirtyFlagController controller, int buttonToDestroy, Transform thingToMove, EdittingMode prev, List<GameObject> setToActive, EditMovementSettings esets)
    {
        SetEdittingMode(controller, prev, setToActive);

        moveSets = esets;

        DESTROY_BUTTON = buttonToDestroy;
        MoveThingy = thingToMove;
    }

    public override bool PseudoFixedUpdate()
    {
        return base.PseudoFixedUpdate();
    }

    public override bool PseudoUpdate()
    {
        if (!Input.GetMouseButton(DESTROY_BUTTON))
        {
            GoBack();
            return false;
        }

        MoveThingy.position += ReceiveMovement() * Time.deltaTime;

        Vector3 eulers = MoveThingy.rotation.eulerAngles + ReceiveRotation()  * Time.deltaTime * moveSets.TurnSpeed;
        eulers.x = Mod(eulers.x, 180f, -180f);
        eulers.x = Mathf.Clamp(eulers.x, -90f, 90f);
        eulers.y = Mod(eulers.y, 360f, 0);

        MoveThingy.rotation = Quaternion.Euler(eulers);

        return base.PseudoUpdate();
    }

    public Vector3 ReceiveMovement()
    {
        Vector3 movement = Vector3.zero;
        Quaternion yrot = Quaternion.Euler(0, MoveThingy.rotation.eulerAngles.y, 0);

        if (Input.GetKey((KeyCode)moveSets.FORWARD))
        {
            movement += yrot * Vector3.forward;
        }
        if (Input.GetKey((KeyCode)moveSets.LEFT))
        {
            movement -= yrot * Vector3.right;
        }
        if (Input.GetKey((KeyCode)moveSets.BACKWARD))
        {
            movement -= yrot * Vector3.forward;
        }
        if (Input.GetKey((KeyCode)moveSets.RIGHT))
        {
            movement += yrot * Vector3.right;
        }
        if (Input.GetKey((KeyCode)moveSets.UP))
        {
            movement += Vector3.up;
        }
        if (Input.GetKey((KeyCode)moveSets.DOWN))
        {
            movement -= Vector3.up;
        }

        movement = Vector3.ClampMagnitude(movement, 1);

        movement *= moveSets.MoveSpeed;

        return movement;
    }

    public Vector3 ReceiveRotation()
    {
        Vector3 rotate = Vector3.zero;

        Vector3 deltaMouse = Input.mousePosition - mousePosition;
        mousePosition = Input.mousePosition;

        rotate.x = deltaMouse.y * moveSets.mouseSensitivityAxes.y;
        rotate.y = deltaMouse.x * moveSets.mouseSensitivityAxes.x;

        return rotate;
    }

    float Mod(float value, float modUpper, float modLower)
    {
        float Val = value;
        while (Val > modUpper)
            Val -= (modUpper - modLower);
        while (Val <= modLower)
            Val += (modUpper - modLower);
        return Val;
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
}
