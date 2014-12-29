namespace Codefarts.UIControls
{
    public enum DrawMode
    {
        /// <summary>
        ///  All the elements are drawn by the assigned list box renderer.
        /// </summary>
        Normal,

        /// <summary>
        /// All elements are drawn using custom drawing callbacks.
        /// </summary>
        OwnerDraw
    }
}