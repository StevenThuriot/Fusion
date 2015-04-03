using Fusion.Domain;
using ClientChange = Fusion.Domain.Change;
using ServerChange = Microsoft.TeamFoundation.VersionControl.Client.Change;

namespace Fusion.Infrastructure.Factories
{
	internal class ChangeFactory : BaseFactory<ServerChange, IChange>, IChangeFactory
	{
		public override IChange Create(ServerChange change)
		{
			return new ClientChange(change.ChangeType.ToString(), change.Item.ItemType.ToString(), change.Item.ServerItem);
		}
	}
}