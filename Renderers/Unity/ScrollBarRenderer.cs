namespace Codefarts.UIControls.Renderers.Unity
{
    using System;

    using Codefarts.UIControls.Interfaces;

    using UnityEngine;         

    public class ScrollBarRenderer : IControlRenderer
    {
        public Type ControlType
        {
            get
            {
                return typeof(ScrollBar);
            }
        }

        public  void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var scrollBar = control as ScrollBar;
            if (scrollBar == null)
            {
                throw new ArgumentException("Argument does not inherit from ScrollBar.", "control");
            }

            switch (scrollBar.Orientation)
            {
                case Orientation.Horizontial:
                    scrollBar.Value = GUILayout.HorizontalScrollbar(scrollBar.Value, 1, scrollBar.Minimum, scrollBar.Maximum, ControlDrawingHelpers.StandardDimentionOptions(scrollBar));
                    break;

                case Orientation.Vertical:
                    scrollBar.Value = GUILayout.VerticalScrollbar(scrollBar.Value, 1, scrollBar.Minimum, scrollBar.Maximum, ControlDrawingHelpers.StandardDimentionOptions(scrollBar));
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