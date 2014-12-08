namespace Codefarts.UIControls.Code.Renderers
{
    using System;

    using UnityEngine;

    public class ButtonCheckBoxRenderer : CheckBoxRenderer
    {     
        public override Type ControlType
        {
            get
            {
                return typeof(ButtonCheckBox);
            }
        }

        public override void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var checkBox = control as CheckBox;
            if (checkBox == null)
            {
                throw new ArgumentException("control does not inherit from CheckBox.", "control");
            }
           
            if (this.style == null)
            {
                this.style = GUI.skin.button;
            }

            base.Draw(manager, control, elapsedGameTime, totalGameTime);
        }
    }
}