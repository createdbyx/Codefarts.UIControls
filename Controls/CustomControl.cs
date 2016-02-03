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
    /// <summary>
    /// Provides an abstract control that does it's own rendering & updating.
    /// </summary>
    public abstract class CustomControl : Control
    {
        /// <summary>
        /// Call this method when the control needs to draw it self.
        /// </summary>
        /// <param name="args">The rendering arguments.</param>
        public abstract void OnDraw(ControlRenderingArgs args);

        /// <summary>
        /// Call this method when the contrl needs to draw it self.
        /// </summary>
        /// <param name="args">The update arguments.</param>
        public abstract void OnUpdate(ControlRenderingArgs args);
    }
}