using UnityEngine;

namespace Standalone.Serialization {
	public interface ISave {
		void Save(Object obj, string fileName, bool overWrite = false);
	}
}