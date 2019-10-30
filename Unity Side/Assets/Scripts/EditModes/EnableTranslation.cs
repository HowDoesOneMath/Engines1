using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public struct Buttons
{
    public ButtonBool place;
    public ButtonBool delete;
    public ButtonBool EnterMove;
    public ButtonBool EnterRot;
}

public class EnableTranslation : EdittingMode
{
    [SerializeField]
    Thingy editee;
    MeshFilter[] mf;

    public Buttons buttons;

    public float FlashTimer = 1f;
    float flashing = 0f;
    public Material flashColor;
    bool colorSwap = false;

    public void SetEnableTranslation(DirtyFlagController controller, EdittingMode prev, List<GameObject> setToActive, Thingy toBeEditted, MeshFilter[] thingyMeshes, Material flash)
    {
        SetEdittingMode(controller, prev, setToActive);
        editee = toBeEditted;
        mf = thingyMeshes;

        flashColor = flash;
        dcf.mLoc.isInTranslate = true;
        buttons = dcf.modes.trans.buttons;
    }

    public override bool PseudoFixedUpdate()
    {
        return base.PseudoFixedUpdate();
    }

    public override bool PseudoUpdate()
    {
        dcf.mLoc.isInTranslate = true;

        if (!CheckAboutActivity())
            return false;

        if (!ButtonHasBeenClicked())
        {
            return false;
        }

        if (!ButtonReceiving())
        {
            return false;
        }

        return base.PseudoUpdate();
    }

    public override bool PseudoLateUpdate()
    {
        return base.PseudoLateUpdate();
    }

    public override bool PseudoOnPreRender()
    {
        return base.PseudoOnPreRender();
    }

    public override bool PseudoOnPostRender()
    {
        return base.PseudoOnPostRender();
    }

    bool ButtonReceiving()
    {
        if (Input.GetMouseButtonUp(TheManager.TM.LEFT_MOUSE))
        {
            editee.GetComponentInChildren<MeshRenderer>().material = editee.myMat;
            Ray mray = dcf.worldCam.ScreenPointToRay(Input.mousePosition);

            if (!TranslationMode())
            {
                dcf.mLoc.CastIntoAll(dcf.worldCam);
                return false;
            }
        }
        if (Input.GetMouseButton(TheManager.TM.RIGHT_MOUSE))
        {
            //EdittingMode move = new EnableMovement(dcf, TheManager.TM.RIGHT_MOUSE, dcf.transform, this);
            EnableMovement move = gameObject.AddComponent<EnableMovement>();
            DeactivateOnSwitch();
            move.SetEnableMovement(dcf, TheManager.TM.RIGHT_MOUSE, transform, this, dcf.modes.move.activeOnSwitch);
            return false;
        }
        if (Input.GetMouseButton(TheManager.TM.MIDDLE_MOUSE))
        {

        }

        return true;
    }



    bool ButtonHasBeenClicked()
    {
        if (!Input.GetMouseButtonUp(TheManager.TM.LEFT_MOUSE))
        {
            return true;
        }

        if (ButtonBool.AnyButtonClicked)
        {
            if (buttons.place.ButtonIsClicked)
            {
                if (dcf.spawnThingy != null && dcf.mLoc.mouseHit)
                {
                    BaseCommand create = new BaseCommand();
                    Thingy thing = PoolQueue.PQ.RequestThing(dcf.spawnThingy.tot);
                    create.AddAction(new CSpawn(thing));
                    create.AddAction(new CMovement(thing, dcf.mLoc.gizmoPseudoPos));
                    create.Do();
                    GoBack();
                    return false;
                }
            }

            if (buttons.delete.ButtonIsClicked)
            {
                if (editee.Deletable())
                {
                    BaseCommand destroy = new BaseCommand();
                    destroy.AddAction(new CDelete(editee));
                    destroy.Do();
                    GoBack();
                    return false;
                }
            }

            if (buttons.EnterMove.ButtonIsClicked)
            {
                EnablePosition ep = gameObject.AddComponent<EnablePosition>();
                DeactivateOnSwitch();
                ep.SetEnablePosition(dcf, this, dcf.modes.sPos.activeOnSwitch, editee, mf);

                return false;
            }

            if (buttons.EnterRot.ButtonIsClicked)
            {
                EnableRotation ep = gameObject.AddComponent<EnableRotation>();
                DeactivateOnSwitch();
                ep.SetEnableRotation(dcf, this, dcf.modes.sRot.activeOnSwitch, editee, mf);

                return false;
            }

            return false;
        }

        return true;
    }

    bool TranslationMode()
    {
        Thingy t = dcf.mLoc.CastIntoScene(dcf.worldCam);
        if (t == null)
        {
            GoBack();
            return false;
        }
        mf = t.GetComponentsInChildren<MeshFilter>();
        editee = t;

        return true;
    }

    public override void DeactivateOnSwitch()
    {
        dcf.mLoc.isInTranslate = false;
        base.DeactivateOnSwitch();
    }

    public bool CheckAboutActivity()
    {
        if (editee == null || !editee.gameObject.activeInHierarchy)
        {
            GoBack();
            return false;
        }
        return true;
    }

    private void Update()
    {
        if (editee != null)
        { 
            flashing += Time.deltaTime;
            while (flashing > FlashTimer)
            {
                flashing -= FlashTimer;
                colorSwap = !colorSwap;
                if (colorSwap)
                {
                    editee.GetComponentInChildren<MeshRenderer>().material = flashColor;
                }
                else
                {
                    editee.GetComponentInChildren<MeshRenderer>().material = editee.myMat;
                }
            }
        }
    }
}
