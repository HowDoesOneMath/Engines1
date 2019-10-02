using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualQueue
{
    public IndividualQueue(System.Type poolType, TypeOfThingy thingType)
    {
        ttype = thingType;
        ptype = poolType;
        things = new List<Thingy>();
    }

    public System.Type ptype { get; private set; }
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
            if (thing.GetType() == IQS[i].ptype)
            {
                IQS[i].things.Add(thing);
                added = true;
                i = IQS.Count;
            }
        }

        if (!added)
        {
            IQS.Add(new IndividualQueue(thing.GetType(), thing.tot));
            IQS[IQS.Count - 1].things.Add(thing);
        }
    }

    public Thingy RequestThing(System.Type thingType)
    {
        for (int i = 0; i < IQS.Count; i++)
        {
            if (IQS[i].ptype == thingType)
            {
                int I = i;
                i = IQS.Count;
                for (int j = 0; j < IQS[I].things.Count; j++)
                {
                    if (!IQS[I].things[I].gameObject.activeSelf)
                    {
                        IQS[I].things[I].PseudoAwake();
                        return IQS[I].things[I];
                    }
                }

                Thingy thing = TheManager.TM.GetThingyOfType(thingType);
                return thing;
            }
        }

        return null;
    }

    public Thingy RequestThing(TypeOfThingy tot)
    {
        for (int i = 0; i < IQS.Count; i++)
        {
            if (IQS[i].ttype == tot)
            {
                System.Type stype = IQS[i].ptype;
                int I = i;
                i = IQS.Count;
                for (int j = 0; j < IQS[I].things.Count; j++)
                {
                    if (!IQS[I].things[j].gameObject.activeSelf)
                    {
                        IQS[I].things[j].PseudoAwake();
                        return IQS[I].things[j];
                    }
                }

                Thingy thing = TheManager.TM.GetThingyOfType(stype);
                return thing;
            }
        }

        return null;
    }
}
