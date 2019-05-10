namespace Codefarts.UIControls
{
    using System;

    /// <summary>
    /// Represents a control that has a value within a specific range.
    /// </summary>
    public abstract class RangeBase : Control
    {
        /// <summary>
        /// The backing field for the <see cref="Minimum"/> property.
        /// </summary>
        protected float minimum;

        /// <summary>
        /// The backing field for the <see cref="Maximum"/> property.
        /// </summary>
        protected float maximum = 1;

        /// <summary>
        /// The backing field for the <see cref="Precision"/> property.
        /// </summary>
        protected int precision;

        /// <summary>
        /// The backing field for the <see cref="Value"/> property.
        /// </summary>
        protected float value;

        /// <summary>
        /// The backing field for the <see cref="LargeChange"/> property.
        /// </summary>
        protected float largeChange = 1;

        /// <summary>
        /// The backing field for the <see cref="LargeChange"/> property.
        /// </summary>
        protected float smallChange = 0.1f;


        /// <summary>
        /// Gets or sets a <see cref="RangeBase.Value" /> to be added to or subtracted from the <see cref="RangeBase.Value" /> of a <see cref="RangeBase" /> control.
        /// </summary>
        /// <returns>
        /// <see cref="RangeBase.Value" /> to add to or subtract from the <see cref="RangeBase.Value" /> of the <see cref="RangeBase" /> element. The default is 0.1.
        /// </returns>
        public virtual float SmallChange
        {
            get
            {
                return this.smallChange;
            }

            set
            {
                var changed = Math.Abs(this.smallChange - value) > float.Epsilon;
                this.smallChange = value;
                if (changed)
                {
                    this.OnPropertyChanged("SmallChange");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value to be added to or subtracted from the <see cref="RangeBase.Value" /> of a <see cref="RangeBase" /> control.
        /// </summary>
        /// <returns>
        /// <see cref="RangeBase.Value" /> to add to or subtract from the <see cref="RangeBase.Value" /> of the <see cref="RangeBase" /> element. The default is 1.</returns>
        public virtual float LargeChange
        {
            get
            {
                return this.largeChange;
            }

            set
            {
                var changed = Math.Abs(this.largeChange - value) > float.Epsilon;
                this.largeChange = value;
                if (changed)
                {
                    this.OnPropertyChanged("LargeChange");
                }
            }
        }

        /// <summary>
        /// Gets or sets the numeric value for the <see cref="RangeBase"/>.
        /// </summary>
        public virtual float Value
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
                var changed = Math.Abs(this.value - value) > float.Epsilon;
                this.value = value;
                if (changed)
                {
                    this.OnPropertyChanged("Value");
                }
            }
        }

        /// <summary>
        /// Gets or sets the decimal precision from 0 to 28.
        /// </summary>
        public virtual int Precision
        {
            get
            {
                return this.precision;
            }

            set
            {
                value = value < 0 ? 0 : value;
                value = value > 28 ? 28 : value;
                var changed = this.precision != value;
                this.precision = value;
                if (changed)
                {
                    this.OnPropertyChanged("Precision");
                }
            }
        }

        /// <summary>
        ///  Gets or sets a minimum allowable value.
        /// </summary>
        public virtual float Minimum
        {
            get
            {
                return this.minimum;
            }

            set
            {
                var changed = Math.Abs(this.minimum - value) > float.Epsilon;
                this.minimum = value;
                if (changed)
                {
                    this.OnPropertyChanged("Minimum");
                }

                this.Value = this.value;
            }
        }

        /// <summary>
        /// Gets or sets a maximum allowable value.
        /// </summary>
        public virtual float Maximum
        {
            get
            {
                return this.maximum;
            }

            set
            {
                var changed = Math.Abs(this.maximum - value) > float.Epsilon;
                this.maximum = value;
                if (changed)
                {
                    this.OnPropertyChanged("Maximum");
                }

                this.Value = this.value;
            }
        }
    }
}