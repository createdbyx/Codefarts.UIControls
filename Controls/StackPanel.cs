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
    public class StackPanel : ItemsControl
    {
        public StackPanel(Orientation orientation)
            : this()
        {
            this.Orientation = orientation;
        }

        public StackPanel()
            : base()
        {
        }

        public virtual Orientation Orientation { get; set; }
    }
}