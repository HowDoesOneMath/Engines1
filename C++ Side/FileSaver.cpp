#include "FileSaver.h"
#include <sstream>

void FileSave::SetFilePath(char* fp)
{
	filePath.clear();

	filePath.append(fp);
}

void FileSave::SaveFileOpen(char* filename, int version, int arrSize)
{
	if (!saveFile.is_open() && !loadFile.is_open())
	{
		ClearMem();

		std::string path = filePath;
		path.append(filename);

		saveFile.open(filename);

		if (saveFile.is_open())
		{
			datFile.versionNum = version;
			datFile.arrSize = arrSize;

			datFile.data = new DataStruct[datFile.arrSize];
		}
	}
}

void FileSave::PackElement(DataStruct d, int position)
{
	if (saveFile.is_open() && !loadFile.is_open())
	{
		datFile.data[position] = d;
	}
}

void FileSave::SaveFileClose()
{
	if (saveFile.is_open() && !loadFile.is_open())
	{
		saveFile << std::to_string(datFile.versionNum) << "\n" << std::to_string(datFile.arrSize) << "\n";
		for (int i = 0; i < datFile.arrSize; ++i)
		{
			saveFile << std::to_string(datFile.data[i].ObjectType) << "\n";
			saveFile << std::to_string(datFile.data[i].PoolNumber) << "\n";
			for (int j = 0; j < 9; ++j)
			{
				saveFile << std::to_string(datFile.data[i].TransformData[j]) << "/";
			}
			saveFile << "\n";
		}

		saveFile << "END";

		saveFile.close();

		ClearMem();
	}
}

void FileSave::LoadFileOpen(char* filename)
{
	if (!loadFile.is_open() && !saveFile.is_open())
	{
		ClearMem();

		std::string path = filePath;
		path.append(filename);

		loadFile.open(path);

		if (loadFile.is_open())
		{
			std::string parser;

			if (std::getline(loadFile, parser))
			{
				int vnum = std::stoi(parser);

				switch (vnum)
				{
				case 1:
					LoadVersion1(&loadFile, &parser);
					break;
				}
			}
		}
	}
}

int FileSave::GetVersion()
{
	if (loadFile.is_open() || saveFile.is_open())
	{
		return datFile.versionNum;
	}
}

int FileSave::GetArrSize()
{
	if (loadFile.is_open() || saveFile.is_open())
	{
		return datFile.arrSize;
	}
}

DataStruct FileSave::ExtractElement(int position)
{
	if (loadFile.is_open() || saveFile.is_open())
	{
		return datFile.data[position];
	}
}

void FileSave::LoadFileClose()
{
	if (loadFile.is_open() && !saveFile.is_open())
	{
		ClearMem();

		loadFile.close();
	}
}



void FileSave::ClearMem()
{
	datFile.versionNum = -1;

	if (datFile.arrSize >= 0)
	{
		delete[] datFile.data;
		datFile.arrSize = -1;
	}
}

//Expected format:
//
//VersionNumber (int) - the version of the file
//arrSize (int) - the size of the array of object data it's expecting
//ObjectType (int) - the type of object to be loaded				< ELEMENT OF STRUCT ARRAY
//PoolNumber (int) - the number it was given in the object pool		< ELEMENT OF STRUCT ARRAY
//TransformData (float[9]) - the number								< ELEMENT OF STRUCT ARRAY
//END - end of the file, unnecessary for now

void FileSave::LoadVersion1(std::ifstream* loadFile, std::string* parser)
{
	datFile.versionNum = 1;
	std::getline(*loadFile, *parser);
	datFile.arrSize = std::stoi(*parser);

	datFile.data = new DataStruct[datFile.arrSize];
	for (int i = 0; i < datFile.arrSize; ++i)
	{
		std::getline(*loadFile, *parser);
		datFile.data[i].ObjectType = std::stoi(*parser);
		std::getline(*loadFile, *parser);
		datFile.data[i].PoolNumber = std::stoi(*parser);

		std::getline(*loadFile, *parser);
		std::stringstream ss(*parser);

		int count = 0;
		while (std::getline(ss, *parser, '/') && count < 9)
		{
			datFile.data[i].TransformData[count] = std::stof(*parser); ++count;
		}
	}
}
