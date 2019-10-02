#pragma once

#ifdef FILESAVERPLUGIN_EXPORTS
#define PLUGIN_API __declspec(dllexport)
#elif FILESAVERPLUGIN_IMPORTS
#define PLUGIN_API __declspec(dllimport)
#else
#define PLUGIN_API
#endif