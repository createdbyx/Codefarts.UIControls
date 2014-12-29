namespace Codefarts.GridMapGame.EditorTools
{
    using System;                 

    using Codefarts.UIControls;
    using Codefarts.UIControls.Interfaces;

    public class SliderTextBoxControl : Control, ICustomRendering
    {
        protected NumericTextField textField;

        protected Slider slider;

        protected Label label;

        protected StackPanel container;
        public event EventHandler<RoutedPropertyChangedEventArgs<float>> ValueChanged;

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
                return this.label.Text;
            }

            set
            {
                this.label.Text = value;
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
            this.slider = new Slider() { Orientation = Orientation.Horizontial, Value = 1, Minimum = 1, Maximum = 256 };
            this.slider.ValueChanged += this.SliderValueChanged;
            this.textField.TextChanged += this.TextFieldTextChanged;
            this.textField.ValueChanged += this.TextFieldValueChanged;
            this.label = new Label();
            this.container.Children.Add(this.label);
            this.container.Children.Add(this.textField);
            this.container.Children.Add(this.slider);
        }

        private void TextFieldValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
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

        protected virtual void TextFieldTextChanged(object sender, RoutedPropertyChangedEventArgs<string> e)
        {
            var newValue = this.slider.Value;
            this.slider.Value = float.TryParse(e.NewValue, out newValue) ? newValue : this.slider.Value;
        }

        protected virtual void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
        {
            this.textField.Value = e.NewValue;
            var handler = this.ValueChanged;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        public virtual void Draw(IControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            manager.DrawControl(this.container, elapsedGameTime, totalGameTime);
        }

        public virtual void Update(IControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            manager.UpdateControl(this.container, elapsedGameTime, totalGameTime);
        }
    }
}