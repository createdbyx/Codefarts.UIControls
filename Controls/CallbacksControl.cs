namespace Codefarts.UIControls
{
    using System;

    public class CallbacksControl : CustomControl
    {
        public Action<ControlRenderingArgs> Draw;
        public Action<ControlRenderingArgs> Update;

        public override void OnDraw(ControlRenderingArgs args)
        {
            var action = this.Draw;
            if (action != null)
            {
                action(args);
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