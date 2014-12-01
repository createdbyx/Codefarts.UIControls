namespace Codefarts.UIControls.Code.Renderers
{
    using System;

    using Codefarts.UIControls.Interfaces;
    using Codefarts.UIControls.Unity;

    using UnityEngine;

    public class ImageRenderer : IControlRenderer
    {
        public Type ControlType
        {
            get
            {
                return typeof(Image);
            }
        }

        public void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var image = control as Image;
            if (image.Source != null)
            {
                ScaleMode scale;
                switch (image.Stretch)
                {
                    case Stretch.Fill:
                        scale = ScaleMode.StretchToFill;
                        break;
                    case Stretch.None:
                        scale = ScaleMode.ScaleAndCrop;
                        break;
                    case Stretch.Uniform:
                        scale = ScaleMode.ScaleToFit;
                        break;
                    case Stretch.UniformToFill:
                        scale = ScaleMode.StretchToFill;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                // figure out dimensions
                var width = control.Width == 0 ? control.MinWidth : control.Width;
                var height = control.Height == 0 ? control.MinHeight : control.Height;

                switch (scale)
                {
                    case ScaleMode.StretchToFill:
                        break;
                    case ScaleMode.ScaleAndCrop:
                        break;
                    case ScaleMode.ScaleToFit:
                        width = width == 0 ? image.Source.width : width;
                        height = height == 0 ? image.Source.height : height;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var rect = new Rect(control.Left, control.Top, width, height);
                GUI.DrawTexture(rect, image.Source, scale, true);
            }
        }

        public void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
        }
    }
}