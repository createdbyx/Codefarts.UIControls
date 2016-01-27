namespace Codefarts.UIControls
{
    using System;

    public static class MarkupExtensionMethods
    {
        public static void SetProperty(this Markup markup, string name, bool conditional, object value)
        {
            if (markup == null)
            {
                throw new ArgumentNullException("markup");
            }

            if (!conditional)
            {
                return;
            }

            markup.Properties[name] = value;
        }
    }
}