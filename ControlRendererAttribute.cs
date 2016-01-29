namespace Codefarts.UIControls
{
    using System;

    /// <summary>
    /// Provides a attribite for informing the renderer manager what renderer to use.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ControlRendererAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Attribute"/> class.
        /// </summary>
        public ControlRendererAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Attribute" /> class.
        /// </summary>
        /// <param name="type">The type that should be used to render the control.</param>
        public ControlRendererAttribute(Type type)
        {
            this.FullName = type.FullName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Attribute" /> class.
        /// </summary>
        /// <param name="fullName">The <see cref="Type.FullName"/> of the type that should be used to render the control.</param>
        public ControlRendererAttribute(string fullName)
        {
            this.FullName = fullName;
        }

        /// <summary>
        /// Gets or sets the full name of the type.
        /// </summary>
        public string FullName { get; set; }
    }
}