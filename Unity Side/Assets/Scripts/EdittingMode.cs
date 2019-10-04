using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[DisallowMultipleComponent]
public abstract class EdittingMode : MonoBehaviour
{
    public List<GameObject> activeOnSwitch;

    protected Vector3 mousePosition;

    protected void SetEdittingMode(DirtyFlagController controller, EdittingMode previousState, List<GameObject> setToActive)
    {
        prevState = previousState;
        dcf = controller;
        dcf.editMode = this;
        mousePosition = Input.mousePosition;
        //Debug.Log(mousePosition);

        activeOnSwitch = setToActive;

        for (int i = 0; i < activeOnSwitch.Count; ++i)
        {
            activeOnSwitch[i].SetActive(true);
        }
    }

    protected DirtyFlagController dcf;
    public EdittingMode prevState { get; protected set; }

    public virtual bool PseudoUpdate()
    {
        return true;
    }

    public virtual bool PseudoFixedUpdate()
    {
        return true;
    }

    public virtual bool PseudoLateUpdate()
    {
        return true;
    }
           
    public virtual bool PseudoOnPreRender()
    {
        return true;
    }
           
    public virtual bool PseudoOnPostRender()
    {
        return true;
    }

    protected void GoBack()
    {
        dcf.RemovePrior = true;
        for (int i = 0; i < activeOnSwitch.Count; ++i)
        {
            activeOnSwitch[i].SetActive(false);
        }
    }

    public virtual void ResetVals()
    {
        mousePosition = Input.mousePosition;

        for (int i = 0; i < activeOnSwitch.Count; ++i)
        {
            activeOnSwitch[i].SetActive(true);
        }
    }
}
