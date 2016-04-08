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
    using System.ComponentModel;

    /// <summary>
    /// Represents a node of a <see cref="TreeView" />.
    /// </summary>
    public class TreeNode : INotifyPropertyChanged
    {
        /// <summary>
        /// The backing field for the <see cref="Nodes"/> property.
        /// </summary>
        protected List<TreeNode> nodes;

        /// <summary>
        /// The backing field for the <see cref="Name"/> property.
        /// </summary>
        protected string name;

        /// <summary>
        /// The backing field for the <see cref="Text"/> property.
        /// </summary>
        protected string text;

        /// <summary>
        /// The backing field for the <see cref="IsExpanded"/> property.
        /// </summary>
        protected bool isExpanded;

        /// <summary>
        /// The backing field for the <see cref="IsVisible"/> property.
        /// </summary>
        protected bool isVisible;

        /// <summary>
        /// The backing field for the <see cref="Tag"/> property.
        /// </summary>
        protected object tag;

        /// <summary>
        /// Gets the collection of <see cref="TreeNode" /> objects assigned to the current tree node.
        /// </summary>
        /// <returns>
        /// A <see cref="List{TreeNode}"/> that represents the tree nodes assigned to the current tree node.
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

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNode"/> class.
        /// </summary>
        public TreeNode()
        {
            this.nodes = new List<TreeNode>();
            this.isVisible = true;
            this.isExpanded = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNode" /> class.
        /// </summary>
        /// <param name="text">Sets the <see cref="Text"/> property.</param>
        public TreeNode(string text) : this()
        {
            this.text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        /// <param name="text">Sets the <see cref="Text"/> property.</param>
        /// <param name="nodes">The nodes that will be added as child nodes.</param>
        public TreeNode(string text, IEnumerable<TreeNode> nodes) : this(text)
        {
            this.nodes.AddRange(nodes);
        }

        /// <summary>
        /// Gets or sets the text displayed in the label of the tree node.
        /// </summary>
        /// <returns>
        /// The text displayed in the label of the tree node.
        /// </returns>
        public virtual string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                var changed = this.text != value;
                this.text = value;
                if (changed)
                {
                    this.OnPropertyChanged("Text");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the tree node.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents the name of the tree node.
        /// </returns>
        public virtual string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                var changed = this.name != value;
                this.name = value;
                if (changed)
                {
                    this.OnPropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets the object that contains data about the tree node.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Object" /> that contains data about the tree node. The default is null.
        /// </returns>
        public virtual object Tag
        {
            get
            {
                return this.tag;
            }

            set
            {
                var changed = this.tag != value;
                this.tag = value;
                if (changed)
                {
                    this.OnPropertyChanged("Tag");
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the tree node is visible or partially visible.
        /// </summary>
        /// <returns>
        /// true if the tree node is visible or partially visible; otherwise, false.
        /// </returns>
        public virtual bool IsVisible
        {
            get
            {
                return this.isVisible;
            }

            set
            {
                var changed = this.isVisible != value;
                this.isVisible = value;
                if (changed)
                {
                    this.OnPropertyChanged("IsVisible");
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the tree node is in the expanded state.
        /// </summary>
        /// <returns>
        /// true if the tree node is in the expanded state; otherwise, false.
        /// </returns>
        public virtual bool IsExpanded
        {
            get
            {
                return this.isExpanded;
            }

            set
            {
                var changed = this.isExpanded != value;
                this.isExpanded = value;
                if (changed)
                {
                    this.OnPropertyChanged("IsExpanded");
                }
            }
        }

        /// <summary>
        /// Occurs when a property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Call this. method to raise the  <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}