namespace Codefarts.UIControls
{
    using System;

    public class RoutedEventArgs : EventArgs
    {
        public bool Handled { get; set; }

        public object Source { get; set; }
    }

    public class InputEventArgs : RoutedEventArgs
    {

    }

    public class KeyboardEventArgs : InputEventArgs
    {

    }
}