using System;
using UnityEngine;

namespace Engine.Tools.Serializer {
	[Serializable]
	public class ApplicationConfig {
		public string DefaultSaveFolder => _defaultSaveFolder;
		public string DefaultSaveFormat => _defaultSaveFormat;

		public static readonly string DefaultGameObjectName = "AppPathsDefinitions";

		public static readonly string NoAppPathDefinitionsFound =
			"AppPathDefinitions was not found in scene. A new GO has been created.";

		public static readonly string Explorer = "explorer.exe";

		public static readonly string NoDirectoryFound =
			"Directory does not exist. The codebase needs to be debugged. There are fallbacks to create the directory if it cannot be found.";

		[SerializeField] string _defaultSaveFolder = "Saves";
		[SerializeField] string _defaultSaveFormat = ".txt";
	}
}