using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMovement : EdittingMode
{
    int DESTROY_BUTTON;
    float MoveSpeed = 10f;
    float TurnSpeed = 90f;

    KeyCode FORWARD = KeyCode.W;
    KeyCode LEFT = KeyCode.A;
    KeyCode BACKWARD = KeyCode.S;
    KeyCode RIGHT = KeyCode.D;
    KeyCode UP = KeyCode.E;
    KeyCode DOWN = KeyCode.Q;

    Transform MoveThingy;
    
    public EnableMovement(DirtyFlagController controller, int buttonToDestroy, Transform thingToMove) : base(controller)
    {
        DESTROY_BUTTON = buttonToDestroy;
        MoveThingy = thingToMove;
    }

    public override bool FixedUpdate()
    {
        return base.FixedUpdate();
    }

    public override bool Update()
    {
        if (!Input.GetMouseButton(DESTROY_BUTTON))
        {
            //Debug.Log("DESTROYED");
            return false;
        }

        MoveThingy.position += ReceiveMovement() * Time.deltaTime;

        Vector3 eulers = MoveThingy.rotation.eulerAngles + ReceiveRotation()  * Time.deltaTime * TurnSpeed;
        eulers.x = Mod(eulers.x, 180f, -180f);
        eulers.x = Mathf.Clamp(eulers.x, -90f, 90f);
        eulers.y = Mod(eulers.y, 360f, 0);

        MoveThingy.rotation = Quaternion.Euler(eulers);

        return base.Update();
    }

    public Vector3 ReceiveMovement()
    {
        Vector3 movement = Vector3.zero;
        Quaternion yrot = Quaternion.Euler(0, MoveThingy.rotation.eulerAngles.y, 0);

        if (Input.GetKey(FORWARD))
        {
            movement += yrot * Vector3.forward;
        }
        if (Input.GetKey(LEFT))
        {
            movement -= yrot * Vector3.right;
        }
        if (Input.GetKey(BACKWARD))
        {
            movement -= yrot * Vector3.forward;
        }
        if (Input.GetKey(RIGHT))
        {
            movement += yrot * Vector3.right;
        }
        if (Input.GetKey(UP))
        {
            movement += Vector3.up;
        }
        if (Input.GetKey(DOWN))
        {
            movement -= Vector3.up;
        }

        movement = Vector3.ClampMagnitude(movement, 1);

        movement *= MoveSpeed;

        return movement;
    }

    public Vector3 ReceiveRotation()
    {
        Vector3 rotate = Vector3.zero;

        Vector3 deltaMouse = Input.mousePosition - mousePosition;
        mousePosition = Input.mousePosition;

        rotate.x = deltaMouse.y;
        rotate.y = deltaMouse.x;

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

    public override bool LateUpdate()
    {
        return base.LateUpdate();
    }

    public override bool OnPreRender()
    {
        return base.OnPreRender();
    }

    public override bool OnPostRender()
    {
        return base.OnPostRender();
    }
}
