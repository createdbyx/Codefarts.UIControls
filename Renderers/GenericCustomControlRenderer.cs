namespace Codefarts.UIControls.Code.Renderers
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