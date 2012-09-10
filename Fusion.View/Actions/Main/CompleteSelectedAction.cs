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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using Nova.Base;
using Fusion.Domain;
using Fusion.View.ViewModel;
using Fusion.View.Views;
using RESX = Fusion.View.Properties.Resources;

namespace Fusion.View.Actions.Main
{
	public class CompleteSelectedAction : BaseAction<MainWindow, MainViewModel>
	{
		public override void ExecuteCompleted()
		{
			var selectedChangesets = ViewModel.Changesets.Where(x => x.IsSelected && !x.IsCompleted).ToList();
			
			if (ViewModel.Configuration.EnableClipboard)
				CopyToClipboard(selectedChangesets);

			selectedChangesets.ForEach(x => x.Complete());
		}

		private void CopyToClipboard(IEnumerable<IChangeset> selectedChangesets)
		{
			var ids = BuildMessage(selectedChangesets.Select(x => x.ID).ToList());
			var comments = BuildMessage(selectedChangesets.Select(x => "\"" + x.Comment + "\"").ToList());
			var idAndComments = BuildMessage(selectedChangesets.Select(x => x.ID + " (\"" + x.Comment + "\")").ToList());

			string message = ViewModel.Configuration.UseCustomMergeMessage
			                 	? ViewModel.Configuration.CustomMergeMessage
			                 	: RESX.MergeMessage;

			var clipboardMessage = Regex.Replace(message, "%ID%", ids, RegexOptions.IgnoreCase);
			clipboardMessage = Regex.Replace(clipboardMessage, "%Comment%", comments, RegexOptions.IgnoreCase);
			clipboardMessage = Regex.Replace(clipboardMessage, "%IDAndComment%", idAndComments, RegexOptions.IgnoreCase);

			Clipboard.SetText(clipboardMessage);
		}

		private static string BuildMessage(IList<string> selectedStrings)
		{
			var builder = new StringBuilder();

			for (int i = 0; i < selectedStrings.Count; i++)
			{
				var selectedString = selectedStrings[i];
				builder.Append(selectedString);

				if (i == selectedStrings.Count - 2)
				{
					builder.Append(RESX.And);
				}
				else if (i != selectedStrings.Count - 1)
				{
					builder.Append(RESX.Comma);
				}
			}

			return builder.ToString();
		}
	}
}
