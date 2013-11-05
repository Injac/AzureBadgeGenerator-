using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// Badge Text Content.
    /// Holds the data that will be inserted into your SVGs
    /// </summary>
    public class BadgeTextContent
    {
        /// <summary>
        /// Gets or sets the name of the element.
        /// </summary>
        /// <value>The name of the element.</value>
        public string ElementName { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }
    }
}
