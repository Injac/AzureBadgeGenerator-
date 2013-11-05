using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// That's the badge itself
    /// With all the necessary 
    /// Text properties, and
    /// the SVG content.
    /// </summary>
    public class Badge
    {
        /// <summary>
        /// Gets or sets the badge text.
        /// </summary>
        /// <value>The badge text.</value>
        public List<BadgeTextContent> BadgeText { get; set; }

        public string BadgeName { get; set; }

        /// <summary>
        /// Gets or sets the badge created.
        /// </summary>
        /// <value>The badge created.</value>
        public DateTime BadgeCreated { get; set; }

        /// <summary>
        /// Gets or sets the content of the raw SVG.
        /// </summary>
        /// <value>The content of the raw SVG.</value>
        public string RawSvgContent { get; set; }
    }
}