namespace Codefarts.UIControls.Code.Renderers
{
    using System;

    using Codefarts.UIControls.Interfaces;

    using UnityEngine;

    public class LabelRenderer : IControlRenderer
    {
        public Type ControlType
        {
            get
            {
                return typeof(Label);
            }
        }

        public void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var label = control as Label;
            GUILayout.Label(label.Text);
        }

        public void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
        }
    }
}