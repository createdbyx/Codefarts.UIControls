namespace Codefarts.UIControls.Code.Renderers
{
    using System;

    using Codefarts.UIControls.Interfaces;

#if UNITY3D
    using UnityEngine;
    
    public class StackPanelRenderer : IControlRenderer
    {
        public Type ControlType
        {
            get
            {
                return typeof(StackPanel);
            }
        }

        public void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var panel = control as StackPanel;
            if (panel.Orientation == Orientation.Horizontial)
            {
                GUILayout.BeginHorizontal(ControlDrawingHelpers.StandardDimentionOptions(control));
                foreach (var child in panel.Children)
                {
                    manager.DrawControl(child, elapsedGameTime, totalGameTime);
                }
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.BeginVertical(ControlDrawingHelpers.StandardDimentionOptions(control));
                foreach (var child in panel.Children)
                {
                    manager.DrawControl(child, elapsedGameTime, totalGameTime);
                }
                GUILayout.EndVertical();
            }
        }

        public void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
        }
    }
#endif
}