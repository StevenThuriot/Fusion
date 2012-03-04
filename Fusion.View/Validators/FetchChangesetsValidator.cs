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
