namespace Codefarts.UIControls.Renderers.Unity
{
    using System;

    using Codefarts.UIControls.Interfaces;

    using UnityEngine;

    public class SliderRenderer : IControlRenderer
    {
        public Type ControlType
        {
            get
            {
                return typeof(Slider);
            }
        }

        public   void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var slider = control as Slider;
            if (slider == null)
            {
                throw new ArgumentException("Argument does not inherit from Slider.", "control");
            }
          
            float value;
            switch (slider.Orientation)
            {
                case Orientation.Horizontial:
                    value = GUILayout.HorizontalSlider(slider.Value, slider.Minimum, slider.Maximum, ControlDrawingHelpers.StandardDimentionOptions(slider));
                    if (slider.IsEnabled)
                    {
                        slider.Value = value;
                    }

                    break;

                case Orientation.Vertical:
                    value = GUILayout.VerticalSlider(slider.Value, slider.Minimum, slider.Maximum, ControlDrawingHelpers.StandardDimentionOptions(slider));
                    if (slider.IsEnabled)
                    {
                        slider.Value = value;
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {   
        }
    }
}