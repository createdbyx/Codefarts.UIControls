namespace Codefarts.UIControls.Factories
{
    /// <summary>
    /// Provides the base factory implementation.
    /// </summary>
    public abstract class UIControlsFactory
    {
        /// <summary>
        /// Gets or sets a singleton instance of a <see cref="IUIControlsManager"/>.
        /// </summary>
        public static IUIControlsManager DefaultManager { get; set; }

        /// <summary>
        /// Gets a <see cref="IUIControlsManager"/> manager instance.
        /// </summary>
        /// <param name="UniqueId">The unique identifier for the manager.</param>
        /// <returns>A reference to a <see cref="IUIControlsManager"/> implementation, otherwise null if unable to get a manager.</returns>
        public static IUIControlsManager GetManager(string UniqueId)
        {
            return null;
        }
    }
}
