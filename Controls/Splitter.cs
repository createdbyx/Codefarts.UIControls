namespace Codefarts.UIControls
{
    /// <summary>
    /// Used to group collections of controls.
    /// </summary>
    public class Splitter : Control
    {
        protected DragableControlHandler drag;

        protected Orientation orientation = Orientation.Vertical;

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
        /// Initializes a new instance of the <see cref="Splitter"/> class.
        /// </summary>
        public Splitter()
        {
            this.drag = new DragableControlHandler(this);
            this.drag.AllowVertical = false;
            this.drag.Dragged += this.OnDragged;
        }

        private void OnDragged(object sender, System.EventArgs e)
        {

        }
    }
}