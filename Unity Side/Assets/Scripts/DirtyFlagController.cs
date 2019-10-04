﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ModeStruct
{
    public EnableMainMenu main;
    public EnableMovement move;
}

[DisallowMultipleComponent]
public class DirtyFlagController : MonoBehaviour
{
    static EdittingMode edits;
    public EdittingMode editMode
    {
        get
        {
            if (edits == null)
            {
                EnableMainMenu enabmen = gameObject.AddComponent<EnableMainMenu>();
                enabmen.SetEnableMainMenu(this, null, modes.main.activeOnSwitch);
                edits = enabmen;
            }
            return edits;
        }
        set { edits = value; }
    }

    public List<Thingy> instantiableObjects;

    public ModeStruct modes;

    bool chosen = false;

    public bool RemovePrior { get; set; } = false;

    private void Awake()
    {
        TheManager.TM.Controls.Add(this);
        FileFuncs.CloseAll();
    }

    private void FixedUpdate()
    {
        if (chosen && editMode != null)
        {
            //if (!editMode.PseudoFixedUpdate())
            //{
            //    editMode = null;
            //}
            CheckPrior();
            editMode.PseudoFixedUpdate();
        }
    }

    void Update()
    {
        chosen = TheManager.TM.SetDirty(true);
        if (chosen && editMode != null)
        {
            CheckPrior();
            //if (Input.GetKeyUp(KeyCode.P))
            //{
            //    FileSave fs = new FileSave("ThisIsIt");
            //    fs.Save();
            //}
            //if (Input.GetKeyUp(KeyCode.O))
            //{
            //    FileLoad fl = new FileLoad("ThisIsIt");
            //    fl.Load();
            //}
            //if (Input.GetKeyUp(KeyCode.R))
            //{
            //    TheManager.TM.Redo();
            //}
            //if (Input.GetKeyUp(KeyCode.U))
            //{
            //    TheManager.TM.Undo();
            //}
            //if (editMode == null)
            //{
            //    ButtonReceiving();
            //}
            //else
            //{
            //    if (!editMode.PseudoUpdate())
            //    {
            //        editMode = null;
            //    }
            //}
            editMode.PseudoUpdate();
        }
    }

    void ButtonReceiving()
    {
        //if (Input.GetMouseButton(TheManager.TM.LEFT_MOUSE))
        //{
        //
        //}
        //if (Input.GetMouseButton(TheManager.TM.RIGHT_MOUSE))
        //{
        //    editMode = new EnableMovement(this, TheManager.TM.RIGHT_MOUSE, transform);
        //}
        //if (Input.GetMouseButton(TheManager.TM.MIDDLE_MOUSE))
        //{
        //
        //}
    }

    public Thingy TryForType(TypeOfThingy tot)
    {
        for (int i = 0; i < instantiableObjects.Count; ++i)
        {
            if (instantiableObjects[i].tot == tot)
            {
                return Instantiate(instantiableObjects[i]);
            }
        }

        return null;
    }

    private void LateUpdate()
    {
        if (chosen && editMode != null)
        {
            //if (!editMode.PseudoFixedUpdate())
            //{
            //    editMode = null;
            //}
            CheckPrior();
            editMode.PseudoLateUpdate();
        }

        TheManager.TM.SetDirty(false);
    }

    private void OnPreRender()
    {
        if (chosen && editMode != null)
        {
            //if (!editMode.PseudoOnPreRender())
            //{
            //    editMode = null;
            //}
            CheckPrior();
            editMode.PseudoOnPreRender();
        }
    }

    private void OnPostRender()
    {
        if (chosen && editMode != null)
        {
            //if (!editMode.PseudoOnPostRender())
            //{
            //    editMode = null;
            //}
            CheckPrior();
            editMode.PseudoOnPostRender();
        }
    }

    private void OnDestroy()
    {
        TheManager.TM.Controls.Remove(this);
    }

    void CheckPrior()
    {
        if (RemovePrior)
        {
            RemovePrior = false;
            EdittingMode etemp = editMode;
            editMode = editMode.prevState;
            Destroy(etemp);
            editMode.ResetVals();
        }
    }
}