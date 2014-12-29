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

    public class ScrollBar : Control
    {
        private float minimum;
        private float maximum = 1;
        private float value;

        public Orientation Orientation { get; set; }

        /// <summary>
        ///  Gets or sets a minimum rotation angle in degrees. 
        /// </summary>
        public float Minimum
        {
            get
            {
                return this.minimum;
            }

            set
            {
                //if (value > this.maximum)
                //{
                //    throw new ArgumentOutOfRangeException("Minimum value can not be greater then Maximum value.");
                //}

                this.minimum = value;
                //  if (this.minimum > this.maximum) this.minimum = this.maximum;
            }
        }

        /// <summary>
        /// Gets or sets a maximum rotation angle in degrees. 
        /// </summary>
        public float Maximum
        {
            get
            {
                return this.maximum;
            }

            set
            {
                //if (value < this.minimum)
                //{
                //    throw new ArgumentOutOfRangeException("Maximum value can not be less then Minimum value.");
                //}

                this.maximum = value;
                // if (this.maximum < this.minimum) this.maximum = this.minimum;
            }
        }

        public float Value
        {
            get
            {
                return this.value;
            }

            set
            {
                var min = Math.Min(this.maximum, this.minimum);
                var max = Math.Max(this.maximum, this.minimum);
                value = value > max ? max : value;
                value = value < min ? min : value;   
                this.value = value;
            }
        }
    }
}