namespace Codefarts.UIControls.Controls
{
    using Codefarts.UIControls;

    /// <summary>
    /// Place holder class for xaml/winforms compatibility.
    /// </summary>
    /// <seealso cref="Codefarts.UIControls.Control" />
    public class UserControl : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserControl"/> class.
        /// </summary>
        public UserControl()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserControl"/> class.
        /// </summary>
        /// <param name="name">
        /// The name for the control.
        /// </param>
        public UserControl(string name)  :base(name)
        {
        }
    }
}
