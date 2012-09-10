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
using System;

namespace Fusion.Domain
{
	[Serializable]
	public class WorkItem : IWorkItem
	{
		public int ID { get; private set; }
		public string Description { get; private set; }
		public string Title { get; private set; }
		public string State { get; private set; }
		public string CreatedBy { get; private set; }
		public DateTime CreatedDate { get; private set; }
		public string AreaPath { get; private set; }
		public string IterationPath { get; private set; }

		public WorkItem(int id, string description, string title, string state, string createdBy, DateTime createdDate, string areaPath, string iterationPath)
		{
			ID = id;
			Description = description;
			Title = title;
			State = state;
			CreatedBy = createdBy;
			CreatedDate = createdDate;
			AreaPath = areaPath;
			IterationPath = iterationPath;
		}
	}
}
