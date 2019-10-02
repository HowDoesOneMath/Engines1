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

        TheManager.TM.DestroyAll();

        for (int i = FileFuncs.GetArrSize() - 1; i >= 0; --i)
        {
            DataStruct d = FileFuncs.ExtractElement(i);

            //Debug.Log(d.ObjectType);

            Thingy thing = PoolQueue.PQ.RequestThing((TypeOfThingy)d.ObjectType);

            thing.transform.position = new Vector3(d.TransformData[0], d.TransformData[1], d.TransformData[2]);
            thing.transform.rotation = Quaternion.Euler(d.TransformData[3], d.TransformData[4], d.TransformData[5]);
            thing.transform.localScale = new Vector3(d.TransformData[6], d.TransformData[7], d.TransformData[8]);
        }

        FileFuncs.LoadFileClose();
    }
}
