namespace Fusion.Domain
{
	public interface IChange
	{
		string ChangeType { get; }
		string ItemType { get; }
		string Path { get; }
	}
}
