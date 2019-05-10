namespace Codefarts.UIControls
{
    using System;

    /// <summary>
    /// Provides arguments for rendering controls.
    /// </summary>
    public class ControlRenderingArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the control to be rendered.
        /// </summary>
        public Control Control { get; set; }

        /// <summary>
        /// Gets or sets the manager being used to renderer the control.
        /// </summary>
        public IControlRendererManager Manager { get; set; }

        /// <summary>
        /// Gets or sets the elapsed game time.
        /// </summary>
        public float ElapsedGameTime { get; set; }

        /// <summary>
        /// Gets or sets the total game time.
        /// </summary>
        public float TotalGameTime { get; set; }

        /// <summary>
        /// Gets or sets the offset to draw the control at.
        /// </summary>
        public Point Offset{ get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ControlRenderingArgs"/> has been handled.
        /// </summary>
        public bool Handled { get; set; }
    }
}