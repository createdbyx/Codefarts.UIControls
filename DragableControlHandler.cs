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

    /// <summary>
    /// Provides a helper method for enabling draggin controls.
    /// </summary>
    public class DragableControlHandler
    {
        public enum DragType
        {
            StartDragging,
            Dragging,
            StoppedDragging
        }

        public class DragEventArgs : MouseEventArgs
        {
            public DragType Type { get; set; }
        }

        /// <summary>
        /// Flag used to determin if the mouse is currently dragging.
        /// </summary>
        private bool isDragging;

        /// <summary>
        /// Stores the last mouse position.
        /// </summary>
        private Point lastMousePos;

        /// <summary>
        /// The control that is to be dragged.
        /// </summary>
        private Control control;

        /// <summary>
        /// Provides a event to notify when the control has been dragged.
        /// </summary>
        public event EventHandler<DragEventArgs> Dragged;

        /// <summary>
        /// Gets or sets the button index used when dragging.
        /// </summary>
        public int ButtonIndex { get; set; }

        /// <summary>
        /// Gets or sets weather or not the control can be dragged along the x axis.
        /// </summary>
        public bool AllowHorizontial { get; set; }

        /// <summary>
        /// Gets or sets weather or not the control can be dragged along the y axis.
        /// </summary>
        public bool AllowVertical { get; set; }

        /// <summary>
        /// Gets the current state of drag.
        /// </summary>
        /// <returns>true if the control is activly being dragged; otherwise false.</returns>
        public bool IsDragging
        {
            get
            {
                return this.isDragging;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DragableControlHandler"/> class.
        /// </summary>
        /// <param name="control">The control that will be dragged.</param>
        public DragableControlHandler(Control control)
        {
            this.control = control;
            this.ButtonIndex = Constants.LeftMouseButton;
            this.AllowHorizontial = true;
            this.AllowVertical = true;
            this.control.MouseEvent += this.MouseEventHandler;
        }

        private void MouseEventHandler(object sender, MouseEventArgs e)
        {
            switch (e.Type)
            {
                case MouseEventType.MouseEnter:
                    break;

                case MouseEventType.MouseLeave:
                    break;

                case MouseEventType.MouseMove:
                    if (this.isDragging && e.Left)
                    {
                        this.lastMousePos.X = !this.AllowHorizontial ? e.X : this.lastMousePos.X;
                        this.lastMousePos.Y = !this.AllowVertical ? e.Y : this.lastMousePos.Y;
                        var offset = e.Position - this.lastMousePos;
                        if (offset != Point.Empty)
                        {
                            this.control.Location += offset;
                            this.lastMousePos = e.Position;
                            this.OnDragged(e, DragType.Dragging);
                        }
                    }

                    break;

                case MouseEventType.MouseDown:
                    if (!this.isDragging && e.Left)
                    {
                        this.isDragging = true;
                        this.OnDragged(e, DragType.StartDragging);
                    }

                    break;

                case MouseEventType.MouseUp:
                    if (this.isDragging && e.Left)
                    {
                        this.isDragging = false;
                        this.OnDragged(e, DragType.StoppedDragging);
                    }

                    break;
            }
        }

        private void OnDragged(MouseEventArgs e, DragType type)
        {
            var handler = this.Dragged;
            if (handler != null)
            {
                handler(this, new DragEventArgs()
                {
                    Type = type,
                    Buttons = e.Buttons,
                    Handled = e.Handled,
                    Position = e.Position,
                });
            }
        }
    }
}
