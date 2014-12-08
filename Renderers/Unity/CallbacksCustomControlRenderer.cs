namespace Codefarts.UIControls.Code.Renderers
{
    public class CallbacksCustomControlRenderer : GenericCustomControlRenderer<CallbacksCustomControl>
    {
        public override void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var callbacks = control as CallbacksCustomControl;
            if (callbacks != null)
            {
                callbacks.Draw(manager, elapsedGameTime, totalGameTime);
            }
        }

        public override void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var callbacks = control as CallbacksCustomControl;
            if (callbacks != null)
            {
                callbacks.Update(manager, elapsedGameTime, totalGameTime);
            }
        }
    }
}