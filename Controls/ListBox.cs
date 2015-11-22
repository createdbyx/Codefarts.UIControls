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
        public ItemsCollection Items { get; internal set; }

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

        public event EventHandler<ListBoxItemInformationArgs> DrawItem;
        public event EventHandler<ListBoxItemInformationArgs> MeasureItem;

        public virtual DrawMode DrawMode
        {
            get
            {
                return this.drawMode;
            }

            set
            {
                this.drawMode = value;
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

        public SelectionMode SelectionMode { get; set; }

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

        /// <summary>
        /// Initializes a new instance of the <see cref="ListBox"/> class.
        /// </summary>
        public ListBox()
            : base()
        {
            this.HorizontialScrollBarVisibility = ScrollBarVisibility.Auto;
            this.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.Items = new ItemsCollection();
            this.Items.CollectionChanged += this.ItemsCollectionChanged;
            this.selectedItems = new List<object>();
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

                case NotifyCollectionChangedAction.Move:
                    break;

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