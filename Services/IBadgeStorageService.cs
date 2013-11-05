using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Use this interface
    /// to implement storage mechanisms
    /// like saving to file, blobs, tables
    /// or even other services.
    /// </summary>
    public interface IBadgeStorageService
    {
        /// <summary>
        /// Saves the badge.
        /// </summary>
        /// <param name="badge">The badge.</param>
        /// <returns></returns>
        Task SaveBadge(Badge badge);

        /// <summary>
        /// Loads the badge.
        /// </summary>
        /// <param name="badgeName">Name of the badge.</param>
        /// <returns></returns>
        Task<string> LoadBadge(string badgeName);

        /// <summary>
        /// Lists all badges.
        /// </summary>
        /// <returns></returns>
        Task<List<string>> ListAllBadges();

        /// <summary>
        /// Deletes the badge.
        /// </summary>
        /// <param name="badgeName">Name of the badge.</param>
        /// <returns></returns>
        Task<bool> DeleteBadge(string badgeName);

        /// <summary>
        /// Updates the badge.
        /// </summary>
        /// <param name="badge">The badge.</param>
        /// <returns></returns>
        Task<bool> UpdateBadge(Badge badge);
    }
}