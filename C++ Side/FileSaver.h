#pragma once
#include "PluginSettings.h"
#include <vector>
#include <fstream>
#include <string>

struct PLUGIN_API DataStruct
{
	int ObjectType;
	//int PoolNumber;

	float TransformData[9];
};


struct PLUGIN_API FileData
{
	int versionNum;

	DataStruct* data;
	int arrSize;
};

class PLUGIN_API FileSave
{
	std::ofstream saveFile;
	std::ifstream loadFile;

	FileData datFile;
	//bool initializedFD = false;
	std::vector<DataStruct> ds;
	std::string filePath;

	//int arrSize = -1;

public:
	void SetFilePath(char* fp);

	void SaveFileOpen(char* filename, int version, int arrSize);

	void PackElement(DataStruct d, int position);

	void SaveFileClose();

	void LoadFileOpen(char* filename);

	int GetVersion();

	int GetArrSize();

	DataStruct ExtractElement(int position);

	void LoadFileClose();

	void ClearMem();

	void CloseAll();

	void LoadVersion1(std::ifstream* loadFile, std::string* parser);
};