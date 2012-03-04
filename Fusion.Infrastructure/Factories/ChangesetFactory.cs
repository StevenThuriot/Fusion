using Ninject;
using Fusion.Domain;
using ClientChangeset = Fusion.Domain.Changeset;
using ServerChangeset = Microsoft.TeamFoundation.VersionControl.Client.Changeset;

namespace Fusion.Infrastructure.Factories
{
    internal class ChangesetFactory : IChangesetFactory
    {
    	private readonly IChangeFactory _ChangeFactory;
    	private readonly IWorkItemFactory _WorkItemFactory;

    	public ChangesetFactory(IKernel kernel)
    	{
    		_ChangeFactory = kernel.Get<IChangeFactory>();
    		_WorkItemFactory = kernel.Get<IWorkItemFactory>();
    	}

    	public IChangeset Create(ServerChangeset changeset)
    	{
    		var changes = _ChangeFactory.Create(changeset.Changes);
    		var workitems = _WorkItemFactory.Create(changeset.WorkItems);

			return new ClientChangeset(changeset.ChangesetId, changeset.Comment, changeset.Committer, changes, workitems);
    	}
    }
}
