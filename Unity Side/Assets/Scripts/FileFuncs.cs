using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

[StructLayout(LayoutKind.Sequential)]
public struct DataStruct
{
    public int ObjectType;
    //public int PoolNumber;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
    public float[] TransformData;
}

internal static class FileFuncs
{
    const string DLL_NAME = "EngineFilesaver";

    [DllImport(DLL_NAME)]
    internal static extern void SetFilePath(string fp);
    [DllImport(DLL_NAME)]
    internal static extern void SaveFileOpen(string filename, int version, int arrSize);
    [DllImport(DLL_NAME)]
    internal static extern void PackElement(DataStruct d, int position);
    [DllImport(DLL_NAME)]
    internal static extern void SaveFileClose();
    [DllImport(DLL_NAME)]
    internal static extern void LoadFileOpen(string filename);
    [DllImport(DLL_NAME)]
    internal static extern int GetVersion();
    [DllImport(DLL_NAME)]
    internal static extern int GetArrSize();
    [DllImport(DLL_NAME)]
    internal static extern DataStruct ExtractElement(int position);
    [DllImport(DLL_NAME)]
    internal static extern void LoadFileClose();
    [DllImport(DLL_NAME)]
    internal static extern void CloseAll();
}
