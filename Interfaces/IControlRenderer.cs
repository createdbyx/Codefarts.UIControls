namespace Codefarts.UIControls.Interfaces
{
    using System;

    /// <summary>
    /// Provides a interface for drawing and updating controls.
    /// </summary>
    public interface IControlRenderer
    {
        /// <summary>
        /// Gets the type of the control that the renderer is designed to render.
        /// </summary> 
        Type ControlType { get; }

        /// <summary>
        /// Draws the specified control.
        /// </summary>
        /// <param name="manager">The manager containing all control renderers.</param>
        /// <param name="control">The control to be rendered.</param>
        /// <param name="elapsedGameTime">The elapsed game time.</param>
        /// <param name="totalGameTime">The total game time.</param>
        void Draw(IControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime);

        /// <summary>
        /// Updates the specified control.
        /// </summary>
        /// <param name="manager">The manager containing all control renderers.</param>
        /// <param name="control">The control to be updated.</param>
        /// <param name="elapsedGameTime">The elapsed game time.</param>
        /// <param name="totalGameTime">The total game time.</param>
        /// <remarks>
        /// <p>This method is provided if a control has is animated and it's animation state can be updated independently of drawing.</p>
        /// <p>Updates generally occur more frequently then draws.</p> 
        /// </remarks>
        void Update(IControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime);
    }
}
