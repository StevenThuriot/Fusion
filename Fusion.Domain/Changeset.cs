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
using System.Collections.Generic;
using System.Globalization;
using Moon.Extensions;
using Moon.Helpers;

namespace Fusion.Domain
{
	public class Changeset : IChangeset, IEquatable<Changeset>
    {
		private readonly int _ID;
        public string ID { get; private set; }
        public string Comment { get; private set; }
		public string Committer { get; private set; }
		public IEnumerable<IChange> Changes { get; private set; }
		public IEnumerable<IWorkItem> Workitems { get; private set; }

		public Changeset(int id, string comment, string committer, IEnumerable<IChange> changes, IEnumerable<IWorkItem> workitems)
        {
    		Guard.NotNull(committer, changes, workitems);

    		_ID = id;
            ID = "# {0}".FormatWith(id.ToString(CultureInfo.InvariantCulture));

            Comment = comment;
			Committer = committer;
			Changes = changes;
			Workitems = workitems;
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
        /// Determines whether the specified <see cref="T:Fusion.Domain.Changeset"/> is equal to the current <see cref="T:Fusion.Domain.Changeset"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:Fusion.Domain.Changeset"/> is equal to the current <see cref="T:Fusion.Domain.Changeset"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:Fusion.Domain.Changeset"/> to compare with the current <see cref="T:Fusion.Domain.Changeset"/>. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof (Changeset))
                return false;
            return Equals((Changeset)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:Fusion.Domain.Changeset"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public static bool operator ==(Changeset left, Changeset right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Changeset left, Changeset right)
        {
            return !Equals(left, right);
        }

        public int CompareTo(object obj)
        {
            var changeset = obj as Changeset;
            return changeset == null ? -1 : string.CompareOrdinal(ID, changeset.ID);
        }

        public override string ToString()
        {
            return _ID.ToString(CultureInfo.InvariantCulture);
        }
    }
}
