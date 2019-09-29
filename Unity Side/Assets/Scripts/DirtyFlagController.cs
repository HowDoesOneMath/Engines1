using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DirtyFlagController : MonoBehaviour
{
    static EdittingMode editMode;

    private void Awake()
    {
        TheManager.TM.Controls.Add(this);   
    }

    void Update()
    {
        if (TheManager.TM.SetDirty(true))
        {
            if (editMode == null)
            {
                ButtonReceiving();
            }
            else
            {
                if (!editMode.Update())
                {
                    editMode = null;
                }
            }
        }
    }

    void ButtonReceiving()
    {
        if (Input.GetMouseButton(TheManager.TM.LEFT_MOUSE))
        {

        }
        if (Input.GetMouseButton(TheManager.TM.RIGHT_MOUSE))
        {
            editMode = new EnableMovement(this);
        }
        if (Input.GetMouseButton(TheManager.TM.MIDDLE_MOUSE))
        {

        }
    }

    private void LateUpdate()
    {
        if (TheManager.TM.SetDirty(false))
        {

        }
    }

    private void OnDestroy()
    {
        TheManager.TM.Controls.Remove(this);
    }
}