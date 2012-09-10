#region License
// 
//  Copyright 2012 Steven Thuriot
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
#endregion
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
