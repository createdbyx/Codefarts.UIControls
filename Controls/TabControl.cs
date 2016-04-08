namespace Codefarts.UIControls
{
    /// <summary>
    /// Displays a tabbed control.
    /// </summary>
    public class TabControl : Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabControl" /> class.
        /// </summary>
        public TabControl()
        {
            this.canFocus = false;
            this.isTabStop = false;
        }

        /// <returns>
        /// The default <see cref="Size" /> of the control.
        /// </returns>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(121, 97);
            }
        }
    }
}