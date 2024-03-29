﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Thingy : MonoBehaviour
{
    [SerializeField]
    TypeOfThingy tt = TypeOfThingy.Test1;
    public TypeOfThingy tot { get { return tt; } }
    public bool thingyActive { get; set; } = false;

    public Material myMat { get; set; }

    private void Awake()
    {
        myMat = GetComponentInChildren<MeshRenderer>().material;
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
            GetComponentInChildren<MeshRenderer>().material = myMat;
            Flip();
            gameObject.SetActive(false);
            TheManager.TM.Thingies.Remove(this);
        }
    }

    public void Flip()
    {
        thingyActive = !thingyActive;
    }

    public virtual bool Deletable()
    {
        return true;
    }
}

public enum TypeOfThingy
{
    Test1,
    Test2,
    Tree,
    StreetLamp,
    Gate,
    Fence_Metal,
    Cactus,
    BrokenTower,
    WaterTower,
    Rock1,
    Rock2,
    Rock3,
    Player,
    Goal
}