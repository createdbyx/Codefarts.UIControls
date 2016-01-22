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

        public override void DrawControl(ControlRenderingArgs args)
        {
            var customControl = args.Control as CustomControl;
            customControl.OnDraw(args);
        }

        public override void Update(ControlRenderingArgs args)
        {
            var customControl = args.Control as CustomControl;
            customControl.OnUpdate(args);
        }
    }
}