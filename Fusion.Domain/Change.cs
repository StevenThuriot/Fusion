using System;

namespace Fusion.Domain
{
	[Serializable]
	public class Change : IChange
	{
		public string ChangeType { get; private set; }
		public string ItemType { get; private set; }
		public string Path { get; private set; }

		public Change(string changeType, string itemType, string path)
		{
			ChangeType = changeType;
			ItemType = itemType;
			Path = path;
		}
	}
}
