﻿using System;

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
