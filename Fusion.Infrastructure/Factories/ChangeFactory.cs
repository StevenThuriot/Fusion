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
using Moon.Factories;
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