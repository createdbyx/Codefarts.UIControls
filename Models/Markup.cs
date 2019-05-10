namespace Codefarts.UIControls.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides a model for markup conversion.
    /// </summary>
    public class Markup
    {
        /// <summary>
        /// Gets or sets the name associated with the markup element.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the markup children.
        /// </summary>
        public IList<Markup> Children { get; set; }

        /// <summary>
        /// Gets or sets the markup element properties.
        /// </summary>
        public IDictionary<string, object> Properties { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> with the specified property name.
        /// </summary>
        /// <param name="key">The name of the property.</param>
        /// <returns>Return the value of the property.</returns>
        /// <remarks>Can throw <see cref="System.NullReferenceException"/> if the <seealso cref="Properties"/> property is null.</remarks>
        public object this[string key]
        {
            get
            {
                return this.Properties[key];
            }

            set
            {
                this.Properties[key] = value;
            }
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        public Markup Parent { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Markup"/> class.
        /// </summary>
        public Markup()
        {
            this.Children = new List<Markup>();
            this.Properties = new Dictionary<string, object>();
        }
    }
}