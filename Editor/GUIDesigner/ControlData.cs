/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace CBX.Unity.Editor.GUIDesigner
{
    using System;

    using Codefarts.UIControls;

    internal class ControlData
    {
        public string Name { get; set; }

        public Type Type { get; set; }

        public Control Reference { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}