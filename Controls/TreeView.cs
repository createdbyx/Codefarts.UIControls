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

    /// <summary>
    /// Displays a hierarchical collection of labeled items, each represented by a <see cref="TreeNode" />.
    /// </summary>
    public class TreeView : ScrollViewer
    {
        /// <summary>
        /// The selected node used by the <see cref="SelectedNode"/> property.
        /// </summary>
        protected TreeNode selectedNode;

        /// <summary>
        /// The node list used by the <see cref="Nodes"/> property.
        /// </summary>
        protected List<TreeNode> nodes;

        /// <summary>
        /// Gets the collection of tree nodes that are assigned to the tree view control.
        /// </summary>
        /// <returns>
        /// A <see cref="List{TreeNode}" /> that represents the tree nodes assigned to the tree view control.
        /// </returns>
        public virtual List<TreeNode> Nodes
        {
            get
            {
                return this.nodes;
            }

            set
            {
                var changed = this.nodes != value;
                this.nodes = value;
                if (changed)
                {
                    this.OnPropertyChanged("Nodes");
                }
            }
        }

        /// <summary>Gets or sets the tree node that is currently selected in the tree view control.</summary>
        /// <returns>The <see cref="TreeNode" /> that is currently selected in the tree view control.</returns>
        public virtual TreeNode SelectedNode
        {
            get
            {
                return this.selectedNode;
            }

            set
            {
                var changed = this.selectedNode != value;
                this.selectedNode = value;
                if (changed)
                {
                    this.OnPropertyChanged("SelectedNode");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeView" /> class.
        /// </summary>
        public TreeView() : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeView"/> class.
        /// </summary>
        /// <param name="name">
        /// The control name.
        /// </param>
        public TreeView(string name) : base(name)
        {
            this.canFocus = true;
            this.isTabStop = true;
            this.nodes = new List<TreeNode>();
        }

        /// <returns>
        /// The default <see cref="Size" /> of the control.
        /// </returns>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(121, 97);
            }
        }
    }
}