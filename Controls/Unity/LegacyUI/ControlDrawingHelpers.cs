/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.UIControls
{
    using System;
    using System.Collections.Generic;

    using UnityEngine;

    using Vector2 = UnityEngine.Vector2;

    /// <summary>
    /// Provides methods for drawing controls.
    /// </summary>
    public static class ControlDrawingHelpers
    {
        /// <summary>
        /// Gets the screen rectangle for the control.
        /// </summary>
        /// <param name="control">A reference to the control to get the screen rectangle for.</param>
        /// <returns>Returns the screen rectangle for the control.</returns>
        public static Rect GetScreenRectangle(this Control control)
        {
            //var a = GUIUtility.GUIToScreenPoint(new Vector2(control.Left, control.Top));
            //var b = GUIUtility.GUIToScreenPoint(new Vector2(control.Left + control.Width, control.Top + control.Height));
            //return new Rect(a.x, a.y, b.x - a.x, b.y - a.y);

            var screenMin = GUIUtility.ScreenToGUIPoint(new Vector2(0, 0));
            var screenMax = GUIUtility.ScreenToGUIPoint(new Vector2(Screen.width, Screen.height));

            // top left corner in screen coordinates
            var tl = Vector2.zero - screenMin;

            // get bottom right corner in screen coordinates
            var br = screenMax - (tl + new Vector2(control.Width, control.Height));

            br = GUIUtility.GUIToScreenPoint(br - tl);
            tl = GUIUtility.GUIToScreenPoint(tl);
            return new Rect(tl.x, tl.y, br.x - tl.x, br.y - tl.y);
        }

        ///// <summary>
        ///// Draws a list of controls.
        ///// </summary>
        ///// <param name="items">A reference to a enumerable of controls to be drawn.</param>
        //public static void DrawControl(IEnumerable<Control> items)
        //{
        //    // if null reference just exit
        //    if (items == null)
        //    {
        //        return;
        //    }

        //    // draw each control
        //    foreach (var control in items)
        //    {
        //        DrawControl(control);
        //    }
        //}

        ///// <summary>
        ///// Draws a control.
        ///// </summary>
        ///// <param name="control">A reference to a control to draw.</param>
        ///// <remarks>Internally checks if reference is null or control is not visible and exits otherwise 
        ///// calls <see cref="UnityControlRenderingService.Render"/> passing in the control reference.</remarks>
        //public static void DrawControl(Control control)
        //{
        //    // if control is null or not visible just exit
        //    if (control == null || control.Visibility == Visibility.Collapsed)
        //    {
        //        return;
        //    }

        //    // get reference to renderer callback service and call render passing in the control
        //    var service = UnityControlRenderingService.Instance;
        //    service.Render(control);
        //}

        ///// <summary>
        ///// Provides a method for drawing a <see cref="Button"/> control.
        ///// </summary>
        ///// <param name="control">A reference to the control that should be drawn.</param>
        ///// <remarks>Does not check if the control reference is null.</remarks>
        //public static void RenderButton(Button control)
        //{
        //    var content = new GUIContent(control.Text, control.Texture);
        //    if (control.Left == 0 && control.Top == 0)
        //    {
        //        if (GUILayout.Button(content, StandardDimentionOptions(control)))
        //        {
        //            control.OnClick();
        //        }
        //    }
        //    else
        //    {
        //        if (GUI.Button(new Rect(control.Left, control.Top, control.Width, control.Height), content))
        //        {
        //            control.OnClick();
        //        }
        //    }
        //}

        ///// <summary>
        ///// Provides a method for drawing a <see cref="StackPanel"/> control.
        ///// </summary>
        ///// <param name="control">A reference to the control that should be drawn.</param>
        ///// <remarks>Does not check if the control reference is null.</remarks>
        //public static void RenderStackPanel(StackPanel control)
        //{
        //    if (control.Orientation == Orientation.Horizontial)
        //    {
        //        GUILayout.BeginHorizontal(StandardDimentionOptions(control));
        //        DrawControl(control.Children);
        //        GUILayout.EndHorizontal();
        //    }
        //    else
        //    {
        //        GUILayout.BeginVertical(StandardDimentionOptions(control));
        //        DrawControl(control.Children);
        //        GUILayout.EndVertical();
        //    }
        //}

        ///// <summary>
        ///// Provides a method for drawing a <see cref="ListBox"/> control.
        ///// </summary>
        ///// <param name="control">A reference to the control that should be drawn.</param>
        ///// <remarks>Does not check if the control reference is null.</remarks>
        //public static void RenderListBoxControl(ListBox control)
        //{

        //    var scrollPosition = new Vector2(control.HorizontialOffset, control.VerticalOffset);
        //    if (control.HorizontialScrollBarVisibility == ScrollBarVisibility.Auto &
        //        control.VerticalScrollBarVisibility == ScrollBarVisibility.Auto)
        //    {
        //        scrollPosition = GUILayout.BeginScrollView(scrollPosition, StandardDimentionOptions(control));
        //    }
        //    else
        //    {
        //        scrollPosition = GUILayout.BeginScrollView(
        //            scrollPosition,
        //            control.HorizontialScrollBarVisibility == ScrollBarVisibility.Visible,
        //            control.VerticalScrollBarVisibility == ScrollBarVisibility.Visible,
        //          StandardDimentionOptions(control));
        //    }

        //    var items = new object[control.Items.Count];
        //    control.Items.CopyTo(items, 0);
        //    control.SelectedIndex = SelectionList(control.SelectedIndex, items, callback => { });

        //    GUILayout.FlexibleSpace();
        //    GUILayout.EndScrollView();
        //    control.HorizontialOffset = scrollPosition.x;
        //    control.VerticalOffset = scrollPosition.y;
        //}

        ///// <summary>
        ///// Provides a method for drawing a <see cref="ScrollViewer"/> control.
        ///// </summary>
        ///// <param name="control">A reference to the control that should be drawn.</param>
        ///// <remarks>Does not check if the control reference is null.</remarks>
        //public static void RenderScrollViewer(ScrollViewer control)
        //{
        //    var scrollPosition = new Vector2(control.HorizontialOffset, control.VerticalOffset);
        //    if (control.HorizontialScrollBarVisibility == ScrollBarVisibility.Auto &
        //        control.VerticalScrollBarVisibility == ScrollBarVisibility.Auto)
        //    {
        //        scrollPosition = GUILayout.BeginScrollView(scrollPosition, StandardDimentionOptions(control));
        //    }
        //    else
        //    {
        //        scrollPosition = GUILayout.BeginScrollView(
        //            scrollPosition,
        //            control.HorizontialScrollBarVisibility == ScrollBarVisibility.Visible,
        //            control.VerticalScrollBarVisibility == ScrollBarVisibility.Visible,
        //          StandardDimentionOptions(control));
        //    }

        //    DrawControl(control.Children);

        //   // GUILayout.FlexibleSpace();
        //    GUILayout.EndScrollView();
        //    control.HorizontialOffset = scrollPosition.x;
        //    control.VerticalOffset = scrollPosition.y;
        //}

        ///// <summary>
        ///// Provides a method for drawing a <see cref="CustomControl"/> control.
        ///// </summary>
        ///// <param name="control">A reference to the control that should be drawn.</param>
        ///// <remarks>Does not check if the control reference is null.</remarks>
        //public static void RenderCustomControl(CustomControl control)
        //{
        //    if (control == null)
        //    {
        //        return;
        //    }

        //    control.OnDraw();
        //}

        ///// <summary>
        ///// Provides a method for drawing a <see cref="Label"/> control.
        ///// </summary>
        ///// <param name="control">A reference to the control that should be drawn.</param>
        ///// <remarks>Does not check if the control reference is null.</remarks>
        //public static void RenderLabel(Label control)
        //{
        //    GUILayout.Label(control.Text);
        //}

        ///// <summary>
        ///// Provides a method for drawing a <see cref="ScrollBar"/> control.
        ///// </summary>
        ///// <param name="control">A reference to the control that should be drawn.</param>
        ///// <remarks>Does not check if the control reference is null.</remarks>
        //public static void RenderScrollBar(ScrollBar control)
        //{
        //    switch (control.Orientation)
        //    {
        //        case Orientation.Horizontial:
        //            control.Value = GUILayout.HorizontalScrollbar(
        //                control.Value,
        //                1,
        //                control.Minimum,
        //                control.Maximum,
        //                StandardDimentionOptions(control));
        //            break;

        //        case Orientation.Vertical:
        //            control.Value = GUILayout.VerticalScrollbar(
        //               control.Value,
        //               1,
        //               control.Minimum,
        //               control.Maximum,
        //               StandardDimentionOptions(control));
        //            break;

        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }
        //}

        ///// <summary>
        ///// Provides a method for drawing a <see cref="Label"/> control.
        ///// </summary>
        ///// <param name="control">A reference to the control that should be drawn.</param>
        ///// <remarks>Does not check if the control reference is null.</remarks>
        //public static void RenderContainerControl(ContainerControl control)
        //{
        //    var service = UnityControlRenderingService.Instance;
        //    GUILayout.BeginVertical();
        //    foreach (var child in control.Children)
        //    {
        //        service.Render(child);
        //    }

        //    GUILayout.EndVertical();
        //}

        ///// <summary>
        ///// Provides a method for drawing a <see cref="CheckBox"/> control.
        ///// </summary>
        ///// <param name="control">A reference to the control that should be drawn.</param>
        ///// <remarks>Does not check if the control reference is null.</remarks>
        //public static void RenderCheckBox(CheckBox control)
        //{
        //    if (String.IsNullOrEmpty(control.Text))
        //    {
        //        control.IsChecked = GUILayout.Toggle(control.IsChecked, String.Empty, StandardDimentionOptions(control));
        //    }
        //    else
        //    {
        //        control.IsChecked = GUILayout.Toggle(control.IsChecked, control.Text, StandardDimentionOptions(control));
        //    }
        //}

        ///// <summary>
        ///// Provides a method for drawing a <see cref="TextBox"/> control.
        ///// </summary>
        ///// <param name="control">A reference to the control that should be drawn.</param>
        ///// <remarks>Does not check if the control reference is null.</remarks>
        //public static void RenderTextBox(TextBox control)
        //{
        //    var scrollPosition = new Vector2(control.HorizontialOffset, control.VerticalOffset);
        //    if (control.HorizontialScrollBarVisibility != ScrollBarVisibility.Hidden | control.VerticalScrollBarVisibility != ScrollBarVisibility.Hidden)
        //    {
        //        scrollPosition = GUILayout.BeginScrollView(
        //            scrollPosition,
        //            control.HorizontialScrollBarVisibility == ScrollBarVisibility.Visible,
        //            control.VerticalScrollBarVisibility == ScrollBarVisibility.Visible);
        //    }

        //    var value = GUILayout.TextArea(control.Text ?? String.Empty, StandardDimentionOptions(control));
            
        //    if(control.IsEnabled)
        //    {
        //        control.Text = value;
        //    }

        //    if (control.HorizontialScrollBarVisibility != ScrollBarVisibility.Hidden | control.VerticalScrollBarVisibility != ScrollBarVisibility.Hidden)
        //    {
        //        GUILayout.EndScrollView();
        //        control.HorizontialOffset = scrollPosition.x;
        //        control.VerticalOffset = scrollPosition.y;
        //    }
        //}

        ///// <summary>
        ///// Provides a method for drawing a <see cref="TextField"/> control.
        ///// </summary>
        ///// <param name="control">A reference to the control that should be drawn.</param>
        ///// <remarks>Does not check if the control reference is null.</remarks>
        //public static void RenderTextField(TextField control)
        //{
        //    var value = GUILayout.TextField(control.Text ?? String.Empty, StandardDimentionOptions(control));
        //    if (control.IsEnabled)
        //    {
        //        control.Text = value;
        //    }
        //}

        ///// <summary>
        ///// Provides a method for drawing a <see cref="Image"/> control.
        ///// </summary>
        ///// <param name="control">A reference to the control that should be drawn.</param>
        ///// <remarks>Does not check if the control reference is null.</remarks>
        //public static void RenderImage(Image control)
        //{
        //    if (control.Source != null)
        //    {
        //        ScaleMode scale;
        //        switch (control.Stretch)
        //        {
        //            case Stretch.Fill:
        //                scale = ScaleMode.StretchToFill;
        //                break;
        //            case Stretch.None:
        //                scale = ScaleMode.ScaleAndCrop;
        //                break;
        //            case Stretch.Uniform:
        //                scale = ScaleMode.ScaleToFit;
        //                break;
        //            case Stretch.UniformToFill:
        //                scale = ScaleMode.StretchToFill;
        //                break;
        //            default:
        //                throw new ArgumentOutOfRangeException();
        //        }

        //        // figure out dimensions
        //        var width = control.Width == 0 ? control.MinWidth : control.Width;
        //        var height = control.Height == 0 ? control.MinHeight : control.Height;

        //        switch (scale)
        //        {
        //            case ScaleMode.StretchToFill:
        //                break;
        //            case ScaleMode.ScaleAndCrop:
        //                break;
        //            case ScaleMode.ScaleToFit:
        //                width = width == 0 ? control.Source.width : width;
        //                height = height == 0 ? control.Source.height : height;
        //                break;
        //            default:
        //                throw new ArgumentOutOfRangeException();
        //        }

        //        var rect = new Rect(control.Left, control.Top, width, height);
        //        GUI.DrawTexture(rect, control.Source, scale, true);
        //    }
        //}

        /// <summary>
        /// Returns an array of <see cref="GUILayoutOption"/> types based on the state of the control.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static GUILayoutOption[] StandardDimentionOptions(Control control)
        {
            // check if null reference
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            var items = new List<GUILayoutOption>();
            if (control.MinWidth != 0)
            {
                items.Add(GUILayout.MinWidth(control.MinWidth));
            }

            if (control.MinHeight != 0)
            {
                items.Add(GUILayout.MinHeight(control.MinHeight));
            }

            if (control.MaxWidth != Single.MaxValue)
            {
                items.Add(GUILayout.MaxWidth(control.MaxWidth));
            }

            if (control.MaxHeight != Single.MaxValue)
            {
                items.Add(GUILayout.MaxHeight(control.MaxHeight));
            }

            if (control.Width != 0)
            {
                items.Add(GUILayout.Width(control.Width));
            }

            if (control.Height != 0)
            {
                items.Add(GUILayout.Height(control.Height));
            }

            if (control.MinWidth == 0f && control.MaxWidth == Single.MaxValue)
            {
                items.Add(GUILayout.ExpandWidth(true));
            }

            if (control.MinHeight == 0f && control.MaxHeight == Single.MaxValue)
            {
                items.Add(GUILayout.ExpandHeight(true));
            }

            return items.ToArray();
        }

        public static int SelectionList(int selected, object[] list, Action<int> callback)
        {
            return SelectionList(selected, list, "List Item", callback);
        }

        public static int SelectionList(int selected, object[] list, GUIStyle elementStyle, Action<int> callback)
        {
            for (int i = 0; i < list.Length; ++i)
            {
                var tmp = list[i];
                if (tmp == null)
                {
                    continue;
                }

                var guiContent = tmp is GUIContent ? (GUIContent)tmp : new GUIContent(tmp.ToString());
                var elementRect = GUILayoutUtility.GetRect(guiContent, elementStyle);

                var hover = elementRect.Contains(Event.current.mousePosition);
                if (hover && Event.current.type == EventType.MouseDown && Event.current.clickCount == 1)
                {
                    selected = i;
                    Event.current.Use();
                }
                // check if changed from MouseUp to MouseDown
                else if (hover && callback != null && Event.current.type == EventType.MouseDown && Event.current.clickCount == 2)
                {
                    Debug.Log("Works !");
                    callback(i);
                    Event.current.Use();
                }
                else if (Event.current.type == EventType.repaint)
                {
                    elementStyle.Draw(elementRect, guiContent, hover, false, i == selected, false);
                }
            }

            return selected;
        }
        //#region List box

        /// <summary>
        /// Provides a delegate
        /// </summary>
        /// <param name="index"></param>
        // public delegate void DoubleClickCallback(int index);

        //public static int SelectionList(int selected, GUIContent[] list)
        //{
        //    return SelectionList(selected, list, "List Item", null);
        //}

        //public static int SelectionList(int selected, GUIContent[] list, GUIStyle elementStyle)
        //{
        //    return SelectionList(selected, list, elementStyle, null);
        //}

        //public static int SelectionList(int selected, GUIContent[] list, DoubleClickCallback callback)
        //{
        //    return SelectionList(selected, list, "List Item", callback);
        //}

        //public static int SelectionList(int selected, GUIContent[] list, GUIStyle elementStyle, DoubleClickCallback callback)
        //{
        //    for (int i = 0; i < list.Length; ++i)
        //    {
        //        Rect elementRect = GUILayoutUtility.GetRect(list[i], elementStyle);
        //        bool hover = elementRect.Contains(Event.current.mousePosition);
        //        if (hover && Event.current.type == EventType.MouseDown && Event.current.clickCount == 1) // added " && Event.current.clickCount == 1"
        //        {
        //            selected = i;
        //            Event.current.Use();
        //        }
        //        else if (hover && callback != null && Event.current.type == EventType.MouseDown && Event.current.clickCount == 2) //Changed from MouseUp to MouseDown
        //        {
        //            Debug.Log("Works !");
        //            callback(i);
        //            Event.current.Use();
        //        } 


        //        else if (Event.current.type == EventType.repaint)
        //        {
        //            elementStyle.Draw(elementRect, list[i], hover, false, i == selected, false);
        //        }
        //    }
        //    return selected;
        //}

        //public static int SelectionList(int selected, string[] list)
        //{
        //    return SelectionList(selected, list, "List Item", null);
        //}

        //public static int SelectionList(int selected, string[] list, GUIStyle elementStyle)
        //{
        //    return SelectionList(selected, list, elementStyle, null);
        //}

        //public static int SelectionList(int selected, string[] list, DoubleClickCallback callback)
        //{
        //    return SelectionList(selected, list, "List Item", callback);
        //}

        //public static int SelectionList(int selected, string[] list, GUIStyle elementStyle, DoubleClickCallback callback)
        //{
        //    for (int i = 0; i < list.Length; ++i)
        //    {
        //        Rect elementRect = GUILayoutUtility.GetRect(new GUIContent(list[i]), elementStyle);
        //        bool hover = elementRect.Contains(Event.current.mousePosition);
        //        if (hover && Event.current.type == EventType.MouseDown && Event.current.clickCount == 1) // added " && Event.current.clickCount == 1"
        //        {
        //            selected = i;
        //            Event.current.Use();
        //        }
        //        else if (hover && callback != null && Event.current.type == EventType.MouseDown && Event.current.clickCount == 2) //Changed from MouseUp to MouseDown
        //        {
        //            Debug.Log("Works !");
        //            callback(i);
        //            Event.current.Use();
        //        }
        //        else if (Event.current.type == EventType.repaint)
        //        {
        //            elementStyle.Draw(elementRect, list[i], hover, false, i == selected, false);
        //        }
        //    }
        //    return selected;
        //}
        //#endregion

        ///// <summary>
        ///// Provides a method for drawing a <see cref="Window"/> control.
        ///// </summary>
        ///// <param name="control">A reference to the control that should be drawn.</param>
        ///// <remarks>Does not check if the control reference is null.</remarks>
        //public static void RenderWindow(Window control)
        //{
        //    var windowRect = new Rect(control.Left, control.Top, control.Width, control.Height);

        //    windowRect = GUILayout.Window(
        //        control.ID,
        //        windowRect,
        //        id =>
        //        {
        //            foreach (var child in control.Children)
        //            {
        //                DrawControl(child);
        //            }

        //            if (control.IsDragable)
        //            {
        //                GUI.DragWindow();
        //            }
        //        },
        //        control.Title,
        //        StandardDimentionOptions(control));
        //    control.Left = windowRect.x;
        //    control.Top = windowRect.y;
        //    control.Width = windowRect.width;
        //    control.Height = windowRect.height;
        //}
    }
}
