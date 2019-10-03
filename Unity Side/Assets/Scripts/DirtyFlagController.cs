using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DirtyFlagController : MonoBehaviour
{
    static EdittingMode editMode;

    public List<Thingy> instantiableObjects;

    bool chosen = false;

    private void Awake()
    {
        TheManager.TM.Controls.Add(this);
        FileFuncs.CloseAll();
    }

    private void FixedUpdate()
    {
        if (chosen && editMode != null)
        {
            if (!editMode.FixedUpdate())
            {
                editMode = null;
            }
        }
    }

    void Update()
    {
        chosen = TheManager.TM.SetDirty(true);
        if (chosen)
        {
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
            editMode = new EnableMovement(this, TheManager.TM.RIGHT_MOUSE, transform);
        }
        if (Input.GetMouseButton(TheManager.TM.MIDDLE_MOUSE))
        {

        }
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
            if (!editMode.FixedUpdate())
            {
                editMode = null;
            }
        }

        TheManager.TM.SetDirty(false);
    }

    private void OnPreRender()
    {
        if (chosen && editMode != null)
        {
            if (!editMode.OnPreRender())
            {
                editMode = null;
            }
        }
    }

    private void OnPostRender()
    {
        if (chosen && editMode != null)
        {
            if (!editMode.OnPostRender())
            {
                editMode = null;
            }
        }
    }

    private void OnDestroy()
    {
        TheManager.TM.Controls.Remove(this);
    }
}