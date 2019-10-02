using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class Thingy : MonoBehaviour
{
    public TypeOfThingy tot { get; protected set; }

    private void Awake()
    {
        PseudoAwake();
        PoolQueue.PQ.AddThingToQueue(this);
    }

    public virtual void PseudoAwake()
    {
        gameObject.SetActive(true);
        TheManager.TM.Thingies.Add(this);
    }

    public virtual void PseudoDestroy()
    {
        gameObject.SetActive(false);
        TheManager.TM.Thingies.Remove(this);
    }
}

public enum TypeOfThingy
{
    Test1,
    Test2
}