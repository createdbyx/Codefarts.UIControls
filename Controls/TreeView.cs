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
    using System;

    using Codefarts.UIControls.Unity;

    public class TreeView : ScrollViewer
    {
        private TreeViewNode selectedNode;

        public List<TreeViewNode> Nodes { get; set; }

        public event EventHandler SelectionChanged;

        public TreeViewNode SelectedNode
        {
            get
            {
                return this.selectedNode;
            }

            set
            {
                var changed = this.selectedNode != value;
                this.selectedNode = value;
                var handler = this.SelectionChanged;
                if (changed && handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
        }

        public TreeView()
            : base()
        {
            this.Nodes = new List<TreeViewNode>();
        }
    }
}