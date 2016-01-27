namespace Codefarts.UIControls
{
    using System.Collections.ObjectModel;
    using System.Linq;

    public class RowDefinitionCollection : ObservableCollection<RowDefinition>
    {
        public virtual Markup ToMarkup()
        {
            var markup = new Markup();
            markup.Name = this.GetType().FullName;
            markup.Children = this.Select(x => x.ToMarkup()).ToList();
            return markup;
        }
    }
}