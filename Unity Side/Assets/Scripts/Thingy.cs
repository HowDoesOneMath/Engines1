using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class Thingy : MonoBehaviour
{
    [SerializeField]
    TypeOfThingy tt = TypeOfThingy.Test1;
    public TypeOfThingy tot { get { return tt; } }
    public bool thingyActive { get; set; } = false;

    private void Awake()
    {
        PseudoAwake();
        PoolQueue.PQ.AddThingToQueue(this);
    }

    public void PseudoAwake()
    {
        if (!thingyActive)
        {
            Flip();
            gameObject.SetActive(true);
            TheManager.TM.Thingies.Add(this);
        }
    }

    public void PseudoDestroy()
    {
        if (thingyActive)
        {
            Flip();
            gameObject.SetActive(false);
            TheManager.TM.Thingies.Remove(this);
        }
    }

    public void Flip()
    {
        thingyActive = !thingyActive;
    }
}

public enum TypeOfThingy
{
    Test1,
    Test2
}