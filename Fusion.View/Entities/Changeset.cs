using System.Collections.Generic;
using Moon.WPF;
using Fusion.Domain;
using System;

namespace Fusion.View.Entities
{
	/// <summary>
	/// A ViewModel interpretation of a Changeset.
	/// </summary>
	[Serializable]
	public class Changeset : BaseEntity, IChangeset
	{
		/// <summary>
		/// Gets the ID.
		/// </summary>
		public string ID { get; private set; }

		/// <summary>
		/// Gets the comment.
		/// </summary>
		public string Comment { get; private set; }

		/// <summary>
		/// Gets the committer.
		/// </summary>
		public string Committer { get; private set; }

		/// <summary>
		/// Gets the changes.
		/// </summary>
		public IEnumerable<IChange> Changes { get; private set; }

		/// <summary>
		/// Gets the workitems.
		/// </summary>
		public IEnumerable<IWorkItem> Workitems { get; private set; }

		private bool _IsSelected;
		/// <summary>
		/// Gets or sets a value indicating whether this instance is selected.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
		/// </value>
		public bool IsSelected
		{
			get { return _IsSelected; }
			set { SetValue(ref _IsSelected, value, () => IsSelected); }
		}

		private bool _IsCompleted;
		/// <summary>
		/// Gets a value indicating whether this instance is completed.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is completed; otherwise, <c>false</c>.
		/// </value>
		public bool IsCompleted
		{
			get { return _IsCompleted; }
			private set { SetValue(ref _IsCompleted, value, () => IsCompleted); }
		}

		private bool _IsDeleted;
		/// <summary>
		/// Gets a value indicating whether this instance is deleted.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is deleted; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeleted
		{
			get { return _IsDeleted; }
			private set { SetValue(ref _IsDeleted, value, () => IsDeleted); }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Changeset"/> class.
		/// </summary>
		/// <param name="changeset">The changeset.</param>
		public Changeset(IChangeset changeset)
		{
			ID = changeset.ID;
			Comment = changeset.Comment;
			Changes = changeset.Changes;
			Committer = changeset.Committer;
			Workitems = changeset.Workitems;

			_IsSelected = false;
			_IsCompleted = false;
			_IsDeleted = false;
		}

		/// <summary>
		/// Deletes this instance.
		/// </summary>
		public void Delete()
		{
			IsDeleted = true;
			IsSelected = false;
		}

		/// <summary>
		/// Completes this instance.
		/// </summary>
		public void Complete()
		{
			IsCompleted = true;
		}

		/// <summary>
		/// Resets this instance.
		/// </summary>
		public void Reset()
		{
			IsDeleted = false;
			IsCompleted = false;
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Changeset other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return Equals(other.ID, ID);
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:Fusion.View.Entities.Changeset"/> is equal to the current <see cref="T:Fusion.View.Entities.Changeset"/>.
		/// </summary>
		/// <returns>
		/// true if the specified <see cref="T:Fusion.View.Entities.Changeset"/> is equal to the current <see cref="T:Fusion.View.Entities.Changeset"/>; otherwise, false.
		/// </returns>
		/// <param name="obj">The <see cref="T:Fusion.View.Entities.Changeset"/> to compare with the current <see cref="T:Fusion.View.Entities.Changeset"/>. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != typeof(Changeset))
				return false;
			return Equals((Changeset)obj);
		}

		/// <summary>
		/// Serves as a hash function for a particular type. 
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:Fusion.View.Entities.Changeset"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}

		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator ==(Changeset left, Changeset right)
		{
			return Equals(left, right);
		}

		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator !=(Changeset left, Changeset right)
		{
			return !Equals(left, right);
		}

		/// <summary>
		/// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
		/// </summary>
		/// <param name="obj">An object to compare with this instance.</param>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than <paramref name="obj"/>. Zero This instance is equal to <paramref name="obj"/>. Greater than zero This instance is greater than <paramref name="obj"/>.
		/// </returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj"/> is not the same type as this instance. </exception>
		public int CompareTo(object obj)
		{
			var changeset = obj as Changeset;
			return changeset == null ? -1 : string.CompareOrdinal(ID, changeset.ID);
		}
	}
}
