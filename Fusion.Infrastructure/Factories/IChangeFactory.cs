using Moon.Factories;
using Fusion.Domain;
using Change = Microsoft.TeamFoundation.VersionControl.Client.Change;

namespace Fusion.Infrastructure.Factories
{
	internal interface IChangeFactory : IFactory<Change, IChange>
	{
	}
}