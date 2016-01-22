namespace Codefarts.UIControls.Renderers
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

        public override void Update(ControlRenderingArgs args)
        {
            var customControl = args.Control as CustomControl;
            customControl.OnUpdate(args);
        }
    }
}