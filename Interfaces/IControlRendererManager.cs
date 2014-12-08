namespace Codefarts.UIControls
{
    using System;

    using Codefarts.UIControls.Interfaces;

    public interface IControlRendererManager
    {
        Type[] GetControlTypes();

        IControlRenderer Get(Type controlType);

        int Count { get; }

        void DrawControl(Control control, float elapsedGameTime, float totalGameTime);

        void UpdateControl(Control control, float elapsedGameTime, float totalGameTime);
    }
}