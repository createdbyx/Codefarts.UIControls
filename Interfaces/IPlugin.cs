namespace Codefarts.UIControls
{
    using System;

    /// <summary>
    /// Provides a basic plugin interface.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Gets the unique identifier for this plugin.
        /// </summary>
        Guid UniqueId { get; }
    }
}
