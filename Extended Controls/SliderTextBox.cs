namespace Codefarts.UIControls
{
    using System.ComponentModel;

    using Codefarts.UIControls;

    /// <summary>
    /// Provides a control with a <see cref="Slider"/>, <see cref="NumericTextField"/>, and a <see cref="TextBlock"/> label.
    /// </summary>
    public class SliderTextBox : RangeBase
    {
        /// <summary>
        /// The numeric text field for changing the value by hand.
        /// </summary>
        protected NumericTextField textField;

        /// <summary>
        /// The slider control for changing the value via a slider.
        /// </summary>
        protected Slider slider;

        /// <summary>
        /// The text block used as a label.
        /// </summary>
        protected TextBlock TextBlock;

        /// <summary>
        /// The container that contains all the controls.
        /// </summary>
        protected StackPanel container;


        /// <summary>
        /// Gets or sets a value to be added to or subtracted from the <see cref="RangeBase.Value" /> of a <see cref="RangeBase" /> control.
        /// </summary>
        /// <returns>
        /// <see cref="RangeBase.Value" /> to add to or subtract from the <see cref="RangeBase.Value" /> of the <see cref="RangeBase" /> element. The default is 1.</returns>
        public override float LargeChange
        {
            get
            {
                return base.LargeChange;
            }

            set
            {
                this.slider.LargeChange = value;
                this.textField.LargeChange = value;
                base.LargeChange = value;
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="RangeBase.Value" /> to be added to or subtracted from the <see cref="RangeBase.Value" /> of a <see cref="RangeBase" /> control.
        /// </summary>
        /// <returns>
        /// <see cref="RangeBase.Value" /> to add to or subtract from the <see cref="RangeBase.Value" /> of the <see cref="RangeBase" /> element. The default is 0.1.
        /// </returns>
        public override float SmallChange
        {
            get
            {
                return base.SmallChange;
            }

            set
            {
                this.slider.SmallChange = value;
                this.textField.SmallChange = value;
                base.SmallChange = value;
            }
        }

        /// <summary>
        /// Gets or sets a minimum allowable value.
        /// </summary>
        public override float Minimum
        {
            get
            {
                return base.Minimum;
            }

            set
            {
                this.slider.Minimum = value;
                this.textField.Minimum = value;
                base.Minimum = value;
            }
        }

        /// <summary>
        /// Gets or sets a maximum allowable value.
        /// </summary>
        public override float Maximum
        {
            get
            {
                return base.Maximum;
            }

            set
            {
                this.slider.Maximum = value;
                this.textField.Maximum = value;
                base.Maximum = value;
            }
        }

        /// <summary>
        /// Gets or sets the text used as the label.
        /// </summary>
        public virtual string Text
        {
            get
            {
                return this.TextBlock.Text;
            }

            set
            {
                this.TextBlock.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the numeric value for the <see cref="RangeBase" />.
        /// </summary>
        public override float Value
        {
            get
            {
                return base.Value;
            }

            set
            {
                this.slider.Value = value;
                this.textField.Value = value;
                base.Value = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates the dimension by which child elements are stacked.
        /// </summary>
        public virtual Orientation Orientation
        {
            get
            {
                return this.container.Orientation;
            }

            set
            {
                var changed = this.container.Orientation != value;
                this.container.Orientation = value;
                if (changed)
                {
                    this.OnPropertyChanged("Orientation");
                }
            }
        }

        /// <summary>
        /// Gets or sets the decimal precision from 0 to 28.
        /// </summary>
        public override int Precision
        {
            get
            {
                return base.Precision;
            }

            set
            {
                this.textField.Precision = value;
                base.Precision = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SliderTextBox"/> class.
        /// </summary>
        public SliderTextBox()
        {
            this.container = new StackPanel() { Orientation = Orientation.Vertical };
            this.textField = new NumericTextField() { Text = "1", Width = 64, Minimum = 1, Maximum = 256 };
            this.slider = new Slider() { Orientation = Orientation.Horizontal , Value = 1, Minimum = 1, Maximum = 256, HorizontalAlignment = HorizontalAlignment.Stretch };
            this.slider.PropertyChanged += this.SliderPropertyChanged;
            this.textField.PropertyChanged += this.TextFieldPropertyChanged;
            this.TextBlock = new TextBlock();
            this.container.Controls.Add(this.TextBlock);
            this.container.Controls.Add(this.textField);
            this.container.Controls.Add(this.slider);
            this.Controls.Add(this.container);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SliderTextBox"/> class.
        /// </summary>
        /// <param name="text">The text for the label.</param>
        /// <param name="value">The value.</param>
        /// <param name="min">The minimum allowable value.</param>
        /// <param name="max">The maximum allowable value.</param>
        public SliderTextBox(string text, float value, float min, float max)
            : this()
        {
            this.Text = text;
            this.Minimum = min;
            this.Maximum = max;
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SliderTextBox"/> class.
        /// </summary>
        /// <param name="name">The name of the control.</param>
        /// <param name="text">The text for the label.</param>
        /// <param name="value">The value.</param>
        /// <param name="min">The minimum allowable value.</param>
        /// <param name="max">The maximum allowable value.</param>
        public SliderTextBox(string name,string text, float value, float min, float max)
            : this()
        {
            this.Name = name;
            this.Text = text;
            this.Minimum = min;
            this.Maximum = max;
            this.Value = value;
        }

        /// <summary>
        /// Handles property changes from <see cref="textField"/>.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void TextFieldPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Value":
                    this.Value = this.textField.Value;
                    break;
            }
        }

        /// <summary>
        /// Handles property changes from <see cref="slider"/>.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void SliderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Value":
                    this.Value = this.slider.Value;
                    break;
            }
        }
    }
}