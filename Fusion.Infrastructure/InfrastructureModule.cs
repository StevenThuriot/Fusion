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
using Ninject.Modules;
using Fusion.Infrastructure.Factories;
using Fusion.Infrastructure.Gateways;

namespace Fusion.Infrastructure
{
    public class InfrastructureModule : NinjectModule
    {
        public override void Load()
		{
			Bind<IChangeFactory>().To<ChangeFactory>().InSingletonScope();
			Bind<IWorkItemFactory>().To<WorkItemFactory>().InSingletonScope();
            Bind<IChangesetFactory>().To<ChangesetFactory>().InSingletonScope();
            Bind<ITfsGateway>().To<TfsGateway>().InSingletonScope();
        }
    }
}
