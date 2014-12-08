namespace Codefarts.UIControls
{
    using System;

    public class CallbacksCustomControl : CustomControl
    {
        public Action<IControlRendererManager, float, float> Draw;
        public Action<IControlRendererManager, float, float> Update;

        public override void OnDraw(IControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            var action = this.Draw;
            if (action != null)
            {
                action(manager,elapsedGameTime,totalGameTime);
            }
        }

        public override void OnUpdate(IControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            var action = this.Update;
            if (action != null)
            {
                action(manager, elapsedGameTime, totalGameTime);
            }
        }
    }
}