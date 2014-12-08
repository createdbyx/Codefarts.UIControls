namespace Codefarts.UIControls.Code.Renderers
{
    using System;

    using Codefarts.UIControls.Interfaces;

    using UnityEngine;

    public class ButtonRenderer : IControlRenderer
    {
        private GUIContent content;

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonRenderer"/> class.
        /// </summary>
        public ButtonRenderer()
        {
            this.content = new GUIContent();
        }

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
            this.content.text = button.Text;
            this.content.image = button.Texture;
            if (control.Left == 0 && control.Top == 0)
            {
                if (GUILayout.Button(this.content, ControlDrawingHelpers.StandardDimentionOptions(control)))
                {
                    button.OnClick();
                }
            }
            else
            {
                if (GUI.Button(new Rect(control.Left, control.Top, control.Width, control.Height), this.content))
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
