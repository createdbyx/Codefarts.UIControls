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
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.Serialization;

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
        protected ObjectCollection items;

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
        protected SelectedObjectCollection selectedItems;

        /// <summary>
        /// The backing field for the <see cref="SelectedIndicies"/> property.
        /// </summary>
        protected SelectedIndexCollection selectedIndicies;

        /// <summary>
        /// The backing field for the <see cref="SelectionMode"/> property.
        /// </summary>
        protected SelectionMode selectionMode = SelectionMode.Single;

        private bool sorted;

        private bool selectionModeChanging;

        private SelectionMode cachedSelectionMode;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListBox"/> class.
        /// </summary>
        public ListBox()
        {
            this.canFocus = true;
            this.isTabStop = true;
            this.items = new ObjectCollection(this);

            // this.items.CollectionChanged += this.ItemsCollectionChanged;
            this.selectedItems = new SelectedObjectCollection(this);
            this.selectedIndicies = new SelectedIndexCollection(this);

            this.PropertyChanged += this.ListBox_PropertyChanged;
        }

        private void ListBox_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "DataContext")
            {
                return;
            }

            if (this.dataContext == null)
            {
                // this.BeginUpdate();
                this.SelectedIndex = -1;
                this.Items.Clear();

                // this.EndUpdate();
            }

            this.RefreshItems();
        }

        /// <summary>
        /// Refreshes all <see cref="T:ListBox" /> items and retrieves new strings for them.
        /// </summary>
        protected virtual void RefreshItems()
        {
            var objectCollections = this.items;
            this.items = null;
            this.selectedIndicies = null;

            // if (base.IsHandleCreated)
            // {
            // this.NativeClear();
            // }
            object[] item = null;
            var dataContextItems = this.dataContext as IEnumerable;

            if (dataContextItems != null)
            {
                // && base.DataManager.Count != -1)
                var count = 1000;
                item = new object[count];
                var index = 0;
                foreach (var contextItem in dataContextItems)
                {
                    item[index++] = contextItem;
                    if (index == count)
                    {
                        count += 1000;
                        Array.Resize(ref item, count);
                    }
                }

                Array.Resize(ref item, index);

                // for (var i = 0; i < (int)item.Length; i++)
                // {
                // item[i] = base.DataManager[i];
                // }
            }
            else if (objectCollections != null)
            {
                item = new object[objectCollections.Count];
                objectCollections.CopyTo(item, 0);
            }

            if (item != null && item.Length > 0)
            {
                this.Items.AddRangeInternal(item);
            }

            if (this.SelectionMode != SelectionMode.None)
            {
                // if (base.DataManager != null)
                // {
                // this.SelectedIndex = base.DataManager.Position;
                // return;
                // }
                if (objectCollections != null)
                {
                    var count = objectCollections.Count;
                    for (var i = 0; i < count; i++)
                    {
                        if (objectCollections.InnerArray.GetState(i, ListBox.SelectedObjectCollection.SelectedObjectMask))
                        {
                            this.SelectedItem = objectCollections[i];
                        }
                    }
                }
            }
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
        /// Gets or sets a value indicating whether the items in the <see cref="T:ListBox" /> are sorted alphabetically.
        /// </summary>
        /// <returns>
        /// true if items in the control are sorted; otherwise, false. The default is false.
        /// </returns>
        [DefaultValue(false)]
        public bool Sorted
        {
            get
            {
                return this.sorted;
            }

            set
            {
                if (this.sorted == value)
                {
                    return;
                }

                this.sorted = value;
                if (this.sorted && this.items != null && this.items.Count >= 1)
                {
                    this.Sort();
                }
            }
        }

        /// <summary>Creates a new instance of the item collection.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> that represents the new item collection.</returns>
        protected virtual ObjectCollection CreateItemCollection()
        {
            return new ListBox.ObjectCollection(this);
        }

        /// <summary>
        /// Gets the items of the <see cref="T:ListBox" />.
        /// </summary>
        /// <returns>
        /// An <see cref="T:ItemsCollection" /> representing the items in the <see cref="T:ListBox" />.
        /// </returns>
        public virtual ObjectCollection Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = this.CreateItemCollection();
                }

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
        public virtual SelectedObjectCollection SelectedItems
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
                if (this.selectedIndicies == null)
                {
                    this.selectedIndicies = new ListBox.SelectedIndexCollection(this);
                }

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
                return this.SelectedItems.Count <= 0 ? null : this.SelectedItems[0];
            }

            set
            {
                if (this.items == null)
                {
                    return;
                }

                if (value == null)
                {
                    this.SelectedIndex = -1;
                }
                else
                {
                    var num = this.items.IndexOf(value);
                    if (num != -1)
                    {
                        this.SelectedIndex = num;
                    }
                }
            }

            // get
            // {
            // var noSelection = this.Items.Count == 0 || this.selectedIndex < 0;
            // return noSelection ? null : this.Items[this.selectedIndex];
            // }

            // set
            // {
            // if (this.items != null)
            // {
            // if (value == null)
            // {
            // this.SelectedIndex = -1;
            // }
            // else
            // {
            // var num = this.items.IndexOf(value);
            // if (num != -1)
            // {
            // this.SelectedIndex = num;
            // }
            // }
            // }
            // }
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
                var selectionMode = this.selectionModeChanging ? this.cachedSelectionMode : this.selectionMode;
                if (selectionMode == SelectionMode.None)
                {
                    return -1;
                }

                if (selectionMode == SelectionMode.Single)
                {
                    // && base.IsHandleCreated)
                    return this.selectedIndex;
                    // return (int)this.SendMessage(392, 0, 0);
                }

                if (this.items == null || this.SelectedItems.Count <= 0)
                {
                    return -1;
                }

                return this.Items.IndexOfIdentifier(this.SelectedItems.GetObjectAt(0));

                // return this.selectedIndex;
            }

            set
            {
                //// quick check if we need to go further. This check if here because the call to GetItemCount is expensive.
                // if (this.selectedIndex == value)
                // {
                // return;
                // }

                // var count = this.GetItemCount();
                // value = value < -1 ? -1 : value;
                // value = value > count - 1 ? count - 1 : value;
                // var changed = this.selectedIndex != value;
                // this.selectedIndex = value;
                // if (changed)
                // {
                // this.OnSelectionChanged();
                // }

                if (value < -1 || value >= (this.items == null ? 0 : this.items.Count))
                {
                    throw new ArgumentOutOfRangeException("SelectedIndex");
                }

                if (this.selectionMode == SelectionMode.None)
                {
                    throw new ArgumentException("Cannot call this method when SelectionMode is SelectionMode.None.", "SelectedIndex");
                }

                this.UnselectAll();
                if (value > -1 && value != this.selectedIndex)
                {
                    this.selectedIndex = value;
                    this.SelectedItems.SetSelected(value, true);
                    this.OnSelectionChanged();
                }

                //if (this.selectionMode == SelectionMode.Single && value != -1)
                //{
                //    var selectedIndex = this.SelectedIndex;
                //    if (selectedIndex != value)
                //    {
                //        if (selectedIndex != -1)
                //        {
                //            this.SelectedItems.SetSelected(selectedIndex, false);
                //        }

                //        this.SelectedItems.SetSelected(value, true);

                //        // if (base.IsHandleCreated)
                //        // {
                //        // this.NativeSetSelected(value, true);
                //        // }
                //        this.selectedIndex = value;
                //        this.OnSelectionChanged();
                //        // return;
                //    }
                //}
                //else if (value == -1)
                //{
                //    if (this.SelectedIndex != -1)
                //    {
                //        this.UnselectAll();
                //        // return;
                //    }
                //}
                //else if (!this.SelectedItems.GetSelected(value))
                //{
                //    this.selectedIndex = this.selectedIndex == -1 ? value : this.selectedIndex;
                //    this.SelectedItems.SetSelected(value, true);

                //    // if (base.IsHandleCreated)
                //    // {
                //    // this.NativeSetSelected(value, true);
                //    // }
                //    this.OnSelectionChanged();
            }
        }

        /// <summary>Returns the text representation of the specified item.</summary>
        /// <returns>If the <see cref="P:ListBox.DisplayMember" /> property is not specified, the value returned by
        /// <see cref="M:ListBox.GetItemText(System.Object)" /> is the value of the item's ToString method. Otherwise,
        /// the method returns the string value of the member returned by <see cref="P:ListBox.DisplayMember" /> property
        /// for the object specified in the <paramref name="item" /> parameter.</returns>
        /// <param name="item">The object from which to get the contents to display. </param>
        public string GetItemText(object item)
        {
            return this.displayMemberCallback == null ? this.items.ToString() : this.displayMemberCallback(item);
        }

        /// <summary>Sorts the items in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
        protected virtual void Sort()
        {
            this.CheckNoDataSource();
            this.selectedItems.EnsureUpToDate();
            if (this.sorted && this.items != null)
            {
                this.items.InnerArray.Sort();
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

        private void CheckNoDataSource()
        {
            if (base.DataContext != null)
            {
                throw new ArgumentException("Items collection cannot be modified when the DataSource property is set.");
            }
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
                throw new NotSupportedException("Can only select all when SelectionMode is not SelectionMode.Single.");
            }

            // this.selectedItems.Clear();
            // this.selectedItems.AddRange(this.Items);
            for (var i = 0; i < this.items.Count; i++)
            {
                this.SetSelected(i, true);
            }
        }

        /// <summary>
        /// Selects or clears the selection for the specified item in a <see cref="T:ListBox" />.
        /// </summary>
        /// <param name="index">The zero-based index of the item in a <see cref="T:ListBox" /> to select or clear the selection for. </param>
        /// <param name="value">true to select the specified item; otherwise, false. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified index was outside the range of valid values. </exception>
        /// <exception cref="T:System.InvalidOperationException">The <see cref="P:ListBox.SelectionMode" /> property was set to None.</exception>
        public void SetSelected(int index, bool value)
        {
            if (index < 0 || index >= (this.items == null ? 0 : this.items.Count))
            {
                throw new ArgumentOutOfRangeException("index");
            }

            switch (this.selectionMode)
            {
                case SelectionMode.None:
                    throw new InvalidOperationException("Cannot call this method when SelectionMode is SelectionMode.None.");

                case SelectionMode.Single:
                    this.SelectedItems.Clear();
                    this.SelectedItems.SetSelected(index, value);
                    break;

                case SelectionMode.Multiple:
                    break;

                case SelectionMode.Extended:
                    this.SelectedItems.SetSelected(index, value);
                    break;
            }

            this.selectedIndex = this.selectedIndex == -1 && value ? index : this.selectedIndex;
            this.SelectedItems.Dirty();
            this.OnSelectionChanged();
        }

        /// <summary>
        /// Clears all the selection in a <see cref="T:ListBox" />.
        /// </summary>
        public void UnselectAll()
        {
            var flag = false;
            var num = this.items == null ? 0 : this.items.Count;
            for (var i = 0; i < num; i++)
            {
                if (this.SelectedItems.GetSelected(i))
                {
                    flag = true;
                    this.SelectedItems.SetSelected(i, false);
                }
            }

            this.selectedIndex = -1;

            if (flag)
            {
                this.OnSelectionChanged();
            }

            // this.selectedItems.Clear();
            // this.selectedIndicies.Clear();
        }

        ///// <summary>
        ///// Handles changes to the items collection.
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        // private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        // {
        // switch (e.Action)
        // {
        // case NotifyCollectionChangedAction.Add:
        // break;

        // case NotifyCollectionChangedAction.Remove:
        // break;

        // case NotifyCollectionChangedAction.Replace:
        // break;

        // //case NotifyCollectionChangedAction.Move:
        // //    break;

        // case NotifyCollectionChangedAction.Reset:
        // this.selectedItems.Clear();
        // this.selectedIndex = -1;
        // break;
        // }
        // }

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



        // ========================================



        /// <summary>Represents the collection of items in a <see cref="T:ListBox" />.</summary>
        public class ObjectCollection : IList, ICollection, IEnumerable
        {
            private ListBox owner;

            private ItemArray items;

            public int Count
            {
                get
                {
                    return this.InnerArray.GetCount(0);
                }
            }

            internal ItemArray InnerArray
            {
                get
                {
                    if (this.items == null)
                    {
                        this.items = new ListBox.ItemArray(this.owner);
                    }

                    return this.items;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    return false;
                }
            }

#if !PORTABLE && !WINDOWS_UWP
            [Browsable(false)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#endif
            public virtual object this[int index]
            {
                get
                {
                    if (index < 0 || index >= this.InnerArray.GetCount(0))
                    {
                        throw new ArgumentOutOfRangeException("index");
                    }

                    return this.InnerArray.GetItem(index, 0);
                }

                set
                {
                    this.owner.CheckNoDataSource();
                    this.SetItemInternal(index, value);
                }
            }

            bool System.Collections.ICollection.IsSynchronized
            {
                get
                {
                    return false;
                }
            }

            object System.Collections.ICollection.SyncRoot
            {
                get
                {
                    return this;
                }
            }

            bool System.Collections.IList.IsFixedSize
            {
                get
                {
                    return false;
                }
            }

            public ObjectCollection(ListBox owner)
            {
                this.owner = owner;
            }

            public ObjectCollection(ListBox owner, ObjectCollection value)
            {
                this.owner = owner;
                this.AddRange(value);
            }

            public ObjectCollection(ListBox owner, object[] value)
            {
                this.owner = owner;
                this.AddRange(value);
            }

            public int Add(object item)
            {
                this.owner.CheckNoDataSource();
                var num = this.AddInternal(item);
                return num;
            }

            private int AddInternal(object item)
            {
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }

                var count = -1;
                if (this.owner.sorted)
                {
                    if (this.Count <= 0)
                    {
                        count = 0;
                    }
                    else
                    {
                        count = this.InnerArray.BinarySearch(item);
                        if (count < 0)
                        {
                            count = ~count;
                        }
                    }

                    this.InnerArray.Insert(count, item);
                }
                else
                {
                    this.InnerArray.Add(item);
                }

                var wasInserted = false;
                try
                {
                    if (!this.owner.sorted)
                    {
                        count = this.Count - 1;
                    }
                    else
                    {
                        if (this.owner.selectedItems != null)
                        {
                            this.owner.selectedItems.Dirty();
                        }
                    }

                    wasInserted = true;
                }
                finally
                {
                    if (!wasInserted)
                    {
                        this.InnerArray.Remove(item);
                    }
                }

                return count;
            }

            public void AddRange(ObjectCollection value)
            {
                this.owner.CheckNoDataSource();
                this.AddRangeInternal(value);
            }

            public void AddRange(object[] items)
            {
                this.owner.CheckNoDataSource();
                this.AddRangeInternal(items);
            }

            internal void AddRangeInternal(ICollection items)
            {
                if (items == null)
                {
                    throw new ArgumentNullException("items");
                }

                foreach (var item in items)
                {
                    this.AddInternal(item);
                }
            }

            public virtual void Clear()
            {
                this.owner.CheckNoDataSource();
                this.InnerArray.Clear();
            }

            public bool Contains(object value)
            {
                return this.IndexOf(value) != -1;
            }

            public void CopyTo(object[] destination, int arrayIndex)
            {
                var count = this.InnerArray.GetCount(0);
                for (var i = 0; i < count; i++)
                {
                    destination[i + arrayIndex] = this.InnerArray.GetItem(i, 0);
                }
            }

            public IEnumerator GetEnumerator()
            {
                return this.InnerArray.GetEnumerator(0);
            }

            public int IndexOf(object value)
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                return this.InnerArray.IndexOf(value, 0);
            }

            internal int IndexOfIdentifier(object value)
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                return this.InnerArray.IndexOfIdentifier(value, 0);
            }

            public void Insert(int index, object item)
            {
                this.owner.CheckNoDataSource();
                if (index < 0 || index > this.InnerArray.GetCount(0))
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                if (!this.owner.sorted)
                {
                    this.InnerArray.Insert(index, item);
                }
                else
                {
                    this.Add(item);
                }
            }

            public void Remove(object value)
            {
                var num = this.InnerArray.IndexOf(value, 0);
                if (num != -1)
                {
                    this.RemoveAt(num);
                }
            }

            public void RemoveAt(int index)
            {
                this.owner.CheckNoDataSource();
                if (index < 0 || index >= this.InnerArray.GetCount(0))
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                this.InnerArray.RemoveAt(index);
            }

            internal void SetItemInternal(int index, object value)
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                if (index < 0 || index >= this.InnerArray.GetCount(0))
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                this.InnerArray.SetItem(index, value);
            }

            void System.Collections.ICollection.CopyTo(Array destination, int index)
            {
                var count = this.InnerArray.GetCount(0);
                for (var i = 0; i < count; i++)
                {
                    destination.SetValue(this.InnerArray.GetItem(i, 0), i + index);
                }
            }

            int System.Collections.IList.Add(object item)
            {
                return this.Add(item);
            }
        }


        internal class ItemArray : IComparer
        {
            private static int lastMask;

            private ListBox listControl;

            private Entry[] entries;

            private int count;

            private int version;

            public int Version
            {
                get
                {
                    return this.version;
                }
            }

            static ItemArray()
            {
                ListBox.ItemArray.lastMask = 1;
            }

            public ItemArray(ListBox listControl)
            {
                this.listControl = listControl;
            }

            public object Add(object item)
            {
                this.EnsureSpace(1);
                var itemArray = this;
                itemArray.version = itemArray.version + 1;
                this.entries[this.count] = new ListBox.ItemArray.Entry(item);
                var entryArray = this.entries;
                var itemArray1 = this;
                var num = itemArray1.count;
                var num1 = num;
                itemArray1.count = num + 1;
                return entryArray[num1];
            }

            public void AddRange(ICollection items)
            {
                if (items == null)
                {
                    throw new ArgumentNullException("items");
                }

                this.EnsureSpace(items.Count);
                foreach (var item in items)
                {
                    var entry = this.entries;
                    var itemArray = this;
                    var num = itemArray.count;
                    var num1 = num;
                    itemArray.count = num + 1;
                    entry[num1] = new ListBox.ItemArray.Entry(item);
                }

                var itemArray1 = this;
                itemArray1.version = itemArray1.version + 1;
            }

            public int BinarySearch(object element)
            {
                return Array.BinarySearch(this.entries, 0, this.count, element, this);
            }

            public void Clear()
            {
                this.count = 0;
                var itemArray = this;
                itemArray.version = itemArray.version + 1;
            }

            public static int CreateMask()
            {
                var num = ListBox.ItemArray.lastMask;
                ListBox.ItemArray.lastMask = ListBox.ItemArray.lastMask << 1;
                return num;
            }

            private void EnsureSpace(int elements)
            {
                if (this.entries == null)
                {
                    this.entries = new Entry[Math.Max(elements, 4)];
                    return;
                }

                if (this.count + elements >= (int)this.entries.Length)
                {
                    var num = Math.Max((int)this.entries.Length * 2, (int)this.entries.Length + elements);
                    var entryArray = new Entry[num];
                    this.entries.CopyTo(entryArray, 0);
                    this.entries = entryArray;
                }
            }

            public int GetActualIndex(int virtualIndex, int stateMask)
            {
                if (stateMask == 0)
                {
                    return virtualIndex;
                }

                var num = -1;
                for (var i = 0; i < this.count; i++)
                {
                    if ((this.entries[i].state & stateMask) != 0)
                    {
                        num++;
                        if (num == virtualIndex)
                        {
                            return i;
                        }
                    }
                }

                return -1;
            }

            public int GetCount(int stateMask)
            {
                if (stateMask == 0)
                {
                    return this.count;
                }

                var num = 0;
                for (var i = 0; i < this.count; i++)
                {
                    if ((this.entries[i].state & stateMask) != 0)
                    {
                        num++;
                    }
                }

                return num;
            }

            internal object GetEntryObject(int virtualIndex, int stateMask)
            {
                var actualIndex = this.GetActualIndex(virtualIndex, stateMask);
                if (actualIndex == -1)
                {
                    throw new IndexOutOfRangeException();
                }

                return this.entries[actualIndex];
            }

            public IEnumerator GetEnumerator(int stateMask)
            {
                return this.GetEnumerator(stateMask, false);
            }

            public IEnumerator GetEnumerator(int stateMask, bool anyBit)
            {
                return new ListBox.ItemArray.EntryEnumerator(this, stateMask, anyBit);
            }

            public object GetItem(int virtualIndex, int stateMask)
            {
                var actualIndex = this.GetActualIndex(virtualIndex, stateMask);
                if (actualIndex == -1)
                {
                    throw new IndexOutOfRangeException();
                }

                return this.entries[actualIndex].item;
            }

            public bool GetState(int index, int stateMask)
            {
                return (this.entries[index].state & stateMask) == stateMask;
            }

            public int IndexOf(object item, int stateMask)
            {
                var num = -1;
                for (var i = 0; i < this.count; i++)
                {
                    if (stateMask == 0 || (this.entries[i].state & stateMask) != 0)
                    {
                        num++;
                        if (this.entries[i].item.Equals(item))
                        {
                            return num;
                        }
                    }
                }

                return -1;
            }

            public int IndexOfIdentifier(object identifier, int stateMask)
            {
                var num = -1;
                for (var i = 0; i < this.count; i++)
                {
                    if (stateMask == 0 || (this.entries[i].state & stateMask) != 0)
                    {
                        num++;
                        if (this.entries[i] == identifier)
                        {
                            return num;
                        }
                    }
                }

                return -1;
            }

            public void Insert(int index, object item)
            {
                this.EnsureSpace(1);
                if (index < this.count)
                {
                    Array.Copy(this.entries, index, this.entries, index + 1, this.count - index);
                }

                this.entries[index] = new ListBox.ItemArray.Entry(item);
                var itemArray = this;
                itemArray.count = itemArray.count + 1;
                var itemArray1 = this;
                itemArray1.version = itemArray1.version + 1;
            }

            public void Remove(object item)
            {
                var num = this.IndexOf(item, 0);
                if (num != -1)
                {
                    this.RemoveAt(num);
                }
            }

            public void RemoveAt(int index)
            {
                var itemArray = this;
                itemArray.count = itemArray.count - 1;
                for (var i = index; i < this.count; i++)
                {
                    this.entries[i] = this.entries[i + 1];
                }

                this.entries[this.count] = null;
                var itemArray1 = this;
                itemArray1.version = itemArray1.version + 1;
            }

            public void SetItem(int index, object item)
            {
                this.entries[index].item = item;
            }

            public void SetState(int index, int stateMask, bool value)
            {
                if (!value)
                {
                    var entry = this.entries[index];
                    entry.state = entry.state & ~stateMask;
                }
                else
                {
                    var entry1 = this.entries[index];
                    entry1.state = entry1.state | stateMask;
                }

                var itemArray = this;
                itemArray.version = itemArray.version + 1;
            }

            public void Sort()
            {
                Array.Sort(this.entries, 0, this.count, this);
            }

            public void Sort(Array externalArray)
            {
                Array.Sort(externalArray, this);
            }

            int System.Collections.IComparer.Compare(object item1, object item2)
            {
                if (item1 == null)
                {
                    if (item2 == null)
                    {
                        return 0;
                    }

                    return -1;
                }

                if (item2 == null)
                {
                    return 1;
                }

                if (item1 is ListBox.ItemArray.Entry)
                {
                    item1 = ((ListBox.ItemArray.Entry)item1).item;
                }

                if (item2 is ListBox.ItemArray.Entry)
                {
                    item2 = ((ListBox.ItemArray.Entry)item2).item;
                }

                var itemText = this.listControl.GetItemText(item1);
                var str = this.listControl.GetItemText(item2);
                return CultureInfo.CurrentCulture.CompareInfo.Compare(itemText, str, CompareOptions.StringSort);
            }

            private class Entry
            {
                public object item;

                public int state;

                public Entry(object item)
                {
                    this.item = item;
                    this.state = 0;
                }
            }

            private class EntryEnumerator : IEnumerator
            {
                private ItemArray items;

                private bool anyBit;

                private int state;

                private int current;

                private int version;

                object System.Collections.IEnumerator.Current
                {
                    get
                    {
                        if (this.current == -1 || this.current == this.items.count)
                        {
                            throw new InvalidOperationException("Enumerator's current position is out of the bounds of the list.");
                        }

                        return this.items.entries[this.current].item;
                    }
                }

                public EntryEnumerator(ItemArray items, int state, bool anyBit)
                {
                    this.items = items;
                    this.state = state;
                    this.anyBit = anyBit;
                    this.version = items.version;
                    this.current = -1;
                }

                bool System.Collections.IEnumerator.MoveNext()
                {
                    if (this.version != this.items.version)
                    {
                        throw new InvalidOperationException("List that this enumerator is bound to has been modified. An enumerator can only be used if the list does not change.");
                    }

                    while (this.current < this.items.count - 1)
                    {
                        var entryEnumerator = this;
                        entryEnumerator.current = entryEnumerator.current + 1;
                        if (!this.anyBit)
                        {
                            if ((this.items.entries[this.current].state & this.state) != this.state)
                            {
                                continue;
                            }

                            return true;
                        }
                        else
                        {
                            if ((this.items.entries[this.current].state & this.state) == 0)
                            {
                                continue;
                            }

                            return true;
                        }
                    }

                    this.current = this.items.count;
                    return false;
                }

                void System.Collections.IEnumerator.Reset()
                {
                    if (this.version != this.items.version)
                    {
                        throw new InvalidOperationException("List that this enumerator is bound to has been modified. An enumerator can only be used if the list does not change.");
                    }

                    this.current = -1;
                }
            }
        }


        /// <summary>Represents the collection containing the indexes to the selected items in a <see cref="T:ListBox" />.</summary>
        public class SelectedIndexCollection : IList, ICollection, IEnumerable
        {
            private ListBox owner;

#if !PORTABLE && !WINDOWS_UWP
            [Browsable(false)]
#endif
            public int Count
            {
                get
                {
                    return this.owner.SelectedItems.Count;
                }
            }

            private ItemArray InnerArray
            {
                get
                {
                    this.owner.SelectedItems.EnsureUpToDate();
                    return this.owner.Items.InnerArray;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            public int this[int index]
            {
                get
                {
                    var entryObject = this.InnerArray.GetEntryObject(index, ListBox.SelectedObjectCollection.SelectedObjectMask);
                    return this.InnerArray.IndexOfIdentifier(entryObject, 0);
                }
            }

            bool System.Collections.ICollection.IsSynchronized
            {
                get
                {
                    return true;
                }
            }

            object System.Collections.ICollection.SyncRoot
            {
                get
                {
                    return this;
                }
            }

            bool System.Collections.IList.IsFixedSize
            {
                get
                {
                    return true;
                }
            }

            object System.Collections.IList.this[int index]
            {
                get
                {
                    return this[index];
                }

                set
                {
                    throw new NotSupportedException("ListBox.SelectedObjectCollection is read only.");
                }
            }

            public SelectedIndexCollection(ListBox owner)
            {
                this.owner = owner;
            }

            public void Add(int index)
            {
                if (this.owner != null && this.owner.Items != null && index != -1 && !this.Contains(index))
                {
                    this.owner.SetSelected(index, true);
                }
            }

            public void Clear()
            {
                if (this.owner != null)
                {
                    this.owner.UnselectAll();
                }
            }

            public bool Contains(int selectedIndex)
            {
                return this.IndexOf(selectedIndex) != -1;
            }

            public void CopyTo(Array destination, int index)
            {
                var count = this.Count;
                for (var i = 0; i < count; i++)
                {
                    destination.SetValue(this[i], i + index);
                }
            }

            public IEnumerator GetEnumerator()
            {
                return new ListBox.SelectedIndexCollection.SelectedIndexEnumerator(this);
            }

            public int IndexOf(int selectedIndex)
            {
                if (selectedIndex < 0 || selectedIndex >= this.InnerArray.GetCount(0) || !this.InnerArray.GetState(selectedIndex, ListBox.SelectedObjectCollection.SelectedObjectMask))
                {
                    return -1;
                }

                return this.InnerArray.IndexOf(this.InnerArray.GetItem(selectedIndex, 0), ListBox.SelectedObjectCollection.SelectedObjectMask);
            }

            public void Remove(int index)
            {
                if (this.owner != null && this.owner.Items != null && index != -1 && this.Contains(index))
                {
                    this.owner.SetSelected(index, false);
                }
            }

            int System.Collections.IList.Add(object value)
            {
                throw new NotSupportedException("ListBox.SelectedObjectCollection is read only.");
            }

            void System.Collections.IList.Clear()
            {
                throw new NotSupportedException("ListBox.SelectedObjectCollection is read only.");
            }

            bool System.Collections.IList.Contains(object selectedIndex)
            {
                if (!(selectedIndex is int))
                {
                    return false;
                }

                return this.Contains((int)selectedIndex);
            }

            int System.Collections.IList.IndexOf(object selectedIndex)
            {
                if (!(selectedIndex is int))
                {
                    return -1;
                }

                return this.IndexOf((int)selectedIndex);
            }

            void System.Collections.IList.Insert(int index, object value)
            {
                throw new NotSupportedException("ListBox.SelectedObjectCollection is read only.");
            }

            void System.Collections.IList.Remove(object value)
            {
                throw new NotSupportedException("ListBox.SelectedObjectCollection is read only.");
            }

            void System.Collections.IList.RemoveAt(int index)
            {
                throw new NotSupportedException("ListBox.SelectedObjectCollection is read only.");
            }

            private class SelectedIndexEnumerator : IEnumerator
            {
                private SelectedIndexCollection items;

                private int current;

                object System.Collections.IEnumerator.Current
                {
                    get
                    {
                        if (this.current == -1 || this.current == this.items.Count)
                        {
                            throw new InvalidOperationException("Enumerator's current position is out of the bounds of the list.");
                        }

                        return this.items[this.current];
                    }
                }

                public SelectedIndexEnumerator(SelectedIndexCollection items)
                {
                    this.items = items;
                    this.current = -1;
                }

                bool System.Collections.IEnumerator.MoveNext()
                {
                    if (this.current >= this.items.Count - 1)
                    {
                        this.current = this.items.Count;
                        return false;
                    }

                    var selectedIndexEnumerator = this;
                    selectedIndexEnumerator.current = selectedIndexEnumerator.current + 1;
                    return true;
                }

                void System.Collections.IEnumerator.Reset()
                {
                    this.current = -1;
                }
            }
        }

        /// <summary>Represents the collection of selected items in the <see cref="T:ListBox" />.</summary>
        public class SelectedObjectCollection : IList, ICollection, IEnumerable
        {
            internal static int SelectedObjectMask;

            private ListBox owner;

            private bool stateDirty;

            private int lastVersion;

            private int count;

            public int Count
            {
                get
                {
                    switch (this.owner.selectionModeChanging ? this.owner.cachedSelectionMode : this.owner.selectionMode)
                    {
                        case SelectionMode.None:
                            return 0;

                        case SelectionMode.Single:
                            return this.owner.SelectedIndex >= 0 ? 1 : 0;

                        case SelectionMode.Multiple:
                        case SelectionMode.Extended:
                            if (this.lastVersion != this.InnerArray.Version)
                            {
                                this.lastVersion = this.InnerArray.Version;
                                this.count = this.InnerArray.GetCount(ListBox.SelectedObjectCollection.SelectedObjectMask);
                            }

                            return this.count;
                    }

                    return 0;
                }
            }

            private ItemArray InnerArray
            {
                get
                {
                    this.EnsureUpToDate();
                    return this.owner.Items.InnerArray;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }

#if !PORTABLE && !WINDOWS_UWP
            [Browsable(false)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#endif
            public object this[int index]
            {
                get
                {
                    return this.InnerArray.GetItem(index, ListBox.SelectedObjectCollection.SelectedObjectMask);
                }

                set
                {
                    throw new NotSupportedException("ListBox.SelectedObjectCollection is read only.");
                }
            }

            bool System.Collections.ICollection.IsSynchronized
            {
                get
                {
                    return false;
                }
            }

            object System.Collections.ICollection.SyncRoot
            {
                get
                {
                    return this;
                }
            }

            bool System.Collections.IList.IsFixedSize
            {
                get
                {
                    return true;
                }
            }

            static SelectedObjectCollection()
            {
                ListBox.SelectedObjectCollection.SelectedObjectMask = ListBox.ItemArray.CreateMask();
            }

            public SelectedObjectCollection(ListBox owner)
            {
                this.owner = owner;
                this.stateDirty = true;
                this.lastVersion = -1;
            }

            public void Add(object value)
            {
                if (this.owner != null)
                {
                    var items = this.owner.Items;
                    if (items != null && value != null)
                    {
                        var num = items.IndexOf(value);
                        if (num != -1 && !this.GetSelected(num))
                        {
                            this.owner.SelectedIndex = num;
                        }
                    }
                }
            }

            public void Clear()
            {
                if (this.owner != null)
                {
                    this.owner.UnselectAll();
                }
            }

            public bool Contains(object selectedObject)
            {
                return this.IndexOf(selectedObject) != -1;
            }

            public void CopyTo(Array destination, int index)
            {
                var count = this.InnerArray.GetCount(ListBox.SelectedObjectCollection.SelectedObjectMask);
                for (var i = 0; i < count; i++)
                {
                    destination.SetValue(this.InnerArray.GetItem(i, ListBox.SelectedObjectCollection.SelectedObjectMask), i + index);
                }
            }

            internal void Dirty()
            {
                this.stateDirty = true;
            }

            internal void EnsureUpToDate()
            {
                if (this.stateDirty)
                {
                    this.stateDirty = false;

                    // if (this.owner.IsHandleCreated)
                    // {
                    // this.owner.NativeUpdateSelection();
                    // }
                }
            }

            public IEnumerator GetEnumerator()
            {
                return this.InnerArray.GetEnumerator(ListBox.SelectedObjectCollection.SelectedObjectMask);
            }

            internal object GetObjectAt(int index)
            {
                return this.InnerArray.GetEntryObject(index, ListBox.SelectedObjectCollection.SelectedObjectMask);
            }

            internal bool GetSelected(int index)
            {
                return this.InnerArray.GetState(index, ListBox.SelectedObjectCollection.SelectedObjectMask);
            }

            public int IndexOf(object selectedObject)
            {
                return this.InnerArray.IndexOf(selectedObject, ListBox.SelectedObjectCollection.SelectedObjectMask);
            }

            internal void PushSelectionIntoNativeListBox(int index)
            {
                if (this.owner.Items.InnerArray.GetState(index, ListBox.SelectedObjectCollection.SelectedObjectMask))
                {
                    this.owner.SetSelected(index, true);
                }
            }

            public void Remove(object value)
            {
                if (this.owner != null)
                {
                    var items = this.owner.Items;
                    if (items != null & value != null)
                    {
                        var num = items.IndexOf(value);
                        if (num != -1 && this.GetSelected(num))
                        {
                            this.owner.SetSelected(num, false);
                        }
                    }
                }
            }

            internal void SetSelected(int index, bool value)
            {
                this.InnerArray.SetState(index, ListBox.SelectedObjectCollection.SelectedObjectMask, value);
            }

            int System.Collections.IList.Add(object value)
            {
                throw new NotSupportedException("ListBox.SelectedObjectCollection is read only.");
            }

            void System.Collections.IList.Clear()
            {
                throw new NotSupportedException("ListBox.SelectedObjectCollection is read only.");
            }

            void System.Collections.IList.Insert(int index, object value)
            {
                throw new NotSupportedException("ListBox.SelectedObjectCollection is read only.");
            }

            void System.Collections.IList.Remove(object value)
            {
                throw new NotSupportedException("ListBox.SelectedObjectCollection is read only.");
            }

            void System.Collections.IList.RemoveAt(int index)
            {
                throw new NotSupportedException("ListBox.SelectedObjectCollection is read only.");
            }
        }
    }
}