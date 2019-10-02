#pragma once
#include "PluginSettings.h"
#include "FileSaver.h"

#ifdef __cplusplus
extern "C"
{
#endif
	PLUGIN_API void SetFilePath(char* fp);

	PLUGIN_API void SaveFileOpen(char* filename, int version, int arrSize);

	PLUGIN_API void PackElement(DataStruct d, int position);
	
	PLUGIN_API void SaveFileClose();

	PLUGIN_API void LoadFileOpen(char* filename);
	 
	PLUGIN_API int GetVersion();
	 
	PLUGIN_API int GetArrSize();
	 
	PLUGIN_API DataStruct ExtractElement(int position);
	 
	PLUGIN_API void LoadFileClose();

	PLUGIN_API void CloseAll();

#ifdef __cplusplus
}
#endif