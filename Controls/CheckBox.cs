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
    using System;

#if UNITY3D
    using UnityEngine;
#endif
#if WINDOWS
    using Microsoft.Xna.Framework.Graphics;
#endif

    public class CheckBox : Control
    {
        public event EventHandler Checked;
        public virtual string Text { get; set; }

        private bool isChecked;

        protected CheckBox(Texture2D texture)
            : this()
        {
            this.Texture = texture;
        }

        public CheckBox()
            : base()
        {
        }

#if UNITY3D || WINDOWS
        /// <summary>
        /// Gets or sets Texture.
        /// </summary>
        public virtual Texture Texture { get; set; }
#endif

        public virtual bool IsChecked
        {
            get
            {
                return this.isChecked;
            }

            set
            {
                var changed = this.isChecked != value;
                this.isChecked = value;
                if (changed)
                {
                    this.OnChecked(EventArgs.Empty);
                }
            }
        }

        public void OnChecked(EventArgs e)
        {
            var handler = this.Checked;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}