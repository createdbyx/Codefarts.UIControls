namespace Codefarts.UIControls.Code.Renderers
{
    using System;

    using Codefarts.UIControls.Interfaces;

#if UNITY3D
    using UnityEngine;
     

    public class ListBoxRenderer : IControlRenderer
    {
        public Type ControlType
        {
            get
            {
                return typeof(ListBox);
            }
        }

        public void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var listbox = control as ListBox;
            var scrollPosition = new Vector2(listbox.HorizontialOffset, listbox.VerticalOffset);
            if (listbox.HorizontialScrollBarVisibility == ScrollBarVisibility.Auto &
                listbox.VerticalScrollBarVisibility == ScrollBarVisibility.Auto)
            {
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, ControlDrawingHelpers.StandardDimentionOptions(control));
            }
            else
            {
                scrollPosition = GUILayout.BeginScrollView(
                    scrollPosition,
                    listbox.HorizontialScrollBarVisibility == ScrollBarVisibility.Visible,
                    listbox.VerticalScrollBarVisibility == ScrollBarVisibility.Visible,
                 ControlDrawingHelpers.StandardDimentionOptions(control));
            }

            var items = new object[listbox.Items.Count];
            listbox.Items.CopyTo(items, 0);
            listbox.SelectedIndex = ControlDrawingHelpers.SelectionList(listbox.SelectedIndex, items, callback => { });

            GUILayout.FlexibleSpace();
            GUILayout.EndScrollView();
            listbox.HorizontialOffset = scrollPosition.x;
            listbox.VerticalOffset = scrollPosition.y;
        }

        public void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
        }
    }
#endif
}