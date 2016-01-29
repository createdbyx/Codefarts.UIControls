namespace Codefarts.UIControls
{
    //  using System;

    using System.Collections.Generic;

    using Codefarts.UIControls.Interfaces;

    public interface IControlRendererManager
    {
        string[] GetControlTypes();

        IList<IControlRenderer> Get(string controlType);

        int Count { get; }

        void DrawControl(Control control, float elapsedGameTime, float totalGameTime);

        void UpdateControl(Control control, float elapsedGameTime, float totalGameTime);
    }
}