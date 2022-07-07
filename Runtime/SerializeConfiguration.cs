using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using TextAsset = UnityEngine.TextCore.Text.TextAsset;

namespace Engine.Tools.Serializer {
	[Serializable, Title("Serialization Configuration")]
	public class SerializeConfiguration {
		[field: SerializeField, InfoBox(SerializedDataInfo)]
		public TextAsset SerializedData { get; private set; }

		[field: SerializeField] public string SaveFolder { get; set; } = "SerializedData";

		[field: SerializeField, ValueDropdown("GetFormats")]
		public string SaveFormat { get; private set; } = ".txt";

		public static readonly string DefaultGameObjectName = "AppPathsDefinitions";

		public static readonly string NoAppPathDefinitionsFound =
			"AppPathDefinitions was not found in scene. A new GO has been created.";

		public static readonly string Explorer = "explorer.exe";

		public static readonly string NoDirectoryFound =
			"Directory does not exist. The codebase needs to be debugged. There are fallbacks to create the directory if it cannot be found.";

		static IEnumerable GetFormats() {
			var formats = new HashSet<string> {
				".txt",
				".json",
				".bytes",
				".omni"
			};

			return formats;
		}
		
		const string SerializedDataInfo =
			"Verify the seed value in the 'SerializedData' file name match with the Mapsolver seed.";
	}
}