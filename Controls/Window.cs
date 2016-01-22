namespace Codefarts.UIControls
{
    using System;

    /// <summary>
    /// A single window control.
    /// </summary>
    public class Window : ScrollViewer
    {
        /// <summary>
        /// The title for the window.
        /// </summary>
        protected string title = string.Empty;

        /// <summary>
        /// The owner value for the <see cref="Owner"/> property.
        /// </summary>
        protected Window owner;

        /// <summary>Occurs whenever the window is first displayed.</summary>
        public event EventHandler Shown;

        /// <summary>Occurs when the window is closed. </summary>
        public event EventHandler Closed;

        /// <summary>Gets or sets the window that owns this window.</summary>
        /// <returns>A <see cref="Window.Owner" /> that represents the window that is the owner of this window.</returns>
        public virtual Window Owner
        {
            get
            {
                return this.owner;
            }

            set
            {
                var changed = this.owner != value;
                this.owner = value;
                if (changed)
                {
                    this.OnPropertyChanged("Owner");
                }
            }
        }

        /// <summary>
        /// Gets or sets the window title.
        /// </summary>
        public virtual string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                var changed = this.title != value;
                this.title = value;
                if (changed)
                {
                    this.OnPropertyChanged("Title");
                }
            }
        }

        /// <summary>Closes the window.</summary>
        public virtual void Close()
        {
            this.Visibility = Visibility.Hidden;
            this.OnClosed();
        }

        /// <summary>Shows the form with the specified owner to the user.</summary>
        /// <param name="owner">Any object that implements <see cref="Window" /> and represents the top-level window that will own this form. </param>
        public virtual void Show(Window owner)
        {
            this.owner = owner;
            this.Visibility = Visibility.Visible;
            this.OnShown();
        }

        /// <summary>
        /// Invokes the <see cref="Shown"/> event if there are any handlers attached.
        /// </summary>
        protected virtual void OnShown()
        {
            var onShown = this.Shown;
            if (onShown != null)
            {
                onShown.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Invokes the <see cref="Closed"/> event if there are any handlers attached.
        /// </summary>
        protected virtual void OnClosed()
        {
            var onClosed = this.Closed;
            if (onClosed != null)
            {
                onClosed.Invoke(this, EventArgs.Empty);
            }
        }

        /// <returns>
        /// The default <see cref="Size" /> of the control.
        /// </returns>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(300, 300);
            }
        }
    }
}