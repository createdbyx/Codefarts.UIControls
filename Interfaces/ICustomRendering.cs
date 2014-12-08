namespace Codefarts.UIControls.Interfaces
{
    using Codefarts.UIControls;

    public interface ICustomRendering
    {
        void Draw(IControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime);

        void Update(IControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime);
    }
}