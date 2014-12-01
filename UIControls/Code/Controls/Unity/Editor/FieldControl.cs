/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.UIControls.Unity.Editor
{
    public abstract class FieldControl : Control
    {
        public string Text { get; set; }

        public FieldControl(string text)
        {
            this.Text = text;
        }

        public FieldControl()
        {                                             
        }
    }
}