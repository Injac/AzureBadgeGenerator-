using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// The generator service 
    /// for the badge.
    /// </summary>
    public interface IBadgeGenerator
    {
                       
        /// <summary>
        /// Generates the badge image.
        /// </summary>
        /// <param name="badge">The badge.</param>
        /// <returns></returns>
        Task<byte[]> GenerateBadgeImage(string badgeName, List<BadgeTextContent> content);
    }
}
