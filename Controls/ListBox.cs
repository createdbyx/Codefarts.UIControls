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
    using System.Collections.Generic;

    public class ListBox : ScrollViewer
    {
        public event EventHandler SelectionChanged;
        private new IList<Control> Children { get; set; }
        public ItemsCollection Items { get; internal set; }
        private int selectedIndex;


        public void OnSelectionChanged()
        {
            if (this.SelectionChanged != null) this.SelectionChanged(this, EventArgs.Empty);
        }

        public int SelectedIndex
        {
            get { return this.selectedIndex; }
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

        public ListBox()
            : base()
        {
            this.HorizontialScrollBarVisibility = ScrollBarVisibility.Auto;
            this.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.Items = new ItemsCollection();
        }
    }
}