using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCommand
{
    protected BaseCommand(GameObject puppet)
    {
        TheManager.TM.AddCommand(this);
    }

    public bool Done { get; set; } = false;

    List<SingularAction> actions;

    public void AddAction(SingularAction sa)
    {
        actions.Add(sa);
    }

    protected void PerformUndo()
    {
        for (int i = actions.Count - 1; i >= 0; --i)
        {
            actions[i].Undo();
        }
    }

    protected void PerformRedo()
    {
        for (int i = 0; i < actions.Count; ++i)
        {
            actions[i].Redo();
        }
    }

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
