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
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
                                              
    public class ListBox : ScrollViewer
    {
        public event EventHandler SelectionChanged;
        protected ItemsCollection items;

        public virtual ItemsCollection Items
        {
            get
            {
                return this.items;
            }

            internal set
            {
                var changed = this.items!=value;
                this.items = value;
                if (changed)
                {
                    this.OnPropertyChanged("Items");
                }
            }
        }

        public IList SelectedItems
        {
            get
            {
                return this.selectedItems;
            }
        }

        private int selectedIndex = -1;

        private List<object> selectedItems;

        protected DrawMode drawMode = DrawMode.Normal;

        protected bool scrollAlwaysVisible;

        private SelectionMode selectionMode;

        private Func<object, string> displayMemberCallback;


        public event EventHandler<ListBoxItemInformationArgs> DrawItem;
        public event EventHandler<ListBoxItemInformationArgs> MeasureItem;

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

        public void OnSelectionChanged()
        {
            if (this.SelectionChanged != null)
            {
                this.SelectionChanged(this, EventArgs.Empty);
            }
        }

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
                    this.OnPropertyChanged("SelectionMode");
                }
            }
        }

        public void SelectAll()
        {
            if (this.SelectionMode != SelectionMode.Single)
            {
                throw new NotSupportedException("Can ont select all when SelectionMode is single.");
            }

            this.selectedItems.Clear();
            this.selectedItems.AddRange(this.Items);
        }

        public void UnselectAll()
        {
            this.selectedItems.Clear();
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

        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }

            set
            {
                value = value < -1 ? -1 : value;
                value = value > this.Items.Count - 1 ? this.Items.Count - 1 : value;
                var changed = this.selectedIndex != value;
                this.selectedIndex = value;
                if (changed)
                {
                    this.OnSelectionChanged();
                }
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
        /// Initializes a new instance of the <see cref="ListBox"/> class.
        /// </summary>
        public ListBox()
        {
            this.items = new ItemsCollection();
            this.items.CollectionChanged += this.ItemsCollectionChanged;
            this.selectedItems = new List<object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListBox"/> class.
        /// </summary>
        /// <param name="name">The name of the control.</param>
        public ListBox(string name) : this()
        {
            this.name = name;
        }

        private void ItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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

        public virtual void OnMeasureItem(ListBoxItemInformationArgs e)
        {
            var handler = this.MeasureItem;
            if (handler != null)
            {
                handler(this, e);
            }
        }

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