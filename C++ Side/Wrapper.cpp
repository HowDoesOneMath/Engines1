#include "Wrapper.h"

FileSave fs;

PLUGIN_API void SetFilePath(char* fp)
{
	fs.SetFilePath(fp);
}

PLUGIN_API void SaveFileOpen(char* filename, int version, int arrSize)
{
	fs.SaveFileOpen(filename, version, arrSize);
}

PLUGIN_API void PackElement(DataStruct d, int position)
{
	fs.PackElement(d, position);
}

PLUGIN_API void SaveFileClose()
{
	fs.SaveFileClose();
}

PLUGIN_API void LoadFileOpen(char* filename)
{
	fs.LoadFileOpen(filename);
}

PLUGIN_API int GetVersion()
{
	return fs.GetVersion();
}

PLUGIN_API int GetArrSize()
{
	return fs.GetArrSize();
}

PLUGIN_API DataStruct ExtractElement(int position)
{
	return fs.ExtractElement(position);
}

PLUGIN_API void LoadFileClose()
{
	fs.LoadFileClose();
}

PLUGIN_API void CloseAll()
{
	return fs.CloseAll();
}
