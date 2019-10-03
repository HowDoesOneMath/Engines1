using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileSave
{
    string fname;
    public FileSave(string fileName)
    {
        fname = fileName;
    }

    public void Save()
    {
        FileFuncs.SaveFileOpen(fname, 1, TheManager.TM.Thingies.Count);

        #region PACK_ELEMENTS
        for (int i = 0; i < TheManager.TM.Thingies.Count; i++)
        {
            DataStruct data;
            data.ObjectType = (int)TheManager.TM.Thingies[i].tot;
            data.TransformData = new float[9];

            Vector3 pos = TheManager.TM.Thingies[i].transform.position;
            Vector3 rot = TheManager.TM.Thingies[i].transform.rotation.eulerAngles;
            Vector3 scl = TheManager.TM.Thingies[i].transform.localScale;

            data.TransformData[0] = pos.x;
            data.TransformData[1] = pos.y;
            data.TransformData[2] = pos.z;

            data.TransformData[3] = rot.x;
            data.TransformData[4] = rot.y;
            data.TransformData[5] = rot.z;

            data.TransformData[6] = scl.x;
            data.TransformData[7] = scl.y;
            data.TransformData[8] = scl.z;

            FileFuncs.PackElement(data, i);
        }
        #endregion

        FileFuncs.SaveFileClose();
    }
}
