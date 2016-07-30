namespace Codefarts.UIControls
{
    using System;

    /// <summary>
    /// Specifies the name of the category in which to group the member when being categorized.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class CategoryAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the category for the member that this attribute is applied to.
        /// </summary>
        /// <returns>
        /// The name of the category for the member that this attribute is applied to.
        /// </returns>
        public string Category { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryAttribute"/> class.
        /// </summary>
        public CategoryAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Attribute"/> class.
        /// </summary>
        public CategoryAttribute(string category) : this()
        {
            this.Category = category;
        }
    }
}
