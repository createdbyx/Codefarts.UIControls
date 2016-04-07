namespace Codefarts.UIControls
{
    /// <summary>
    /// <summary>Displays a <see cref="T:ListBox" /> in which a check box is displayed to the left of each item.</summary>
    /// </summary>
    public class CheckedListBox : ListBox
    {
        /// <summary>
        /// The backing field for the <see cref="CheckOnClick"/> property.
        /// </summary>
        protected bool checkOnClick;

        /// <summary>
        /// Gets or sets a value indicating whether the check box should be toggled when an item is selected.
        /// </summary>
        /// <returns>
        /// true if the check mark is applied immediately; otherwise, false. The default is false.
        /// </returns>
        public virtual bool CheckOnClick
        {
            get
            {
                return this.checkOnClick;
            }

            set
            {
                var changed = this.checkOnClick != value;
                this.checkOnClick = value;
                if (changed)
                {
                    this.OnPropertyChanged("CheckOnClick");
                }
            }
        }
    }
}