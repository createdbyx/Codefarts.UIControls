/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

using System;
using UnityEngine;

namespace Codefarts.UIControls
{
    using Codefarts.UIControls.Code;
    using Codefarts.UIControls.Unity;

    public class ScrollBar : CustomControl
    {
        private float minimum;
        private float maximum=1;
        private float value;

        public Orientation Orientation { get; set; }

        /// <summary>
        ///  Gets or sets a minimum rotation angle in degrees. 
        /// </summary>
        public float Minimum
        {
            get { return this.minimum; }
            set
            {
                if (value > this.maximum) throw new ArgumentOutOfRangeException("Minimum value can not be greater then Maximum value.");
                this.minimum = value;
                //  if (this.minimum > this.maximum) this.minimum = this.maximum;
            }
        }

        /// <summary>
        /// Gets or sets a maximum rotation angle in degrees. 
        /// </summary>
        public float Maximum
        {
            get { return this.maximum; }
            set
            {
                if (value < this.minimum) throw new ArgumentOutOfRangeException("Maximum value can not be less then Minimum value.");
                this.maximum = value;
                // if (this.maximum < this.minimum) this.maximum = this.minimum;
            }
        }

        public float Value
        {
            get { return this.value; }
            set
            {
                if (value > this.maximum) throw new ArgumentOutOfRangeException("Value can not be greater then Maximum value.");
                if (value < this.minimum) throw new ArgumentOutOfRangeException("Value can not be less then Minimum value.");
                this.value = value;
            }
        }

        public override void OnDraw(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            switch (this.Orientation)
            {
                case Orientation.Horizontial:
                    this.Value = GUILayout.HorizontalScrollbar(this.value, 1, this.minimum, this.maximum, ControlDrawingHelpers.StandardDimentionOptions(this));
                    break;
                case Orientation.Vertical:
                    this.Value = GUILayout.VerticalScrollbar(this.value, 1, this.minimum, this.maximum, ControlDrawingHelpers.StandardDimentionOptions(this));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void OnUpdate(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {   
        }
    }
}