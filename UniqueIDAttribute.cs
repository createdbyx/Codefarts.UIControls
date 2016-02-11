namespace Codefarts.UIControls
{
    using System;

    /// <summary>
    /// Provides a attribite for assigning a unique id to a type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class UniqueIDAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Attribute" /> class.
        /// </summary>
        /// <param name="fullName">The unique id associated with the type.</param>
        public UniqueIDAttribute(Guid fullName)
        {
            this.UniqueId = fullName;
        }

        /// <summary>
        /// Gets or sets the unique id of the type.
        /// </summary>
        public Guid UniqueId { get; set; }
    }
}