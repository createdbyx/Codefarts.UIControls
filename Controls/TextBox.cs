/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.UIControls
{
    public class TextBox : Control
    {
        public event System.EventHandler<RoutedPropertyChangedEventArgs<string>> TextChanged;
        private string text;
        public ScrollBarVisibility HorizontialScrollBarVisibility { get; set; }
        public ScrollBarVisibility VerticalScrollBarVisibility { get; set; }
        public float HorizontialOffset { get; set; }
        public float VerticalOffset { get; set; }

        public virtual string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                var changed = this.text != value;
                var oldValue = this.text;
                this.text = value;
                if (changed)
                {
                    this.OnTextChanged(new RoutedPropertyChangedEventArgs<string>(oldValue, value));
                }
            }
        }

        public void OnTextChanged(RoutedPropertyChangedEventArgs<string> e)
        {
            var handler = this.TextChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public TextBox()
        {
            this.HorizontialScrollBarVisibility = ScrollBarVisibility.Auto;
            this.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        }
    }
}