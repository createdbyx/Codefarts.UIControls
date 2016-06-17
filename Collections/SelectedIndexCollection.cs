/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.UIControls
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Provides a collection of <see cref="int"/> types.
    /// </summary>
    /// <remarks>This collection is used by the <see cref="ListBox"/> control.</remarks>
    public class SelectedIndexCollection : ObservableCollection<int>
    {
        /// <summary>
        /// The owner of this collection.
        /// </summary>
        private ListBox owner;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedIndexCollection"/> class.
        /// </summary>
        /// <param name="listBox">The list box that is the owner of this collection.</param>
        public SelectedIndexCollection(ListBox listBox)
        {
            this.owner = listBox;
        }

        #region Overrides of ObservableCollection<int>

        /// <summary>Inserts an item into the collection at the specified index.</summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert.</param>
        protected override void InsertItem(int index, int item)
        {
            if (this.owner != null && this.owner.Items != null && index != -1 && !this.Contains(index))
            {
                this.owner.SetSelected(index, true);
            }

            base.InsertItem(index, item);
        }

        #endregion
    }
}