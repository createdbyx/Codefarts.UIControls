namespace Codefarts.UIControls.Interfaces
{
    using System;
                                          
    public interface IControlRenderer
    {
        Type ControlType { get; }

        void Draw(IControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime);

        void Update(IControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime);   
    }
}
