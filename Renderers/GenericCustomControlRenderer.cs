namespace Codefarts.UIControls.Renderers
{
    using System;             

    public abstract class GenericCustomControlRenderer<T> : CustomControlRenderer
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