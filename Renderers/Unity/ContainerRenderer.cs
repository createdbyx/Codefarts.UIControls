namespace Codefarts.UIControls.Code.Renderers
{
    using System;

    using Codefarts.UIControls.Interfaces;

    using UnityEngine;

    public class ContainerRenderer : IControlRenderer
    {
        public Type ControlType
        {
            get
            {
                return typeof(ContainerControl);
            }
        }

        public void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var containerControl = control as ContainerControl;
            GUILayout.BeginVertical();
            foreach (var child in containerControl.Children)
            {
                manager.DrawControl(child, elapsedGameTime, totalGameTime);
            }

            GUILayout.EndVertical();
        }

        public void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
        }
    }
}