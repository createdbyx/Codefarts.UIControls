namespace Codefarts.UIControls.Code.Renderers
{
    using System;

    using Codefarts.UIControls.Interfaces;

    using UnityEngine;

    public class ScrollViewerRenderer : IControlRenderer
    {
        public Type ControlType
        {
            get
            {
                return typeof(ScrollViewer);
            }
        }

        public void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var viewer = control as ScrollViewer;
            var scrollPosition = new Vector2(viewer.HorizontialOffset, viewer.VerticalOffset);
            if (viewer.HorizontialScrollBarVisibility == ScrollBarVisibility.Auto &
                viewer.VerticalScrollBarVisibility == ScrollBarVisibility.Auto)
            {
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, ControlDrawingHelpers.StandardDimentionOptions(control));
            }
            else
            {
                scrollPosition = GUILayout.BeginScrollView(
                    scrollPosition,
                    viewer.HorizontialScrollBarVisibility == ScrollBarVisibility.Visible,
                    viewer.VerticalScrollBarVisibility == ScrollBarVisibility.Visible,
                ControlDrawingHelpers.StandardDimentionOptions(control));
            }

            foreach (var child in viewer.Children)
            {
                manager.DrawControl(child, elapsedGameTime, totalGameTime);
            }

            // GUILayout.FlexibleSpace();
            GUILayout.EndScrollView();
            viewer.HorizontialOffset = scrollPosition.x;
            viewer.VerticalOffset = scrollPosition.y;
        }

        public void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
        }
    }
}