using Fusion.Domain;
using Changeset = Microsoft.TeamFoundation.VersionControl.Client.Changeset;

namespace Fusion.Infrastructure.Factories
{
    public interface IChangesetFactory
    {
		/// <summary>
		/// Creates the specified changeset.
		/// </summary>
		/// <param name="changeset">The changeset.</param>
		/// <returns></returns>
        IChangeset Create(Changeset changeset);
    }
}