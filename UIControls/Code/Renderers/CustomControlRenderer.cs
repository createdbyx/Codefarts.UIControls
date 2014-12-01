namespace Codefarts.UIControls.Code.Renderers
{
    using System;

    using Codefarts.UIControls.Interfaces;
    using Codefarts.UIControls.Unity;     

    public class CustomControlRenderer : IControlRenderer
    {
        public virtual Type ControlType
        {
            get
            {
                return typeof(CustomControl);
            }
        }

        public virtual void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var customControl = control as CustomControl;
            if (customControl == null)
            {
                return;
            }

            customControl.OnDraw(manager, elapsedGameTime, totalGameTime);
        }

        public virtual void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var customControl = control as CustomControl;
            if (customControl == null)
            {
                return;
            }

            customControl.OnUpdate(manager, elapsedGameTime, totalGameTime);
        }
    }
}