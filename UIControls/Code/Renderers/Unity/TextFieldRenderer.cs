namespace Codefarts.UIControls.Code.Renderers
{
    using System;

    using Codefarts.UIControls.Interfaces;
    using Codefarts.UIControls.Unity;

    using UnityEngine;

    public class TextFieldRenderer : IControlRenderer
    {
        public Type ControlType
        {
            get
            {
                return typeof(TextField);
            }
        }

        public void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var textField = control as TextField;
            var value = GUILayout.TextField(string.IsNullOrEmpty(textField.Text) ? string.Empty : textField.Text, ControlDrawingHelpers.StandardDimentionOptions(control));
            if (control.IsEnabled)
            {
                textField.Text = value;
            }
        }

        public void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
        }
    }
}