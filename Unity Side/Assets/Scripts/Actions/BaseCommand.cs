using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCommand
{
    public BaseCommand()
    {
        actions = new List<SingularAction>();
        //TheManager.TM.AddCommand(this);
    }

    bool sent = false;

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

    public void Do()
    {
        if (!sent)
        {
            sent = true;
            Flip();
            PerformRedo();
            TheManager.TM.AddCommand(this);
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
