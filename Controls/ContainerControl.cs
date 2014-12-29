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

    public class ContainerControl : Control
    {
        /// <summary>
        /// Holds the child controls.
        /// </summary>
        protected IList<Control> children;

        /// <summary>
        /// Gets or sets the child control list.
        /// </summary> 
        public virtual IList<Control> Children
        {
            get
            {
                return this.children;
            }

            set
            {
                this.children = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerControl"/> class.
        /// </summary>
        public ContainerControl()
        {
            this.children = new List<Control>();
        }       
    }
}