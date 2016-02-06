namespace Codefarts.UIControls
{
    /// <summary>
    /// Provides a progres bar control.
    /// </summary>
    public class ProgressBar : RangeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/> class.
        /// </summary>
        public ProgressBar()
        {
            this.Foreground = new SolidColorBrush(new Color(0, 1, 0));
            this.Maximum = 100;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/> class.
        /// </summary>
        /// <param name="name">The name of the control.</param>
        public ProgressBar(string name) : this()
        {
            this.name = name;
        }
    }
}