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
    public class ScrollViewer : ContainerControl
    {
        public ScrollBarVisibility HorizontialScrollBarVisibility { get; set; }
        public ScrollBarVisibility VerticalScrollBarVisibility { get; set; }
        public float HorizontialOffset { get; set; }
        public float VerticalOffset { get; set; }

        public ScrollViewer()
        {
            this.HorizontialScrollBarVisibility = ScrollBarVisibility.Hidden;
            this.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }
    }
}