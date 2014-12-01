namespace Codefarts.UIControls
{
    using System;

    using Codefarts.UIControls.Code;
    using Codefarts.UIControls.Unity;

    using UnityEngine;

    public class Slider : CustomControl
    {
        private float minimum;
        private float maximum=10;
        private float value;

        public event EventHandler<RoutedPropertyChangedEventArgs<float>> ValueChanged;

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
                if (value > this.maximum)
                {
                    throw new ArgumentOutOfRangeException("Minimum value can not be greater then Maximum value.");
                }

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
                if (value < this.minimum)
                {
                    throw new ArgumentOutOfRangeException("Maximum value can not be less then Minimum value.");
                }

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
                if (value > this.maximum)
                {
                    throw new ArgumentOutOfRangeException("Value can not be greater then Maximum value.");
                }

                if (value < this.minimum)
                {
                    throw new ArgumentOutOfRangeException("Value can not be less then Minimum value.");
                }

                var changed = this.value != value;
                var oldValue = this.value;
                this.value = value;
                if (changed)
                {
                    var handler = this.ValueChanged;
                    if (handler != null)
                    {
                        handler(this, new RoutedPropertyChangedEventArgs<float>(oldValue, value) { Source = this });
                    }
                }
            }
        }

        public override void OnDraw(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            float value;
            switch (this.Orientation)
            {
                case Orientation.Horizontial:
                    value = GUILayout.HorizontalSlider(this.value, this.minimum, this.maximum, ControlDrawingHelpers.StandardDimentionOptions(this));
                    if (this.IsEnabled)
                    {
                        this.Value = value;
                    }

                    break;

                case Orientation.Vertical:
                    value = GUILayout.VerticalSlider(this.value, this.minimum, this.maximum, ControlDrawingHelpers.StandardDimentionOptions(this));
                    if (this.IsEnabled)
                    {
                        this.Value = value;
                    }

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