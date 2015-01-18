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
       

        protected string text;
        public ScrollBarVisibility HorizontalScrollBarVisibility { get; set; }
        public ScrollBarVisibility VerticalScrollBarVisibility { get; set; }
        public float HorizontalOffset { get; set; }
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
                if (!changed)
                {
                    return;
                }

                value = !this.AcceptsReturn ? value.Replace("\r\n", string.Empty) : value;

                var oldValue = this.text;
                this.text = value;
                this.OnTextChanged(new RoutedPropertyChangedEventArgs<string>(oldValue, value));
            }
        }


        protected virtual void OnTextChanged(RoutedPropertyChangedEventArgs<string> e)
        {
            var handler = this.TextChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the text box accepts return keys.
        /// </summary> 
        public virtual bool AcceptsReturn { get; set; }
      
        /// <summary>
        /// Gets or sets a value indicating whether the text box accepts tab keys.
        /// </summary> 
        public bool AcceptsTab { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBox"/> class.
        /// </summary>
        public TextBox()
        {
            this.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.AcceptsReturn = true;
        }
    }
}