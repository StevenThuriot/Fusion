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
using System.Linq;
using System.Text;
using System.Windows;
using Nova.Base;
using Fusion.View.Entities;
using Fusion.View.Properties;
using Fusion.View.ViewModel;
using Fusion.View.Views;

namespace Fusion.View.Actions.Main
{
	public class CopyAction : BaseAction<MainWindow, MainViewModel>
	{
		private string _Message;

		public override bool Execute()
		{
			var mode = ActionContext.GetValue<CopyMode>("CommandParameter");

			switch (mode)
			{
				case CopyMode.ID:
					CopyID();
					break;
				case CopyMode.Comment:
					CopyComment();
					break;
				case CopyMode.Excel:
					CopyForExcel();
					break;
				default:
					return false;
			}

			return true;
		}

		public override void ExecuteCompleted()
		{
			Clipboard.SetText(_Message);
		}

		private void CopyForExcel()
		{
			var builder = new StringBuilder();

			builder.Append(Resources.ID)
				   .Append("\t")
				   .Append(Resources.Changeset)
				   .Append("\t")
				   .Append(Resources.Merged)
				   .Append("\t")
				   .Append(Environment.NewLine);

			foreach (var changeset in ViewModel.Changesets)
			{
				builder.Append(changeset.ID)
					   .Append("\t")
					   .Append(changeset.Comment.Replace("\r\n", " ").Replace("\n", " "))
					   .Append("\t")
					   .Append(changeset.IsDeleted ? Resources.Deleted : changeset.IsCompleted ? Resources.Yes : Resources.No)
					   .Append("\t")
					   .Append(Environment.NewLine);
			}

			_Message = builder.ToString();

		}

		private void CopyComment()
		{
			var selectedChangeset = ViewModel.Changesets.FirstOrDefault(x => x.IsSelected);

			if (selectedChangeset != null)
			{
				_Message = selectedChangeset.Comment;
			}
		}

		private void CopyID()
		{
			var selectedChangeset = ViewModel.Changesets.FirstOrDefault(x => x.IsSelected);

			if (selectedChangeset != null)
			{
				_Message = selectedChangeset.ID;
			}
		}
	}
}
