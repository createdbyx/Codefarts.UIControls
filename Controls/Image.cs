/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.UIControls.Controls
{
    public class Image : Control
    {
        public ImageSource Source { get; set; }
        public Stretch Stretch { get; set; }

        public Image()
        {
            this.Stretch = Stretch.None;
        }
    }
}