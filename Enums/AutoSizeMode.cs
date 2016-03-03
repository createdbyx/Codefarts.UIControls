namespace Codefarts.UIControls
{
    /// <summary>
    /// Specifies how a control will behave when its <see cref="Control.AutoSize" /> property is enabled.
    /// </summary>
    public enum AutoSizeMode
    {
        /// <summary>
        /// The control grows or shrinks to fit its contents. The control cannot be resized manually.
        /// </summary>
        GrowAndShrink,

        /// <summary>
        /// The control grows as much as necessary to fit its contents but does not shrink smaller than the value of
        /// its <see cref="Control.Width" /> & <see cref="Control.Height" /> properties.
        /// The form can be resized, but cannot be made so small that any of its contained controls are hidden.
        /// </summary>
        GrowOnly
    }
}