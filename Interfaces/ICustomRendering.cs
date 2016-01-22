namespace Codefarts.UIControls.Interfaces
{
    using Codefarts.UIControls;

    public interface ICustomRendering
    {
        void Draw(ControlRenderingArgs args);

        void Update(ControlRenderingArgs args);
    }
}