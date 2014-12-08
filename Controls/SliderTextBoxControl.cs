namespace Codefarts.GridMapGame.EditorTools
{
    using System;

    using Codefarts.UIControls;
    using Codefarts.UIControls.Interfaces;

    public class SliderTextBoxControl : Control, ICustomRendering
    {
        private NumericTextField textField;

        private Slider slider;

        private Label label;

        private StackPanel container;
        public event EventHandler<RoutedPropertyChangedEventArgs<float>> ValueChanged;

        public float Minimum
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

        public float Maximum
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

        public string Text
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

        public float Value
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

        public Orientation Orientation
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

        public SliderTextBoxControl()
            : base()
        {
            this.container = new StackPanel() { Orientation = Orientation.Vertical };
            this.textField = new NumericTextField() { Text = "1", Width = 64, Minimum = 1, Maximum = 256 };
            this.slider = new Slider() { Orientation = Orientation.Horizontial, Value = 1, Minimum = 1, Maximum = 256 };
            this.slider.ValueChanged += this.SliderValueChanged;
            this.textField.TextChanged += this.TextFieldTextChanged;
            this.label = new Label();
            this.container.Children.Add(this.label);
            this.container.Children.Add(this.textField);
            this.container.Children.Add(this.slider);
        }

        public SliderTextBoxControl(string text, float value, float min, float max)
            : this()
        {
            this.Text = text;
            this.Minimum = min;
            this.Maximum = max;
            this.Value = value;
        }

        void TextFieldTextChanged(object sender, RoutedPropertyChangedEventArgs<string> e)
        {
            this.slider.Value = Single.Parse(e.NewValue);
        }

        void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<float> e)
        {
            this.textField.Text = e.NewValue.ToString();
            var handler = this.ValueChanged;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        public void Draw(IControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            manager.DrawControl(this.container, elapsedGameTime, totalGameTime);
        }

        public void Update(IControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            manager.UpdateControl(this.container, elapsedGameTime, totalGameTime);
        }
    }
}