namespace Codefarts.UIControls.Code.Renderers
{
    using System;

    using Codefarts.UIControls.Interfaces;

    using UnityEngine;

    public class CheckBoxRenderer : IControlRenderer
    {
        private GUIContent content;

        protected GUIStyle style;

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonRenderer"/> class.
        /// </summary>
        public CheckBoxRenderer()
        {
            this.content = new GUIContent();
        }

        public virtual Type ControlType
        {
            get
            {
                return typeof(CheckBox);
            }
        }

        public virtual void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var checkBox = control as CheckBox;
            if (checkBox == null)
            {
                throw new ArgumentException("control does not inherit from CheckBox.", "control");
            }

            if (this.style == null)
            {
                this.style = GUI.skin.toggle;
            }

            this.content.text = string.IsNullOrEmpty(checkBox.Text) ? string.Empty : checkBox.Text;
            this.content.image = checkBox.Texture;

            if (control.Left == 0 && control.Top == 0)
            {
                checkBox.IsChecked = GUILayout.Toggle(checkBox.IsChecked, this.content,this.style, ControlDrawingHelpers.StandardDimentionOptions(control));
            }
            else
            {
                checkBox.IsChecked = GUI.Toggle(new Rect(control.Left, control.Top, control.Width, control.Height), checkBox.IsChecked, this.content,this.style);
            }
        }

        public virtual void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
        }
    }
}