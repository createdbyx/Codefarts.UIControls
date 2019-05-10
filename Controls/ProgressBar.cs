namespace Codefarts.UIControls
{
    using System;

    /// <summary>
    /// Provides a progres bar control.
    /// </summary>
    public class ProgressBar : RangeBase
    {
        /// <summary>
        /// The backing field for the <see cref="Orientation"/> property.
        /// </summary>
        protected Orientation orientation = Orientation.Horizontal;

        /// <summary>
        /// The backing field for the <see cref="Text"/> property.
        /// </summary>
        protected string text = string.Empty;

        /// <summary>
        /// The backing field for the <see cref="Step"/> property.
        /// </summary>
        protected float step = 10f;

        /// <summary>
        /// Gets or sets the text displayed in the progress bar.
        /// </summary>
        /// <returns>
        /// A string containing the text displayed in the progress bar. The default is an empty string ("").
        /// </returns>
        public virtual string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                value = value == null ? string.Empty : value;
                var changed = this.text != value;
                this.text = value;
                if (changed)
                {
                    this.OnPropertyChanged("Text");
                }
            }
        }

        /// <summary>
        /// Gets or sets the amount by which a call to the <see cref="ProgressBar.PerformStep" /> method increases the current position
        /// of the progress bar.
        /// </summary>
        /// <returns>The amount by which to increment the progress bar with each call to the <see cref="ProgressBar.PerformStep" /> method. The default is 10.</returns>
        public virtual float Step
        {
            get
            {
                return this.step;
            }

            set
            {
                var changed = Math.Abs(this.step - value) > float.Epsilon;
                this.step = value;
                if (changed)
                {
                    this.OnPropertyChanged("Step");
                }
            }
        }

        /// <summary>
        /// Advances the current position of the progress bar by the specified amount.
        /// </summary>
        /// <param name="value">
        /// The amount by which to increment the progress bar's current position.
        /// </param>
        public virtual void Increment(float value)
        {
            this.Value += value;
        }

        /// <summary>
        /// Advances the current position of the progress bar by the amount of the <see cref="ProgressBar.Step" /> property.
        /// </summary>
        public virtual void PerformStep()
        {
            this.Increment(this.step);
        }

        /// <summary>
        /// Gets or sets a value that determines the orientation of the progress bar.
        /// </summary>
        public virtual Orientation Orientation
        {
            get
            {
                return this.orientation;
            }

            set
            {
                var changed = this.orientation != value;
                this.orientation = value;
                if (changed)
                {
                    this.OnPropertyChanged("Orientation");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/> class.
        /// </summary>
        public ProgressBar()
        {
            this.foreground = new SolidColorBrush(Colors.Green);
            this.maximum = 100;
            this.canFocus = false;
            this.isTabStop = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/> class.
        /// </summary>
        /// <param name="name">The name of the control.</param>
        public ProgressBar(string name) : this()
        {
            this.name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar" /> class.
        /// </summary>
        /// <param name="orientation">Sets the initial orientation.</param>
        public ProgressBar(Orientation orientation) : this()
        {
            this.orientation = orientation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/> class.
        /// </summary>
        /// <param name="name">The name of the control.</param>
        /// <param name="orientation">Sets the initial orientation.</param>
        public ProgressBar(string name, Orientation orientation) : this(name)
        {
            this.orientation = orientation;
        }
    }
}