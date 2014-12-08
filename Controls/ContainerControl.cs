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
        protected IList<Control> children;

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

        public ContainerControl()
        {
            this.children = new List<Control>();
        }       
    }
}