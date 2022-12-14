/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

using System.Collections.Generic;

namespace Codefarts.UIControls
{
    public class Expander : Control
    {
        public bool IsExpanded { get; set; }
        public IList<Control> Children { get; set; }
        public string Header { get; set; }

        public Expander()
        {
            this.Header = string.Empty;
            this.Children = new List<Control>();
        }
    }
}