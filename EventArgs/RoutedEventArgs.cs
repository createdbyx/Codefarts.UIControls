namespace Codefarts.UIControls
{
    using System;

    /// <summary>
    /// Contains state information and event data associated with a event. 
    /// </summary>
    public class RoutedEventArgs : EventArgs
    {
        /// <summary>Gets or sets a value that indicates the present state of the event handling for a routed event as it travels the route. </summary>
        /// <returns>If setting, set to true if the event is to be marked handled; otherwise false. If reading this value, true indicates that either a 
        /// class handler, or some instance handler along the route, has already marked this event handled. false indicates that no such handler 
        /// has marked the event handled. The default value is false.</returns>
        public bool Handled { get; set; }

        /// <summary>Gets or sets a reference to the object that raised the event. </summary>
        /// <returns>The object that raised the event.</returns>
        public object Source { get; set; }
    }

    public class InputEventArgs : RoutedEventArgs
    {

    }

    public class KeyboardEventArgs : InputEventArgs
    {

    }
}