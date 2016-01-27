namespace Codefarts.UIControls
{
    using System.Collections.Generic;

    public class Markup
    {
        public string Name { get; set; }
        public IList<Markup> Children { get; set; }
        public IDictionary<string, object> Properties { get; set; }

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