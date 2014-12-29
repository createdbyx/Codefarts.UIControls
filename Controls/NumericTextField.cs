namespace Codefarts.UIControls
{
    using System;
    using System.Globalization;

    public class NumericTextField : TextBox
    {
        private float minimum;
        private float maximum = 1;

        private int precision;

        private float value;

        public event EventHandler<RoutedPropertyChangedEventArgs<float>> ValueChanged;

        public override string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = this.SanitizeValue(value, this.minimum, this.maximum);
            }
        }

        private string SanitizeValue(string value, float min, float max)
        {
            //  var acceptedValue = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.' };
            var i = 0;
            var periodIndex = -1;
            var newValue = value.Trim();
            while (i < newValue.Length)
            {
                switch (newValue[i])
                {
                    case '-': // remove - chars that are not at the beginning
                        if (i > 0)
                        {
                            newValue = newValue.Remove(i, 1);
                            continue;
                        }

                        break;

                    case '0':  // remove any zeros at the start of the number
                        if (i == 0)
                        {
                            newValue = newValue.Remove(i, 1);
                            continue;
                        }

                        break;

                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        // do nothing and allow the character
                        break;

                    case '.':
                        // store the first period index
                        periodIndex = periodIndex == -1 ? i : periodIndex;

                        // check if the period character is not hte same as the forst period we found
                        // if it is not then remove it
                        if (periodIndex != i)
                        {
                            newValue = newValue.Remove(i, 1);
                            continue;
                        }

                        break;

                    default:
                        // unrecognized character so remove it
                        newValue = newValue.Remove(i, 1);
                        break;
                }

                i++;
            }

            // try and parse the result
            float result;
            if (float.TryParse(newValue, out result))
            {
                // if parsing a success then round the value to precision and store the value
                if (result < min)
                {
                    newValue = Math.Round(min, this.Precision).ToString(CultureInfo.InvariantCulture);
                }
                else if (result > max)
                {
                    newValue = Math.Round(max, this.Precision).ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    newValue = Math.Round(result, this.precision).ToString(CultureInfo.InvariantCulture);
                }

                this.Value = result;
            }
            else
            {
                // could not parse value so use existing value
                newValue = Math.Round(Math.Max(this.minimum, 0), this.precision).ToString(CultureInfo.InvariantCulture);// string.IsNullOrEmpty(newValue) ? string.Empty : value;
            }

            return newValue;
        }

        public virtual float Value
        {
            get
            {
                return this.value;
            }

            set
            {
                var oldValue = this.value;
                var min = Math.Min(this.maximum, this.minimum);
                var max = Math.Max(this.maximum, this.minimum);
                value = value > max ? max : value;
                value = value < min ? min : value;
                var changed = this.value != value;
                if (!changed)
                {
                    return;
                }

                this.value = value;
                this.text = value.ToString(CultureInfo.InvariantCulture);
                var handler = this.ValueChanged;
                if (handler != null)
                {
                    handler(this, new RoutedPropertyChangedEventArgs<float>(oldValue, value));
                }
            }
        }

        public virtual int Precision
        {
            get
            {
                return this.precision;
            }

            set
            {
                if (value < 0)
                {
                    this.precision = 0;
                }
                else if (value > 28)
                {
                    this.precision = 28;
                }
                else
                {
                    this.precision = value;
                }
            }
        }

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
            }
        }
    }
}