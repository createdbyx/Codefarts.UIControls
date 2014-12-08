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
                try
                {
                    GUILayout.BeginHorizontal(ControlDrawingHelpers.StandardDimentionOptions(control));
                     //for (int i = panel.Children.Count - 1; i >= 0; i--)
                     for (int i = 0; i < panel.Children.Count; i++)
                    {
                        var child = panel.Children[i];
                        manager.DrawControl(child, elapsedGameTime, totalGameTime);
                    }
                }
                finally
                {
                    GUILayout.EndHorizontal();
                }
            }
            else
            {
                try
                {
                    GUILayout.BeginVertical(ControlDrawingHelpers.StandardDimentionOptions(control));
                    foreach (var child in panel.Children)
                    {
                        manager.DrawControl(child, elapsedGameTime, totalGameTime);
                    }
                }
                finally
                {
                    GUILayout.EndVertical();
                }
            }
        }

        public void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
        }
    }
#endif
}