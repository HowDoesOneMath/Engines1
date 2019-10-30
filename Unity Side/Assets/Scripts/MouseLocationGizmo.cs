using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocationGizmo : MonoBehaviour
{
    public bool mouseHit { get; private set; } = false;
    public bool wireFrame { get; private set; } = false;
    public bool isInTranslate { get; set; } = false;
    Transform cam = null;
    MeshCollider mc;
    Mesh me;

    public Vector3 gizmoPseudoPos { get; set; } = Vector3.zero;

    public Vector3 gizmoPos { get; set; } = Vector3.zero;
    public Quaternion gizmoRot { get; set; } = Quaternion.identity;
    public Vector3 gizmoScale { get; set; } = Vector3.one;

    float mouseDistance = 0;
    public float scale = 0.1f;

    public MeshCollider MC
    {
        get {
            if (mc == null)
            {
                mc = GetComponent<MeshCollider>();
                if (mc == null)
                    mc = gameObject.AddComponent<MeshCollider>();
            }
            return mc;
        }
    }

    public Thingy CastIntoScene(Camera raycam)
    {
        Ray mouseray = raycam.ScreenPointToRay(Input.mousePosition);
        
        cam = raycam.transform;
        RaycastHit rch;
        Thingy targettedObject = null;
        mouseHit = false;
        wireFrame = false;

        for (int i = TheManager.TM.Thingies.Count - 1; i >= 0; --i)
        {
            MeshFilter[] m = TheManager.TM.Thingies[i].GetComponentsInChildren<MeshFilter>();

            for (int j = m.Length - 1; j >= 0; --j)
            {
                MeshCollider MeCo = TheManager.TM.Thingies[i].gameObject.AddComponent<MeshCollider>();
                MeCo.sharedMesh = m[j].sharedMesh;
                if (MeCo.Raycast(mouseray, out rch, 10000f))
                {
                    //Debug.Log(TheManager.TM.Thingies[i].name);
                    if (targettedObject == null)
                    {
                        targettedObject = TheManager.TM.Thingies[i];
                        mouseDistance = rch.distance;
                        gizmoPseudoPos = rch.point;
                        gizmoPos = m[j].transform.position;
                        gizmoRot = m[j].transform.rotation;
                        gizmoScale = m[j].transform.localScale;
                        me = m[j].sharedMesh;
                        mouseHit = true;
                        wireFrame = true;
                    }
                    else if (mouseDistance > rch.distance)
                    {
                        targettedObject = TheManager.TM.Thingies[i];
                        mouseDistance = rch.distance;
                        gizmoPseudoPos = rch.point;
                        gizmoPos = m[j].transform.position;
                        gizmoRot = m[j].transform.rotation;
                        gizmoScale = m[j].transform.localScale;
                        me = m[j].sharedMesh;
                    }
                }

                Destroy(MeCo);
            }

        }

        return targettedObject;
    }

    private void Update()
    {
        if (cam != null)
        {
            transform.position = gizmoPseudoPos;
            transform.localScale = Vector3.one * scale * (gizmoPseudoPos - cam.position).magnitude;
        }
    }

    public bool CastIntoAll(Camera raycam)
    {
        float dist = 0f;
        cam = raycam.transform;

        RaycastHit[] rch;

        rch = Physics.RaycastAll(raycam.ScreenPointToRay(Input.mousePosition));

        mouseHit = false;

        for (int i = 0; i < rch.Length; ++i)
        {
            if (!mouseHit)
            {
                gizmoPseudoPos = rch[i].point;
                dist = rch[i].distance;
                mouseHit = true;
            }
            else if (dist > rch[i].distance)
            {
                gizmoPseudoPos = rch[i].point;
                dist = rch[i].distance;
            }
        }

        return mouseHit;
    }

    //private void OnDrawGizmos()
    //{
    //    if (gameObject.activeInHierarchy && mouseHit)
    //    {
    //        if (wireFrame && isInTranslate)
    //            Gizmos.DrawWireMesh(me, gizmoPos, gizmoRot, gizmoScale);
    //        Gizmos.color = Color.white;
    //        Gizmos.DrawSphere(gizmoPseudoPos, scale * (gizmoPseudoPos - cam.position).magnitude);
    //    }
    //}
}
