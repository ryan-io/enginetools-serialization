using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Engine.Tools.Serializer {
	public static class Serializer {
		public readonly struct JsonJob {
			public SerializerSetup Setup      { get; }
			public string          SaveName   { get; }
			public string          JsonString { get; }

			public JsonJob(SerializerSetup setup, string saveName, string jsonString) {
				Setup      = setup;
				SaveName   = saveName;
				JsonString = jsonString;
			}
		}

		public readonly struct TxtJob {
			public SerializerSetup Setup      { get; }
			public string          SaveName   { get; }
			public byte[]          Data       { get; }
			public string          FileFormat { get; }

			public TxtJob(SerializerSetup setup, string saveName, byte[] data, string fileFormat) {
				Setup      = setup;
				SaveName   = saveName;
				Data       = data;
				FileFormat = fileFormat;
			}
		}

		public static readonly string DefaultDirectory = Application.dataPath + "/" + DefaultFolder + "/";

		public const string DefaultFolder = "SerializedData";

		public static string GetJsonString(Object obj) {
			return JsonUtility.ToJson(obj);
		}

		public static void SaveJson(JsonJob job, bool sanitizeName = true) {
			var name = job.SaveName;
			if (sanitizeName)
				name = InternalSanitizeName(job.SaveName);

			Debug.Log($"Saving to: {job.Setup.SaveLocation + name}");

			File.WriteAllText(job.Setup.SaveLocation + name, job.JsonString);
		}

		public static void SaveBytesData(TxtJob job, bool sanitizeName) {
			var name = job.SaveName;

			var location = job.Setup.SaveLocation + name + job.FileFormat;
			Debug.Log($"Saving to: {location}");

			File.WriteAllBytes(location, job.Data);
		}

		public static bool TryLoadBytesData(string filePathFull, out byte[] data) {
			var exists = File.Exists(filePathFull);

			if (!exists) {
				Debug.LogWarning($"Could not find a file at: {filePathFull}");
				data = default;
				return false;
			}

			data = File.ReadAllBytes(filePathFull);
			return true;
		}

		public static void EnsureDirectoryExists(string folderName) {
			if (!Directory.Exists(folderName))
				Directory.CreateDirectory(folderName);

			if (!Directory.Exists(DefaultDirectory)) {
				Debug.Log($"Could not find the default directory ({DefaultDirectory}. Now creating...");
				Directory.CreateDirectory(DefaultDirectory);
			}
		}

		public static string InternalSanitizeName(string value) {
			var sanitizedString = Sanitizer.Sanitize(value);
			return sanitizedString == string.Empty ? "stringWasEmpty" : sanitizedString;
		}
	}
}