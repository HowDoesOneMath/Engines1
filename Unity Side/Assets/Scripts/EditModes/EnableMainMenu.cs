using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct MenuButtons
{
    public ButtonBool save;
    public ButtonBool load;
    public ButtonBool place;
}

public class EnableMainMenu : EdittingMode
{
    public MenuButtons menuButtons;

    public void SetEnableMainMenu(DirtyFlagController controller, EdittingMode prev, List<GameObject> setToActive)
    {
        SetEdittingMode(controller, prev, setToActive);

        menuButtons = dcf.modes.main.menuButtons;
    }

    public override bool PseudoFixedUpdate()
    {
        return base.PseudoFixedUpdate();
    }

    public override bool PseudoUpdate()
    {
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
            if (!TranslationMode())
                return false;
            if (!dcf.mLoc.CastIntoAll(dcf.worldCam))
                return false;
        }
        if (Input.GetMouseButton(TheManager.TM.RIGHT_MOUSE))
        {
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
            return true;

        if (ButtonBool.AnyButtonClicked)
        {
            if (menuButtons.place.ButtonIsClicked)
            {
                if (dcf.spawnThingy != null && dcf.mLoc.mouseHit)
                {
                    BaseCommand create = new BaseCommand();
                    Thingy thing = PoolQueue.PQ.RequestThing(dcf.spawnThingy.tot);
                    create.AddAction(new CSpawn(thing));
                    create.AddAction(new CMovement(thing, dcf.mLoc.gizmoPseudoPos));
                    create.Do();
                    return false;
                }
            }

            if (menuButtons.load.ButtonIsClicked)
            {
                FileLoad fs = new FileLoad("SaveFile");
                fs.Load();
                return false;
            }

            if (menuButtons.save.ButtonIsClicked)
            {
                FileSave fs = new FileSave("SaveFile");
                fs.Save();
                return false;
            }

            return false;
        }

        return true;
    }

    bool TranslationMode()
    {
        Thingy t = dcf.mLoc.CastIntoScene(dcf.worldCam);
        if (t != null)
        {
            EnableTranslation trans = gameObject.AddComponent<EnableTranslation>();
            DeactivateOnSwitch();
            trans.SetEnableTranslation(dcf, this, dcf.modes.trans.activeOnSwitch, t, t.GetComponentsInChildren<MeshFilter>());
            return false;
        }
        return true;
    }
}
