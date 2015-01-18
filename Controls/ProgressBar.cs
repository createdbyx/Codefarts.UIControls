namespace Codefarts.UIControls
{
    using Codefarts.Imaging;

    public class ProgressBar : RangeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/> class.
        /// </summary>
        public ProgressBar()
            : base()
        {
            this.Foreground = new SolidColorBrush(Color.Green);
            this.Maximum = 100;
        }
    }
}