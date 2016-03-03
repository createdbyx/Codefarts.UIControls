namespace Codefarts.UIControls
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using Codefarts.UIControls.Interfaces;
    using Codefarts.UIControls.Models;

    /// <summary>
    /// Provides a collection to store <see cref="ColumnDefinition"/> types.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{ColumnDefinition}" />
    /// <seealso cref="IMarkup" />
    public class ColumnDefinitionCollection : ObservableCollection<ColumnDefinition>, IMarkup
    {
        /// <summary>
        /// Builds a <see cref="Markup" /> object that represent the state of the implementor.
        /// </summary>
        /// <returns>
        /// A <see cref="Markup" /> object containing the relavent information.
        /// </returns>
        /// <remarks>
        ///   <p>The returned <see cref="Markup" /> object contains the relavnet data stored by the implementor.</p>
        /// </remarks>
        public virtual Markup ToMarkup()
        {
            var markup = new Markup();
            markup.Name = this.GetType().FullName;
            markup.Children = this.Select(x => x.ToMarkup()).ToList();
            return markup;
        }
    }
}