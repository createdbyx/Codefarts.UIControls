namespace Codefarts.UIControls.Interfaces
{
    using System;

    using Codefarts.UIControls.Code;

    public interface IControlRenderer
    {
        Type ControlType { get; }

        void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime);

        void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime);   
    }
}
