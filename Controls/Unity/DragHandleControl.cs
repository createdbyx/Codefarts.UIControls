/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/
namespace CBX.Unity.Editor.Windows
{
    using System;

    using Codefarts.UIControls;
    using Codefarts.UIControls.Code;
    using Codefarts.UIControls.Unity;
    using UnityEngine;

    public class DragHandleControl : CustomControl
    {
        private DragableControlHandler handler = new DragableControlHandler();

        public event EventHandler Dragged;

        public DragHandleControl()
        {
            this.Width = 10;
            this.Height = 10;
        }

        public override void OnDraw(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            var rect = this.GetScreenRectangle();
            GUI.Box(new Rect(rect.x + this.Left, rect.y + this.Top, this.Width, this.Height), String.Empty);
            if (this.handler.HandleEvents(this))
            {
                if (this.Dragged != null)
                {
                    this.Dragged(this, EventArgs.Empty);
                }
            }
        }

        public override void OnUpdate(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            throw new NotImplementedException();
        }
    }
}