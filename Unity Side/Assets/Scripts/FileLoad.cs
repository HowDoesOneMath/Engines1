using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileLoad : BaseCommand
{
    string fname;

    public FileLoad(string fileName)
    {
        fname = fileName;
    }

    public void Load()
    {
        FileFuncs.LoadFileOpen(fname);

        BaseCommand bc = new BaseCommand();

        //TheManager.TM.DestroyAll();
        for (int i = TheManager.TM.Thingies.Count - 1; i >= 0; --i)
        {
            bc.AddAction(new CDelete(TheManager.TM.Thingies[i]));
        }

        for (int i = FileFuncs.GetArrSize() - 1; i >= 0; --i)
        {
            DataStruct d = FileFuncs.ExtractElement(i);

            //Debug.Log(d.ObjectType);

            Thingy thing = PoolQueue.PQ.RequestThing((TypeOfThingy)d.ObjectType);

            bc.AddAction(new CSpawn(thing));

            bc.AddAction(new CMovement(thing, new Vector3(d.TransformData[0], d.TransformData[1], d.TransformData[2])));
            bc.AddAction(new CRotate(thing, Quaternion.Euler(d.TransformData[3], d.TransformData[4], d.TransformData[5])));
            bc.AddAction(new CScale(thing, new Vector3(d.TransformData[6], d.TransformData[7], d.TransformData[8])));

            //thing.transform.position = new Vector3(d.TransformData[0], d.TransformData[1], d.TransformData[2]);
            //thing.transform.rotation = Quaternion.Euler(d.TransformData[3], d.TransformData[4], d.TransformData[5]);
            //thing.transform.localScale = new Vector3(d.TransformData[6], d.TransformData[7], d.TransformData[8]);
        }

        bc.Do();

        FileFuncs.LoadFileClose();
    }
}
