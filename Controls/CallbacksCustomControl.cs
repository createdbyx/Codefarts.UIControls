namespace Codefarts.UIControls
{
    using System;

    public class CallbacksCustomControl : CustomControl
    {
        public Action<ControlRendererManager, float, float> Draw;
        public Action<ControlRendererManager, float, float> Update;

        public override void OnDraw(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            var action = this.Draw;
            if (action != null)
            {
                action(manager,elapsedGameTime,totalGameTime);
            }
        }

        public override void OnUpdate(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            var action = this.Update;
            if (action != null)
            {
                action(manager, elapsedGameTime, totalGameTime);
            }
        }
    }
}