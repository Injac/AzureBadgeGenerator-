using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// Data needed to generate a badge.
    /// </summary>
    public class BadgeGenData
    {
        /// <summary>
        /// Gets or sets the name of the badge.
        /// </summary>
        /// <value>The name of the badge.</value>
        public string BadgeName { get; set; }

        /// <summary>
        /// Gets or sets the badge data.
        /// </summary>
        /// <value>The badge data.</value>
        public List<BadgeTextContent> BadgeData { get; set; }
       
        public BadgeGenData()
        {
            this.BadgeData = new List<BadgeTextContent>();
        }
    }
}
