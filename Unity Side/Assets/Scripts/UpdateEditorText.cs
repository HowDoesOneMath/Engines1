using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UpdateEditorText : MonoBehaviour
{
    Text tex;
    public DirtyFlagController dcf;

    void Update()
    {
        if (tex == null)
            tex = GetComponent<Text>();
        if (dcf.spawnThingy != null)
            tex.text = dcf.spawnThingy.name;
    }
}
