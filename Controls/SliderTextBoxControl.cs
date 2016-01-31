namespace Codefarts.GridMapGame.EditorTools
{
    using System;

    using Codefarts.UIControls;
    using Codefarts.UIControls.Interfaces;

    public class SliderTextBoxControl : Control, ICustomRendering
    {
        protected NumericTextField textField;

        protected Slider slider;

        protected TextBlock TextBlock;

        protected StackPanel container;
        public event EventHandler<PropertyChangedEventArgs<float>> ValueChanged;

        public virtual float Minimum
        {
            get
            {
                return this.slider.Minimum;
            }

            set
            {
                this.slider.Minimum = value;
                this.textField.Minimum = value;
            }
        }

        public virtual float Maximum
        {
            get
            {
                return this.slider.Maximum;
            }

            set
            {
                this.slider.Maximum = value;
                this.textField.Maximum = value;
            }
        }

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

        public virtual float Value
        {
            get
            {
                return this.slider.Value;
            }

            set
            {
                this.slider.Value = value;
            }
        }

        public virtual Orientation Orientation
        {
            get
            {
                return this.container.Orientation;
            }

            set
            {
                this.container.Orientation = value;
            }
        }

        public virtual int Precision
        {
            get
            {
                return this.textField.Precision;
            }

            set
            {
                this.textField.Precision = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SliderTextBoxControl"/> class.
        /// </summary>
        public SliderTextBoxControl()
            : base()
        {
            this.container = new StackPanel() { Orientation = Orientation.Vertical };
            this.textField = new NumericTextField() { Text = "1", Width = 64, Minimum = 1, Maximum = 256 };
            this.slider = new Slider() { Orientation = Orientation.Horizontial, Value = 1, Minimum = 1, Maximum = 256, HorizontalAlignment = HorizontalAlignment.Stretch };
            this.slider.ValueChanged += this.SliderValueChanged;
            this.textField.TextChanged += this.TextFieldTextChanged;
            this.textField.ValueChanged += this.TextFieldValueChanged;
            this.TextBlock = new TextBlock();
            this.container.Controls.Add(this.TextBlock);
            this.container.Controls.Add(this.textField);
            this.container.Controls.Add(this.slider);
        }

        private void TextFieldValueChanged(object sender, PropertyChangedEventArgs<float> e)
        {
            this.slider.Value = e.NewValue;
        }

        public SliderTextBoxControl(string text, float value, float min, float max)
            : this()
        {
            this.Text = text;
            this.Minimum = min;
            this.Maximum = max;
            this.Value = value;
        }

        protected virtual void TextFieldTextChanged(object sender, PropertyChangedEventArgs<string> e)
        {
            var newValue = this.slider.Value;
            this.slider.Value = float.TryParse(e.NewValue, out newValue) ? newValue : this.slider.Value;
        }

        protected virtual void SliderValueChanged(object sender, PropertyChangedEventArgs<float> e)
        {
            this.textField.Value = e.NewValue;
            var handler = this.ValueChanged;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is enabled.
        /// </summary>
        public override bool IsEnabled
        {
            get
            {
                return base.IsEnabled;
            }

            set
            {
                base.IsEnabled = value;
                this.container.IsEnabled = value;
            }
        }

        /// <summary>
        /// Gets or sets the controls visibility.
        /// </summary>
        public override Visibility Visibility
        {
            get
            {
                return base.Visibility;
            }

            set
            {
                base.Visibility = value;
                this.container.Visibility = value;
            }
        }

        /// <summary>
        /// Draws the specified manager.
        /// </summary>
        /// <param name="args"></param>
        public virtual void Draw(ControlRenderingArgs args)
        {
            if (this.Visibility == Visibility.Visible)
            {
                args.Manager.DrawControl(this.container, args.ElapsedGameTime, args.TotalGameTime);
            }
        }

        /// <summary>
        /// Updates the specified manager.
        /// </summary>
        /// <param name="args"></param>
        public virtual void Update(ControlRenderingArgs args)
        {
            if (this.IsEnabled && this.Visibility == Visibility.Visible)
            {
                args.Manager.UpdateControl(this.container, args.ElapsedGameTime, args.TotalGameTime);
            }
        }
    }
}