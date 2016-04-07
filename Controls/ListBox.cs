/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

// ReSharper disable InconsistentNaming

namespace Codefarts.UIControls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    /// <summary>
    /// Represents a control to display a list of items. 
    /// </summary>
    public class ListBox : ScrollViewer
    {
        /// <summary>
        /// The backing field for the <see cref="DisplayMemberCallback"/> property.
        /// </summary>
        protected Func<object, string> displayMemberCallback;

        /// <summary>
        /// The backing field for the <see cref="DrawMode"/> property.
        /// </summary>
        protected DrawMode drawMode = DrawMode.Normal;

        /// <summary>
        /// The backing field for the <see cref="Items"/> property.
        /// </summary>
        protected ItemsCollection items;

        /// <summary>
        /// The backing field for the <see cref="ScrollAlwaysVisible"/> property.
        /// </summary>
        protected bool scrollAlwaysVisible;

        /// <summary>
        /// The backing field for the <see cref="SelectedIndex"/> property.
        /// </summary>
        protected int selectedIndex = -1;

        /// <summary>
        /// The backing field for the <see cref="SelectedItems"/> property.
        /// </summary>
        protected List<object> selectedItems;

        /// <summary>
        /// The backing field for the <see cref="SelectedIndicies"/> property.
        /// </summary>
        protected SelectedIndexCollection selectedIndicies;

        /// <summary>
        /// The backing field for the <see cref="SelectionMode"/> property.
        /// </summary>
        protected SelectionMode selectionMode;


        /// <summary>
        /// Initializes a new instance of the <see cref="ListBox"/> class.
        /// </summary>
        public ListBox()
        {
            this.canFocus = true;
            this.isTabStop = true;
            this.items = new ItemsCollection();
            this.items.CollectionChanged += this.ItemsCollectionChanged;
            this.selectedItems = new List<object>();
            this.selectedIndicies = new SelectedIndexCollection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListBox"/> class.
        /// </summary>
        /// <param name="name">The name of the control.</param>
        public ListBox(string name)
            : this()
        {
            this.name = name;
        }

        /// <summary>
        /// Gets the items of the <see cref="T:ListBox" />.
        /// </summary>
        /// <returns>
        /// An <see cref="T:ItemsCollection" /> representing the items in the <see cref="T:ListBox" />.
        /// </returns>
        public virtual ItemsCollection Items
        {
            get
            {
                return this.items;
            }

            internal set
            {
                var changed = this.items != value;
                this.items = value;
                if (changed)
                {
                    this.OnPropertyChanged("Items");
                }
            }
        }

        /// <summary>
        /// Gets a collection containing the currently selected items in the <see cref="T:ListBox" />.
        /// </summary>
        /// <returns>
        /// A <see cref="T:IList" /> containing the currently selected items in the control.
        /// </returns>
        public virtual List<object> SelectedItems
        {
            get
            {
                return this.selectedItems;
            }
        }

        /// <summary>
        /// Gets a collection containing the currently selected item indexes in the <see cref="T:ListBox" />.
        /// </summary>
        /// <returns>
        /// A <see cref="T:SelectedIndexCollection" /> containing the currently selected item indexes in the control.
        /// </returns>
        public virtual SelectedIndexCollection SelectedIndicies
        {
            get
            {
                return this.selectedIndicies;
            }
        }

        /// <summary>
        /// Gets or sets the display member callback used to get a string that represents an item in the list.
        /// </summary>
        public Func<object, string> DisplayMemberCallback
        {
            get
            {
                return this.displayMemberCallback;
            }

            set
            {
                var changed = this.displayMemberCallback != value;
                this.displayMemberCallback = value;
                if (changed)
                {
                    this.OnPropertyChanged("DisplayMemberCallback");
                }
            }
        }

        /// <summary>
        /// Gets or sets the drawing mode for the control.
        /// </summary>
        /// <returns>
        /// One of the <see cref="T:DrawMode" /> values representing the mode for drawing the items of the control. The default is DrawMode.Normal.
        /// </returns>
        public virtual DrawMode DrawMode
        {
            get
            {
                return this.drawMode;
            }

            set
            {
                var changed = this.drawMode != value;
                this.drawMode = value;
                if (changed)
                {
                    this.OnPropertyChanged("DrawMode");
                }
            }
        }

        /// <summary>
        /// Gets or sets the first item in the current selection or returns null if the selection is empty.
        /// </summary>
        /// <returns>
        /// The first item in the current selection or null if the selection is empty.
        /// </returns>
        public object SelectedItem
        {
            get
            {
                var noSelection = this.Items.Count == 0 || this.selectedIndex < 0;
                return noSelection ? null : this.Items[this.selectedIndex];
            }
        }

        /// <summary>
        /// Gets or sets the method in which items are selected in the <see cref="ListBox" />.
        /// </summary>
        /// <returns>
        /// One of the <see cref="SelectionMode" /> values. The default is SelectionMode.One.
        /// </returns>
        public SelectionMode SelectionMode
        {
            get
            {
                return this.selectionMode;
            }

            set
            {
                var changed = this.selectionMode != value;
                this.selectionMode = value;
                if (changed)
                {
                    if (value == SelectionMode.Single)
                    {
                        this.selectedIndicies.Clear();
                        if (this.selectedIndex != -1)
                        {
                            this.selectedIndicies.Add(this.selectedIndex);
                        }
                    }

                    this.OnPropertyChanged("SelectionMode");
                }
            }
        }

        #region Overrides of ScrollViewer

        /// <summary>
        /// Gets or sets the vertical scroll bar visibility.
        /// </summary>    
        public override ScrollBarVisibility VerticalScrollBarVisibility
        {
            get
            {
                return base.VerticalScrollBarVisibility;
            }

            set
            {
                base.VerticalScrollBarVisibility = this.scrollAlwaysVisible ? ScrollBarVisibility.Visible : value;
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets a value indicating whether the vertical scroll bar is shown at all times.
        /// </summary>
        /// <returns>
        /// true if the vertical scroll bar should always be displayed; otherwise, false. The default is false.
        /// </returns>
        public bool ScrollAlwaysVisible
        {
            get
            {
                return this.scrollAlwaysVisible;
            }
            set
            {
                var changed = this.scrollAlwaysVisible != value;
                this.scrollAlwaysVisible = value;
                if (changed)
                {
                    this.OnPropertyChanged("ScrollAlwaysVisible");
                }

                if (value)
                {
                    this.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                }
            }
        }

        /// <summary>
        /// Gets or sets the zero-based index of the currently selected item in a <see cref="T:ListBox" />.
        /// </summary>
        /// <returns>
        /// A zero-based index of the currently selected item. A value of negative one (-1) is returned if no item is selected.
        /// </returns>
        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }

            set
            {
                // quick check if we need to go further. This check if here because the call to GetItemCount is expensive.
                if (this.selectedIndex == value)
                {
                    return;
                }

                var count = this.GetItemCount();
                value = value < -1 ? -1 : value;
                value = value > count - 1 ? count - 1 : value;
                var changed = this.selectedIndex != value;
                this.selectedIndex = value;
                if (changed)
                {
                    this.OnSelectionChanged();
                }
            }
        }

        /// <summary>
        /// Gets the item count of the items in the <see cref="Items"/> property or the <see cref="Control.DataContext"/>.
        /// </summary>
        /// <returns>The number of items in the list.</returns>
        /// <remarks><p>If set the count will be returned from <see cref="Control.DataContext"/> property if it has been assigned
        /// some type of ICollection, IList, Array, or IEnumerable. (Checked in that order).</p>
        /// <p><b>NOTE:</b> If the <see cref="Control.DataContext"/> property is resolved to a IEnumerable and that enumerable is too large
        /// or unending the cost of calling this method may be too high and your app may seem to lock up or suffer a performance hit.</p></remarks>
        private int GetItemCount()
        {
            var count = this.Items.Count;
            var context = this.dataContext;
            if (context == null)
            {
                return count;
            }

            var collection = context as ICollection;
            if (collection != null)
            {
                return collection.Count;
            }

            var list = context as IList;
            if (list != null)
            {
                return list.Count;
            }

            var array = context as Array;
            if (array != null)
            {
                return array.Length;
            }

            var enumerable = context as IEnumerable;
            if (enumerable != null)
            {
                count = 0;
                foreach (var item in enumerable)
                {
                    count++;
                }

                return count;
            }

            return count;
        }

        /// <returns>
        /// The default <see cref="Size" /> of the control.
        /// </returns>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(120, 96);
            }
        }

        /// <summary>
        /// Occurs when a visual aspect of an owner-drawn <see cref="T:ListBox" /> changes.
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// Occurs when a visual aspect of an owner-drawn <see cref="T:ListBox" /> changes.
        /// </summary>
        public event EventHandler<ListBoxItemInformationArgs> DrawItem;

        /// <summary>
        /// Occurs when an owner-drawn <see cref="T:ListBox" /> is created and the sizes of the list items are determined.
        /// </summary>
        public event EventHandler<ListBoxItemInformationArgs> MeasureItem;

        /// <summary>
        /// Raises the <see cref="E:SelectionChanged"/> event.
        /// </summary>
        public virtual void OnSelectionChanged()
        {
            var handler = this.SelectionChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary> 
        /// Selects all the items in a <see cref="T:ListBox" />. 
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="P:ListBox.SelectionMode" /> property is set to <see cref="UIControls.SelectionMode.Single" />.
        /// </exception>
        public void SelectAll()
        {
            if (this.SelectionMode != SelectionMode.Single)
            {
                throw new NotSupportedException("Can ont select all when SelectionMode is single.");
            }

            this.selectedItems.Clear();
            this.selectedItems.AddRange(this.Items);
        }

        /// <summary>
        /// Clears all the selection in a <see cref="T:ListBox" />.
        /// </summary>
        public void UnselectAll()
        {
            this.selectedItems.Clear();
        }

        /// <summary>
        /// Handles changes to the items collection.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    break;

                case NotifyCollectionChangedAction.Remove:
                    break;

                case NotifyCollectionChangedAction.Replace:
                    break;

                //case NotifyCollectionChangedAction.Move:
                //    break;

                case NotifyCollectionChangedAction.Reset:
                    this.selectedItems.Clear();
                    this.selectedIndex = -1;
                    break;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:MeasureItem"/> event.
        /// </summary>
        public virtual void OnMeasureItem(ListBoxItemInformationArgs e)
        {
            var handler = this.MeasureItem;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:DrawItem"/> event.
        /// </summary>
        public virtual void OnDrawItem(ListBoxItemInformationArgs e)
        {
            var handler = this.DrawItem;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}