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
    using System;

    using CBX.Common;
    using Codefarts.UIControls;
    using Codefarts.UIControls.Code;

    using UnityEditor;

    using UnityEngine;

    /// <summary>
    /// The color picker.
    /// </summary>
    public class ColorPicker : CustomControl
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The color value.
        /// </summary>
        private Color color;

        /// <summary>
        /// The color changed event.
        /// </summary>
        public event EventHandler ColorChanged;

        /// <summary>
        /// Raises the <see cref="ColorChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        public void OnColorChanged(EventArgs e)
        {
            var handler = this.ColorChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color Color
        {
            get
            {
                return this.color;
            }

            set
            {
                var changed = this.color.a != value.a || this.color.r != value.r || this.color.g != value.g || this.color.b != value.b;
                this.color = value;
                if (changed)
                {
                    this.OnColorChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPicker"/> class.
        /// </summary>
        public ColorPicker()
        {
            this.color = Color.white;
        }

        public override void OnDraw(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                this.Color = EditorGUILayout.ColorField(this.Color, ControlDrawingHelpers.StandardDimentionOptions(this));
            }
            else
            {
                this.Color = EditorGUILayout.ColorField(this.Text, this.Color, ControlDrawingHelpers.StandardDimentionOptions(this));
            }
        }

        public override void OnUpdate(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
        }
    }
}