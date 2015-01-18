namespace Codefarts.UIControls
{
    using UnityEngine;

    /// <summary>Provides data for the <see cref="E:Control.KeyUp" /> and <see cref="E:Control.KeyDown" /> routed events, 
    /// as well as related attached and Preview events.</summary>
    public class KeyEventArgs : KeyboardEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEventArgs"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="isDown">if set to <c>true</c> [is down].</param>
        /// <param name="isUp">if set to <c>true</c> [is up].</param>
        public KeyEventArgs(KeyCode key, bool isDown, bool isUp)
        {
            this.IsDown = isDown;
            this.IsUp = isUp;
            this.Key = key;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEventArgs"/> class.
        /// </summary>
        public KeyEventArgs()
        {
        }

        /// <summary>Gets a value that indicates whether the key referenced by the event is in the down state. </summary>
        /// <returns>true if the key is down; otherwise, false.</returns>
        public bool IsDown { get; private set; }

        /// <summary>Gets a value that indicates whether the key referenced by the event is in the toggled state. </summary>
        /// <returns>true if the key is toggled; otherwise, false.  There is no default value.</returns>
        public bool IsToggled { get; private set; }

        /// <summary>Gets a value that indicates whether the key referenced by the event is in the up state. </summary>
        /// <returns>true if the key is up; otherwise, false.  There is no default value.</returns>
        public bool IsUp { get; private set; }

        /// <summary>Gets the keyboard key associated with the event. </summary>
        /// <returns>The <see cref="T:System.Windows.Input.Key" /> referenced by the event.</returns>
        public KeyCode Key { get; private set; }   
    }
}