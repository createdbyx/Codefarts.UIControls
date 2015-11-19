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
    using System.Collections.Generic;

    public class TreeViewNode
    {
        public List<TreeViewNode> Nodes { get; set; }

        public TreeViewNode()
        {
            this.Nodes = new List<TreeViewNode>();
            this.IsVisible = true;
            this.IsExpanded = true;
        }

        public object Value { get; set; }

        public bool IsVisible { get; set; }

        public bool IsExpanded { get; set; }
    }
}