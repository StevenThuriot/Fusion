using System;

namespace Fusion.Domain
{
	public interface IWorkItem
	{
		int ID { get; }
		string Title { get; }
		string State { get; }
		string CreatedBy { get; }
		DateTime CreatedDate { get; }
		string AreaPath { get; }
		string IterationPath { get; }
		string Description { get; }
	}
}