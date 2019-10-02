using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TheManager
{
    #region SINGLETONS
    private TheManager() { }

    static TheManager tM;
    public static TheManager TM
    {
        get { if (tM == null) { tM = new TheManager(); } return tM; }
        private set { tM = value; }
    }

    List<Thingy> longNameForThingies;
    public List<Thingy> Thingies
    {
        get { if (longNameForThingies == null) { longNameForThingies = new List<Thingy>(); } return longNameForThingies; }
        private set { longNameForThingies = value; }
    }

    List<BaseCommand> longNameForCommandList;
    public List<BaseCommand> CommandList
    {
        get { if (longNameForCommandList == null) { longNameForCommandList = new List<BaseCommand>(); } return longNameForCommandList; }
        private set { longNameForCommandList = value; }
    }

    List<DirtyFlagController> longNameForControls;
    public List<DirtyFlagController> Controls
    {
        get { if (longNameForControls == null) { longNameForControls = new List<DirtyFlagController>(); } return longNameForControls; }
        set { longNameForControls = value; }
    }
    #endregion

    #region COMMAND_HANDLER
    int CurrentCommand = -1;
    int undoLimit = 50;
    public int UndoLimit
    {
        get { return undoLimit; }
        set { RemoveUndos(); undoLimit = value; TrimCommandList(); }
    }

    public void AddCommand(BaseCommand bc)
    {
        if (CurrentCommand < CommandList.Count - 1)
            RemoveUndos();
        CommandList.Add(bc);
        CurrentCommand++;
        TrimCommandList();
    }

    void TrimCommandList()
    {
        while (CommandList.Count > UndoLimit)
        {
            CurrentCommand--;
            CommandList.RemoveAt(0);
        }
    }

    void RemoveUndos()
    {
        for (int k = CurrentCommand + 1; k < CommandList.Count; )
        {
            CommandList.RemoveAt(k);
        }
    }

    public void Undo()
    {
        if (CurrentCommand >= 0)
        {
            CommandList[CurrentCommand].Undo();
            CurrentCommand--;
        }
    }

    public void Redo()
    {
        if (CurrentCommand < CommandList.Count - 1)
        {
            CurrentCommand++;
            CommandList[CurrentCommand].Redo();
        }
    }
    #endregion

    #region DIRTY_FLAG_CONTROLLERS

    bool controlIsDirty = false;

    public bool SetDirty(bool dirty)
    {
        if (controlIsDirty != dirty)
        {
            controlIsDirty = dirty;
            return true;
        }

        return false;
    }

    public Thingy GetThingyOfType(System.Type t)
    {
        for (int i = 0; i < Controls.Count; i++)
        {
            Thingy thing = Controls[i].TryForType(t);
            if (thing != null)
                return thing;
        }

        return null;
    }

    public void DestroyAll()
    {
        for (int i = Thingies.Count - 1; i >= 0; --i)
        {
            Thingies[i].PseudoDestroy();
        }
    }

    #endregion

    public int LEFT_MOUSE { get; } = 0;
    public int RIGHT_MOUSE { get; } = 1;
    public int MIDDLE_MOUSE { get; } = 2;
}