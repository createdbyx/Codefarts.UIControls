namespace Codefarts.UIControls
{
    using Codefarts.Input;

    using UnityEngine;

    /// <summary>
    /// Used to manager which control has focus, and directs device input to controls.
    /// </summary>
    public class ControlManager
    {
        /// <summary>
        /// The input manger used to handle user input
        /// </summary>
        private InputManager inputManger;
                            
        private float mouseX;
        private float mouseY;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlManager"/> class.
        /// </summary>
        public ControlManager()
        {
            this.inputManger = new InputManager();
            this.inputManger.Action += this.HandleAction;
        }
                         
        /// <summary>
        /// Handles the action.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void HandleAction(object sender, Codefarts.Input.Models.BindingData e)
        {
            switch (e.Name)
            {
                case "MouseX":
                    this.mouseX = e.Value;
                    break;

                case "MouseY":
                    this.mouseY = e.Value;
                    break;        
            }
        }

        /// <summary>
        /// Gets the input manger reference.
        /// </summary>
        public InputManager InputManger
        {
            get
            {
                return this.inputManger;
            }
        }

        /// <summary>
        /// Updates the input manager and directs events to controls tha need it.
        /// </summary>
        public void Update()
        {
            this.inputManger.Update();
        }
    }
}
