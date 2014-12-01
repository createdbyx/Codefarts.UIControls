namespace Codefarts.UIControls.Code.Renderers
{
    using System;

    using Codefarts.UIControls.Interfaces;
    using Codefarts.UIControls.Unity;

    using UnityEngine;

    public class WindowRenderer : IControlRenderer
    {
        public Type ControlType
        {
            get
            {
                return typeof(Window);
            }
        }

        public void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var window = control as Window;
            var windowRect = new Rect(control.Left, control.Top, control.Width, control.Height);

            windowRect = GUILayout.Window(
                window.ID,
                windowRect,
                id =>
                {
                    foreach (var child in window.Children)
                    {
                        manager.DrawControl(child, elapsedGameTime, totalGameTime);
                    }

                    if (window.IsDragable)
                    {
                        GUI.DragWindow();
                    }
                },
                window.Title,
               ControlDrawingHelpers.StandardDimentionOptions(control));
            control.Left = windowRect.x;
            control.Top = windowRect.y;
            control.Width = windowRect.width;
            control.Height = windowRect.height;
        }

        public void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
        }
    }
}