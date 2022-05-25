using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Engine.Tools.Serializer {
	public static class Serializer {
		public static string GetJsonString(Object obj) {
			return JsonUtility.ToJson(obj);
		}
		
		public static void Save(string saveName, string jsonString) {
			
			File.WriteAllText(AppPathDefinitions.Global.SaveLocation, jsonString);
		}
	}
}