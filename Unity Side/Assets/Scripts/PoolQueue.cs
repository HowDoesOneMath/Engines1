using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualQueue
{
    public IndividualQueue(TypeOfThingy thingType)
    {
        ttype = thingType;
        things = new List<Thingy>();
    }

    public TypeOfThingy ttype { get; private set; }

    public List<Thingy> things { get; private set; }
}

public class PoolQueue
{
    static PoolQueue pq;
    public static PoolQueue PQ
    {
        get { if (pq == null){ pq = new PoolQueue(); } return pq; }
        private set { pq = value; }
    }

    List<IndividualQueue> iqs;
    public List<IndividualQueue> IQS
    {
        get { if (iqs == null) { iqs = new List<IndividualQueue>(); } return iqs; }
        private set { iqs = value; }
    }

    public void AddThingToQueue(Thingy thing)
    {
        bool added = false;

        for (int i = 0; i < IQS.Count; i++)
        {
            if (thing.tot == IQS[i].ttype)
            {
                IQS[i].things.Add(thing);
                added = true;
                i = IQS.Count;
            }
        }

        if (!added)
        {
            IQS.Add(new IndividualQueue(thing.tot));
            IQS[IQS.Count - 1].things.Add(thing);
        }
    }

    public Thingy RequestThing(TypeOfThingy tot)
    {
        for (int i = 0; i < IQS.Count; i++)
        {
            if (IQS[i].ttype == tot)
            {
                int I = i;
                i = IQS.Count;
                return RequestFromSlot(tot, I);
            }
        }

        IQS.Add(new IndividualQueue(tot));
            return RequestFromSlot(tot, IQS.Count - 1);
    }

    Thingy RequestFromSlot(TypeOfThingy tot, int i)
    {
        for (int j = 0; j < IQS[i].things.Count; j++)
        {
            if (!IQS[i].things[j].gameObject.activeSelf)
            {
                IQS[i].things[j].PseudoAwake();
                return IQS[i].things[j];
            }
        }

        Thingy thing = TheManager.TM.GetThingyOfType(IQS[i].ttype);
        return thing;
    }
}
