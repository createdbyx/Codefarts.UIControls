// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlsCollection.cs" company="">
//   
// </copyright>
// <summary>
//   The controls collection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Codefarts.UIControls
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// The controls collection.
    /// </summary>
    public class ControlsCollection : ObservableCollection<Control>
    {
        private Control owner;

        protected override void InsertItem(int index, Control item)
        {
            base.InsertItem(index, item);
            if (item.Parent != null)
            {
                item.Parent.Controls.Remove(item);
            }

            item.Parent = this.Owner;
        }

        protected override void RemoveItem(int index)
        {
            var item = this[index];
            base.RemoveItem(index);
            if (item.Parent == this.owner)
            {
                item.Parent = null;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlsCollection"/> class.
        /// </summary>
        /// <param name="owner">The owner control of the collection.</param>
        public ControlsCollection(Control owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// Gets the control owner of this collection.
        /// </summary>    
        public Control Owner
        {
            get
            {
                return this.owner;
            }   
        }
    }
}
