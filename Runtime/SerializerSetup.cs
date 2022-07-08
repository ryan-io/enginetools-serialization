using System;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

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
		///
		/// 

		public string SaveRoot {
			get {
				Serializer.EnsureDirectoryExists(AppPathRoot, _config.SaveFolderRoot);
				return AppPathRoot;
			}
		}
		
		public string SaveLocation {
			get {
				Serializer.EnsureDirectoryExists(AppPath, _config.SaveFolderRoot);
				return AppPath;
			}
		}

		void OnValidate() {
			if (_config == null || Application.isPlaying)
				return;
			
			if (string.IsNullOrWhiteSpace(_config.SaveFolderRoot))
				_config.SaveFolderRoot = Serializer.DefaultRoot;
			
			if (string.IsNullOrWhiteSpace(_config.SaveFolder))
				_config.SaveFolder = Serializer.DefaultFolder;
		}

		public string FileFormat => _config.SaveFormat;

		string AppPath => AppPathRoot + $"{_config.SaveFolder}/";
		
		string AppPathRoot => Application.dataPath + "/" + _config.SaveFolderRoot +"/";

		[SerializeField, HideLabel] SerializeConfiguration _config;
		string                                             _saveFolderNameSanitized;
	}
}