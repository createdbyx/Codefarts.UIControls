namespace Codefarts.UIControls
{
    /// <summary>
    /// Defines the selection behavior for a <see cref="ListBox" />.
    /// </summary>
    public enum SelectionMode
    {
        /// <summary>
        /// No items can be selected.
        /// </summary>
        None,

        /// <summary>
        /// The user can select only one item at a time.
        /// </summary>
        Single,

        /// <summary>
        /// The user can select multiple items without holding down a modifier key.
        /// </summary>
        Multiple,

        /// <summary>
        /// The user can select multiple consecutive items while holding down the SHIFT key.
        /// </summary>
        Extended
    }
}