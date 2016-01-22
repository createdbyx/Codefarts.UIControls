namespace Codefarts.UIControls
{
    using System;

    public class CallbacksControl : CustomControl
    {
        public Action<IControlRendererManager, float, float> Draw;
        public Action<ControlRenderingArgs> Update;

        public override void OnDraw(IControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            var action = this.Draw;
            if (action != null)
            {
                action(manager, elapsedGameTime, totalGameTime);
            }
        }

        public override void OnUpdate(ControlRenderingArgs args)
        {
            var action = this.Update;
            if (action != null)
            {
                action(args);
            }
        }
    }
}