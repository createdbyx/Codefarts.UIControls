namespace Codefarts.UIControls.Code.Renderers
{
    using System;

    using Codefarts.UIControls.Interfaces;

    using UnityEngine;

    public class TextBoxRenderer : IControlRenderer
    {
        public Type ControlType
        {
            get
            {
                return typeof(TextBox);
            }
        }

        public void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var textBox = control as TextBox;
            var scrollPosition = new Vector2(textBox.HorizontialOffset, textBox.VerticalOffset);
            if (textBox.HorizontialScrollBarVisibility != ScrollBarVisibility.Hidden | textBox.VerticalScrollBarVisibility != ScrollBarVisibility.Hidden)
            {
                scrollPosition = GUILayout.BeginScrollView(
                    scrollPosition,
                    textBox.HorizontialScrollBarVisibility == ScrollBarVisibility.Visible,
                    textBox.VerticalScrollBarVisibility == ScrollBarVisibility.Visible);
            }

            var value = GUILayout.TextArea(textBox.Text ?? string.Empty, ControlDrawingHelpers.StandardDimentionOptions(control));

            if (control.IsEnabled)
            {
                textBox.Text = value;
            }

            if (textBox.HorizontialScrollBarVisibility != ScrollBarVisibility.Hidden | textBox.VerticalScrollBarVisibility != ScrollBarVisibility.Hidden)
            {
                GUILayout.EndScrollView();
                textBox.HorizontialOffset = scrollPosition.x;
                textBox.VerticalOffset = scrollPosition.y;
            }
        }

        public void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
        }
    }
}