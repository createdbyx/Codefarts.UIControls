namespace Codefarts.UIControls.Interfaces
{
  //  using System;

    /// <summary>
    /// Provides a interface for drawing and updating controls.
    /// </summary>
    public interface IControlRenderer
    {
        /// <summary>
        /// Gets the type of the control that the renderer is designed to render.
        /// </summary> 
       // Type ControlType { get; }

        /// <summary>
        /// Draws the specified control.
        /// </summary>
        /// <param name="args">The rendering argument information.</param>
        void Draw(ControlRenderingArgs args);

        /// <summary>
        /// Updates the specified control.
        /// </summary>
        /// <param name="args">The rendering argument information.</param>
        /// <remarks>
        /// <p>This method is provided if a control has is animated and it's animation state can be updated independently of drawing.</p>
        /// <p>Updates generally occur more frequently then draws.</p> 
        /// </remarks>
        void Update(ControlRenderingArgs args);
    }
}
