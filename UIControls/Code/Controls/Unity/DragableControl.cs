/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/
namespace Codefarts.UIControls.Unity
{                          

    using UnityEngine;
                                         
    public class DragableControlHandler
    {
        private bool isDragging;

        private Vector2 lastMousePos;

        public bool IsDragging
        {
            get
            {
                return this.isDragging;
            }
        }

        public bool HandleEvents(Control control)
        {
            var current = Event.current;

            if (this.isDragging && current.type == EventType.MouseUp && current.button == 0)
            {
                this.isDragging = false;
            }

            if (this.isDragging && current.type == EventType.MouseDrag && current.button == 0)
            {
                control.Left += current.delta.x;
                control.Top += current.delta.y;
            }

            if (!this.isDragging && current.type == EventType.MouseDown && current.button == 0)
            {
                var box = new Bounds();
                box.min = GUIUtility.GUIToScreenPoint(new Vector2(control.Left, control.Top));
                box.max = GUIUtility.GUIToScreenPoint(new Vector2(control.Left + control.Width, control.Top + control.Height));
                var mPos = GUIUtility.GUIToScreenPoint(current.mousePosition);

                this.isDragging = box.Contains(mPos);
            }

            return this.isDragging;
        }
    }
}
