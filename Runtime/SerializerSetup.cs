using System;
using Sirenix.OdinInspector;
using UnityEngine;
using TextAsset = UnityEngine.TextCore.Text.TextAsset;

namespace Engine.Tools.Serializer {
	/// <summary>
	/// The 'brain' of saving & loading persistent data within the Engine environment.
	/// </summary>
	public class SerializerSetup : MonoBehaviour {
		/// <summary>
		/// Save location. This lies within the Unity Application Data Path directory.
		/// The serializedfield '_saveFolderName' should default to "Saves'.
		/// There is currently no need to change this name, but it is exposed in the inspector if data should
		/// be saved in a different location that "Saves".
		/// All strings will be sanitized.
		/// This is NOT encrypted as of 17 March, 2022.
		/// </summary>

		public string SaveLocation {
			get {
				Serializer.EnsureDirectoryExists(AppPath);

				if (string.IsNullOrWhiteSpace(_config.SaveFolder))
					_config.SaveFolder = Serializer.DefaultFolder;

				return AppPath;
			}
		}

		public string FileFormat => _config.SaveFormat;

		void OnValidate() {
			
		}

		bool HasNoConfigData => !_config.SerializedData;
		
		string AppPath => Application.dataPath + $"/{_config.SaveFolder}/";

		[SerializeField, HideLabel] SerializeConfiguration _config;
		string                                             _saveFolderNameSanitized;
	}
}