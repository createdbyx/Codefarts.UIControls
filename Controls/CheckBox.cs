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

    public class CheckBox : Control
    {
        public event EventHandler Checked;
        public string Text { get; set; }
        private bool isChecked;

        public bool IsChecked
        {
            get { return this.isChecked; }
            set
            {
                var changed = this.isChecked != value;
                this.isChecked = value;
                if (changed)
                {
                    this.OnChecked(EventArgs.Empty);
                }
            }
        }

        public void OnChecked(EventArgs e)
        {
            if (this.Checked != null)
            {
                this.Checked(this, e);
            }
        }
    }
}