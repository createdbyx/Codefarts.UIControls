namespace Codefarts.UIControls
{
    using System;

    public abstract class RangeBase : Control
    {
        private float minimum;
        private float maximum = 10;
        private float value;

        public event EventHandler<PropertyChangedEventArgs<float>> ValueChanged;

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
                var changed = this.value != value;
                if (!changed)
                {
                    return;
                }

                var oldValue = this.value;
                this.value = value;
                var handler = this.ValueChanged;
                if (changed && handler != null)
                {
                    handler(this, new PropertyChangedEventArgs<float>(oldValue, value) { Source = this });
                }
            }
        }
    }
}