namespace Codefarts.UIControls
{
    /// <summary>
    /// Provides data for mouse related events</summary>
    public class MouseEventArgs : InputEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseEventArgs"/> class.
        /// </summary>
        /// <param name="leftButton">The left button.</param>
        /// <param name="rightButton">The right button.</param>
        /// <remarks>Button states are converted into a 1 for true and 0 for false.</remarks>
        public MouseEventArgs(bool leftButton, bool rightButton)
        {
            this.Buttons[Constants.LeftMouseButton] = leftButton ? 1 : 0;
            this.Buttons[Constants.RightMouseButton] = rightButton ? 1 : 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseEventArgs"/> class.
        /// </summary>
        /// <param name="leftButton">The left button.</param>
        /// <param name="middleButton">The middle button.</param>
        /// <param name="rightButton">The right button.</param>
        /// <remarks>Button states are converted into a 1 for true and 0 for false.</remarks>
        public MouseEventArgs(bool leftButton, bool middleButton, bool rightButton)
        {
            this.Buttons[Constants.LeftMouseButton] = leftButton ? 1 : 0;
            this.Buttons[Constants.RightMouseButton] = rightButton ? 1 : 0;
            this.Buttons[Constants.MiddleMouseButton] = middleButton ? 1 : 0;
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
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="leftButton">The left button.</param>
        /// <param name="rightButton">The right button.</param>
        /// <remarks>Button states are converted into a 1 for true and 0 for false.</remarks>
        public MouseEventArgs(float x, float y, bool leftButton, bool rightButton)
        {
            this.Buttons[Constants.LeftMouseButton] = leftButton ? 1 : 0;
            this.Buttons[Constants.RightMouseButton] = rightButton ? 1 : 0;
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseEventArgs"/> class.
        /// </summary>
        /// <param name="leftButton">if set to <c>true</c> [left button].</param>
        /// <param name="middleButton">if set to <c>true</c> [middle button].</param>
        /// <param name="rightButton">if set to <c>true</c> [right button].</param>
        /// <param name="y">The y.</param>
        /// <param name="x">The x.</param>
        /// <remarks>Button states are converted into a 1 for true and 0 for false.</remarks>
        public MouseEventArgs(bool leftButton, bool middleButton, bool rightButton, float y, float x)
        {
            this.Buttons[Constants.LeftMouseButton] = leftButton ? 1 : 0;
            this.Buttons[Constants.RightMouseButton] = rightButton ? 1 : 0;
            this.Buttons[Constants.MiddleMouseButton] = middleButton ? 1 : 0;
            this.Y = y;
            this.X = x;
        }

        /// <summary>Gets the current state of the left mouse button.</summary>
        /// <returns>The current state of the left mouse button.</returns>
        /// <remarks>The number of buttons is determined by <see cref="Constants.MaxMouseButtons"/> and may vary depending on the platform.</remarks>
        public float[] Buttons { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseEventArgs"/> class.
        /// </summary>
        public MouseEventArgs()
        {
            this.Buttons = new float[Constants.MaxMouseButtons];
        }

        /// <summary>
        /// Gets or sets a value indicating whether the left mouse button is pressed.
        /// </summary>
        public bool Left
        {
            get
            {
                return this.Buttons[Constants.LeftMouseButton] > float.Epsilon;
            }

            set
            {
                this.Buttons[Constants.LeftMouseButton] = value ? 1 : 0;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the right mouse button is pressed.
        /// </summary>
        public bool Right
        {
            get
            {
                return this.Buttons[Constants.RightMouseButton] > float.Epsilon;
            }

            set
            {
                this.Buttons[Constants.RightMouseButton] = value ? 1 : 0;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the middle mouse button is pressed.
        /// </summary>
        public bool Middle
        {
            get
            {
                return this.Buttons[Constants.MiddleMouseButton] > float.Epsilon;
            }

            set
            {
                this.Buttons[Constants.MiddleMouseButton] = value ? 1 : 0;
            }
        }

        /// <summary>Gets the state of the mouse X position.</summary>
        /// <returns>The current state of the mouse X position.</returns>
        public float X { get; set; }

        /// <summary>Gets the state of the mouse Y position.</summary>
        /// <returns>The current state of the mouse Y position.</returns>
        public float Y { get; set; }

        /// <summary>Gets or sets the event type.</summary>
        /// <returns>The current event type.</returns>
        public MouseEventType Type { get; set; }
    }
}