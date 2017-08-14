namespace Codefarts.UIControls.Interfaces
{
    using Codefarts.UIControls.Models;

    /// <summary>
    /// Provides a markup interface for classes that wish to offer <see cref="ToMarkup"/> support.
    /// </summary>
    public interface IMarkup
    {
        /// <summary>
        /// Builds a <see cref="Markup"/> object that represent the state of the implementor.
        /// </summary>
        /// <returns>A <see cref="Markup"/> object containing the relevant information.</returns>
        /// <remarks>
        /// <p>The returned <see cref="Markup"/> object contains the relevant data stored by the implementor.</p>
        /// </remarks>
        Markup ToMarkup();
    }
}