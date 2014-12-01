/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

using System;

namespace Codefarts.UIControls
{
    public class SelectionChangedEventArgs : System.EventArgs
    {
        public int OldValue { get; set; }
        public int NewValue { get; set; }

        public SelectionChangedEventArgs(int oldValue, int newValue)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        public SelectionChangedEventArgs()
        {

        }
    }
}