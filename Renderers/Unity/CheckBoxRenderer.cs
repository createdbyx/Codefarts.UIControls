namespace Codefarts.UIControls.Code.Renderers
{
    using System;

    using Codefarts.UIControls.Interfaces;

    using UnityEngine;

    public class CheckBoxRenderer : IControlRenderer
    {
        public Type ControlType
        {
            get
            {
                return typeof(CheckBox);
            }
        }

        public void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var checkBox = control as CheckBox;
            if (string.IsNullOrEmpty(checkBox.Text))
            {
                checkBox.IsChecked = GUILayout.Toggle(checkBox.IsChecked, String.Empty, ControlDrawingHelpers.StandardDimentionOptions(control));
            }
            else
            {
                checkBox.IsChecked = GUILayout.Toggle(checkBox.IsChecked, checkBox.Text, ControlDrawingHelpers.StandardDimentionOptions(control));
            }
        }

        public void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
        }
    }
}