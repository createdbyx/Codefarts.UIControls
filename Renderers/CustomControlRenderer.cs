namespace Codefarts.UIControls.Code.Renderers
{
    using System;        

    public class CustomControlRenderer : BaseRenderer
    {
        public override Type ControlType
        {
            get
            {
                return typeof(CustomControl);
            }
        }

        public override void DrawControl(IControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var customControl = control as CustomControl;
            customControl.OnDraw(manager, elapsedGameTime, totalGameTime);
        }

        public override void Update(IControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var customControl = control as CustomControl;
            customControl.OnUpdate(manager, elapsedGameTime, totalGameTime);
        }
    }
}