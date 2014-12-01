namespace Codefarts.UIControls.Code.Renderers
{
    using System;

    using Codefarts.UIControls.Unity;

    public abstract class GenericCustomControlRenderer<T> : CustomControlRenderer where T : CustomControl
    {
        public override Type ControlType
        {
            get
            {
                return typeof(T);
            }
        }
    }
}