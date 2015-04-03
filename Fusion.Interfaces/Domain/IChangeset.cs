using System;
using System.Collections.Generic;

namespace Fusion.Domain
{
    public interface IChangeset : IComparable
    {
        string ID { get; }
        string Comment { get; }
    	string Committer { get; }
    	IEnumerable<IChange> Changes { get; }
    	IEnumerable<IWorkItem> Workitems { get; }
    }
}