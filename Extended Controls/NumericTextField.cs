namespace Codefarts.UIControls
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Provides a <see cref="TextBox"/> that only accepts numerical input.
    /// </summary>
    public class NumericTextField : RangeBase
    {
        /// <summary>
        /// The underlting <see cref="TextBox"/> child control.
        /// </summary>
        private TextBox txtValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericTextField"/> class.
        /// </summary>
        public NumericTextField()
        {
            this.txtValue = new TextBox()
            {
                AcceptsReturn = false,
                AcceptsTab = false,
                Width = this.Width,
                Height = this.Height,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Text = this.value.ToString(CultureInfo.InvariantCulture),
            };

            this.Controls.Add(this.txtValue);
            this.txtValue.PropertyChanged += this.TextValuePropertyChanged;
        }

        #region Overrides of Control

        /// <summary>Gets the default size of the control.</summary>
        /// <returns>The default <see cref="Control.Size" /> of the control.</returns>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(100, 33);
            }
        }

        #endregion

        /// <summary>
        /// Handles the property changed events for the <see cref="txtValue"/> field.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void TextValuePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Text":
                    this.Text = this.txtValue.Text;
                    break;
            }
        }

        /// <summary>
        /// Gets or sets the text contents of the text box.
        /// </summary>
        /// <remarks>This is a wrapper for the underlying <see cref="TextBox.Text"/> property that will sanitize the value before setting the underlying setter.</remarks>
        public virtual string Text
        {
            get
            {
                return this.txtValue.Text;
            }

            set
            {
                var result = this.SanitizeValue(value, this.minimum, this.maximum);
                var changed = result != value;
                this.txtValue.Text = result;
                if (changed)
                {
                    this.OnPropertyChanged("Text");
                }
            }
        }

        /// <summary>
        /// Sanitizes the value and prevent unwanted characters from being entered into the text field.
        /// </summary>
        /// <param name="value">The value to sanitize.</param>
        /// <param name="min">The minimum allowable value.</param>
        /// <param name="max">The maximum allowable value.</param>
        /// <returns>The sanitized value.</returns>
        protected virtual string SanitizeValue(string value, float min, float max)
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
                newValue = Math.Round(Math.Max(this.minimum, 0), this.precision).ToString(CultureInfo.InvariantCulture);
            }

            return newValue;
        }

        /// <summary>
        /// Gets or sets the numeric value for the <see cref="NumericTextField"/>.
        /// </summary>
        public override float Value
        {
            get
            {
                return this.value;
            }

            set
            {
                base.Value = value;
                this.txtValue.Text = value.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}