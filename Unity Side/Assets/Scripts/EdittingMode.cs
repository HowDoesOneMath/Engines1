using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EdittingMode
{
    protected Vector3 mousePosition;

    protected EdittingMode(DirtyFlagController controller)
    {
        dcf = controller;
        mousePosition = Input.mousePosition;
        //Debug.Log(mousePosition);
    }

    DirtyFlagController dcf;

    public virtual bool Update()
    {
        return true;
    }

    public virtual bool FixedUpdate()
    {
        return true;
    }

    public virtual bool LateUpdate()
    {
        return true;
    }
           
    public virtual bool OnPreRender()
    {
        return true;
    }
           
    public virtual bool OnPostRender()
    {
        return true;
    }
}
