namespace Codefarts.UIControls
{
    using System;

    /// <summary>
    /// Provides a custom rendering control that uses callbacks to draw and update.
    /// </summary>
    public class CallbacksControl : CustomControl
    {
        /// <summary>
        /// The draw callback.
        /// </summary>
        public Action<ControlRenderingArgs> Draw;

        /// <summary>
        /// The update callback.
        /// </summary>
        public Action<ControlRenderingArgs> Update;


        /// <summary>
        /// Call this method when the control needs to draw it self.
        /// </summary>
        /// <param name="args">The rendering arguments.</param>
        public override void OnDraw(ControlRenderingArgs args)
        {
            var action = this.Draw;
            if (action != null)
            {
                action(args);
            }
        }

        /// <summary>
        /// Call this method when the contrl needs to draw it self.
        /// </summary>
        /// <param name="args">The update arguments.</param>
        public override void OnUpdate(ControlRenderingArgs args)
        {
            var action = this.Update;
            if (action != null)
            {
                action(args);
            }
        }
    }
}