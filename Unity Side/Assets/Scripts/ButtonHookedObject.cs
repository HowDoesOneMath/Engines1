using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonHookedObject : MonoBehaviour
{
    public Thingy thingyToSpawn;
    public DirtyFlagController dcf;

    public void HookThisToController()
    {
        dcf.spawnThingy = thingyToSpawn;
    }
}
