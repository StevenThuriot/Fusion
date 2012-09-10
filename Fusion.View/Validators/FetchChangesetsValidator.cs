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
using Fusion.View.Properties;
using Fusion.View.ViewModel;
using Moon.Extensions;
using Nova.Base;
using Nova.Validation;

namespace Fusion.View.Validators
{
	public class FetchChangesetsValidator : BaseValidator<MainViewModel>
	{
		public FetchChangesetsValidator(ValidationResults results, ActionContext actionContext) 
			: base(results, actionContext)
		{
		}

		public override void Validate(MainViewModel entity)
		{
			if (string.IsNullOrWhiteSpace(entity.SelectedCollection))
			{
				AddRequired(Fields.Collections);
			}

			if (string.IsNullOrWhiteSpace(entity.SelectedProject))
			{
				AddRequired(Fields.Projects);
			}

			if (string.IsNullOrWhiteSpace(entity.SearchText))
			{
				AddRequired(Fields.WorkItemID);
			}

			int parsedValue;
			var parseSucceeded = Int32.TryParse(entity.SearchText, out parsedValue);
			if (parseSucceeded)
			{
				ActionContext.Add("ID", parsedValue);
			}
			else
			{
				var message = Resources.InvalidWorkItemIDText.FormatWith(entity.SearchText);
				Add(Fields.WorkItemID, message);
			}
		}
	}
}
