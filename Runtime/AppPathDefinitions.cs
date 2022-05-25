using System.Diagnostics;
using System.IO;
using Engine.Tools.Utility;
using Sirenix.OdinInspector;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Engine.Tools.Serializer {
	/// <summary>
	/// The 'brain' of saving & loading persistent data within the Engine environment.
	/// </summary>
	public class AppPathDefinitions : Singleton<AppPathDefinitions, AppPathDefinitions.IAppPath>,
	                                  AppPathDefinitions.IAppPath {
		/// <summary>
		/// Save location. This lies within the Unity Application Data Path directory.
		/// The serializedfield '_saveFolderName' should default to "Saves'.
		/// There is currently no need to change this name, but it is exposed in the inspector if data should
		/// be saved in a different location that "Saves".
		/// All strings will be sanitized.
		/// This is NOT encrypted as of 17 March, 2022.
		/// </summary>
		string IAppPath.SaveLocation {
			get {
				_saveFolderNameSanitized = InternalSanitizeFolder(_config.DefaultSaveFolder);

				if (!Directory.Exists(AppPath))
					Directory.CreateDirectory(AppPath);

				return AppPath;
			}
		}

		public bool EnforceInScene() {
			if (!Global.IsNull())
				return true;

			AddObjectToScene();
			return false;
		}

		static void AddObjectToScene() {
			var obj = new GameObject(ApplicationConfig.DefaultGameObjectName);
			obj.AddComponent<AppPathDefinitions>();
		}

		string InternalSanitizeFolder(string value) {
			var sanitizedString = Sanitizer.Sanitize(value);
			return sanitizedString == string.Empty ? _config.DefaultSaveFolder : sanitizedString;
		}

		string AppPath => Application.dataPath + $"/{_saveFolderNameSanitized}/";

		[Button]
		void OpenDirectory() {
			try {
				var info = new ProcessStartInfo(Global.SaveLocation, ApplicationConfig.Explorer);
				Process.Start(info);
			}
			catch (DirectoryNotFoundException) {
				Debug.LogError(ApplicationConfig.NoDirectoryFound);
			}
		}

		[SerializeField] ApplicationConfig _config;
		string                           _saveFolderNameSanitized;

		public interface IAppPath {
			string SaveLocation { get; }
		}
	}
}