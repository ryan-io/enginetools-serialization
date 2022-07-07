using System.IO;
using UnityEngine;

namespace Engine.Tools.Serializer {
	public static class AstarSerializer {
		/// <summary>
		/// Please ensure that your active graph data has been scanned.
		/// </summary>
		public static void SerializeCurrentAstarGraph(SerializerSetup setup, string name) {
			if (string.IsNullOrWhiteSpace(name)) {
				name = $"DefaultAstar";
			}

			var settings = new Pathfinding.Serialization.SerializeSettings {
				nodes          = true,
				editorSettings = true
			};

			var bytes            = AstarPath.active.data.SerializeGraphs(settings);
			var serializationJob = new Serializer.TxtJob(setup, name, bytes, setup.FileFormat);
			Serializer.SaveBytesData(serializationJob, true);
		}

		public static void DeserializeAstarGraph(AstarDeserializationJob job) {
			var output = job.DataPath + Prefix + job.Seed + "_luid" + job.Iteration + ".txt";
			var hasData   = Serializer.TryLoadBytesData(output, out var data);
			
			if (hasData)
				AstarPath.active.data.DeserializeGraphs(data);
		}
		
		public const string Prefix = "AstarGraph_";

		public readonly struct AstarDeserializationJob {
			public string DataPath  { get; }
			public string Seed      { get; }
			public int    Iteration { get; }

			public AstarDeserializationJob(string dataPath, string seed, int iteration) {
				DataPath       = dataPath;
				Seed           = seed;
				Iteration = iteration;
			}
		}
	}
}