namespace Codefarts.UIControls.Code.Renderers
{
    using System;

    using Codefarts.UIControls.Interfaces;

    using UnityEngine;

    public class ButtonRenderer : IControlRenderer
    {
        public Type ControlType
        {
            get
            {
                return typeof(Button);
            }
        }

        public void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var button = control as Button;
            var content = new GUIContent(button.Text, button.Texture);
            if (control.Left == 0 && control.Top == 0)
            {
                if (GUILayout.Button(content, ControlDrawingHelpers.StandardDimentionOptions(control)))
                {
                    button.OnClick();
                }
            }
            else
            {
                if (GUI.Button(new Rect(control.Left, control.Top, control.Width, control.Height), content))
                {
                    button.OnClick();
                }
            }
        }

        public void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
        }
    }
}
