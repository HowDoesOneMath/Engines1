using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Thingy : MonoBehaviour
{
    private void OnTransformParentChanged()
    {
        Debug.Log("CHANGED! " + name);
    }
}
