namespace Codefarts.UIControls
{
    /// <summary>Provides data for mouse related routed events that do not specifically involve mouse buttons or the mouse wheel, for example <see cref="E:System.Windows.UIElement.MouseMove" />.</summary>
    public class MouseEventArgs : InputEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseEventArgs"/> class.
        /// </summary>
        /// <param name="leftButton">The left button.</param>
        /// <param name="rightButton">The right button.</param>
        public MouseEventArgs(bool leftButton, bool rightButton)
        {
            this.LeftButton = leftButton;
            this.RightButton = rightButton;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseEventArgs"/> class.
        /// </summary>
        /// <param name="leftButton">The left button.</param>
        /// <param name="middleButton">The middle button.</param>
        /// <param name="rightButton">The right button.</param>
        /// <param name="xButton1">The x button1.</param>
        /// <param name="xButton2">The x button2.</param>
        public MouseEventArgs(bool leftButton, bool middleButton, bool rightButton, bool xButton1, bool xButton2)
        {
            this.LeftButton = leftButton;
            this.MiddleButton = middleButton;
            this.RightButton = rightButton;
            this.XButton1 = xButton1;
            this.XButton2 = xButton2;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseEventArgs"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public MouseEventArgs(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseEventArgs"/> class.
        /// </summary>
        /// <param name="leftButton">The left button.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="rightButton">The right button.</param>
        public MouseEventArgs(bool leftButton, float x, float y, bool rightButton)
        {
            this.LeftButton = leftButton;
            this.X = x;
            this.Y = y;
            this.RightButton = rightButton;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseEventArgs"/> class.
        /// </summary>
        /// <param name="leftButton">if set to <c>true</c> [left button].</param>
        /// <param name="middleButton">if set to <c>true</c> [middle button].</param>
        /// <param name="rightButton">if set to <c>true</c> [right button].</param>
        /// <param name="xButton1">if set to <c>true</c> [x button1].</param>
        /// <param name="xButton2">if set to <c>true</c> [x button2].</param>
        /// <param name="y">The y.</param>
        /// <param name="x">The x.</param>
        public MouseEventArgs(bool leftButton, bool middleButton, bool rightButton, bool xButton1, bool xButton2, float y, float x)
        {
            this.LeftButton = leftButton;
            this.MiddleButton = middleButton;
            this.RightButton = rightButton;
            this.XButton1 = xButton1;
            this.XButton2 = xButton2;
            this.Y = y;
            this.X = x;
        }

        /// <summary>Gets the current state of the left mouse button.</summary>
        /// <returns>The current state of the left mouse button.</returns>
        public bool LeftButton { get; set; }

        /// <summary>Gets the current state of the middle mouse button.</summary>
        /// <returns>The current state of the middle mouse button. There is no default value.</returns>
        public bool MiddleButton { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseEventArgs"/> class.
        /// </summary>
        public MouseEventArgs()
        {
        }

        /// <summary>Gets the current state of the right mouse button.</summary>
        /// <returns>The current state of the right mouse button.</returns>
        public bool RightButton { get; set; }

        /// <summary>Gets the current state of the first extended mouse button.</summary>
        /// <returns>The current state of the first extended mouse button.</returns>
        public bool XButton1 { get; set; }

        /// <summary>Gets the state of the second extended mouse button.</summary>
        /// <returns>The current state of the second extended mouse button.</returns>
        public bool XButton2 { get; set; }

        /// <summary>Gets the state of the mouse X position.</summary>
        /// <returns>The current state of the mouse X position.</returns>
        public float X { get; set; }

        /// <summary>Gets the state of the mouse Y position.</summary>
        /// <returns>The current state of the mouse Y position.</returns>
        public float Y { get; set; }
    }
}