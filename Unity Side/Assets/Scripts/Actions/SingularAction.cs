using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingularAction
{
    protected Thingy go;

    protected SingularAction(Thingy puppet)
    {
        go = puppet;
    }

    public bool Done { get; set; } = false;

    protected abstract void PerformUndo();

    protected abstract void PerformRedo();

    public void Undo()
    {
        if (Done)
        {
            Flip();
            PerformUndo();
        }
    }

    public void Redo()
    {
        if (!Done)
        {
            Flip();
            PerformRedo();
        }
    }

    protected void Flip()
    {
        Done = !Done;
    }
}
