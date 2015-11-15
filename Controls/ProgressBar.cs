namespace Codefarts.UIControls
{
#if UNITY_5
    using UnityEngine;
#endif

    public class ProgressBar : RangeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/> class.
        /// </summary>
        public ProgressBar()
            : base()
        {
            this.Foreground = new SolidColorBrush(new Color(0, 1, 0));
            this.Maximum = 100;
        }
    }
}