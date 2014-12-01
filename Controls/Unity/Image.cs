/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

using UnityEngine;

namespace Codefarts.UIControls.Unity
{
    public class Image : Control
    {
        public Texture2D Source { get; set; }
        public Stretch Stretch { get; set; }

        public Image()
        {
            this.Stretch = Stretch.None;
        }
    }
}