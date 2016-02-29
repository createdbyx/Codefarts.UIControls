namespace Codefarts.UIControls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    using Codefarts.Input;
    using Codefarts.Input.Models;

    /// <summary>
    /// Used to manager which control has focus, and directs device input to controls.
    /// </summary>
    public class ControlManager : INotifyPropertyChanged
    {
        /// <summary>
        /// The input manger used to handle user input
        /// </summary>
        private InputManager inputManger;

        /// <summary>
        /// The current mouse state.
        /// </summary>
        private MouseState currentMouseState;

        /// <summary>
        /// The previous mouse state.
        /// </summary>
        private MouseState previousMouseState;

        /// <summary>
        /// The cached property arguments.
        /// </summary>
        protected IDictionary<string, PropertyChangedEventArgs> propertyArgs = new Dictionary<string, PropertyChangedEventArgs>();

        /// <summary>
        /// The arguments pool used for object recycling and prevent garbage collection.
        /// </summary>
        private Stack<MouseEventArgs> argumentsPool;

        /// <summary>
        /// The last mouse control that the mouse was over.
        /// </summary>
        private Control lastMouseControl;

        /// <summary>
        /// The mouse entered flag used to specify weather or noth the mouse is currently over a control.
        /// </summary>
        private bool mouseEntered;

        /// <summary>
        /// The backing field for the <see cref="InputEnabled"/> property.
        /// </summary>
        private bool inputEnabled = true;

        /// <summary>
        /// Gets or sets a value indicating whether input is enabled.
        /// </summary>   
        /// <remarks><p>Setting this flag to false allows you to immediatley stop input from being sent to controls.</p>
        /// <p>Take care when using this as there can be consequenses like disabling input after a mouse down event but 
        /// before a mouse up event causing the control input & input state mismatches.</p></remarks>
        public bool InputEnabled
        {
            get
            {
                return this.inputEnabled;
            }

            set
            {
                var changed = this.inputEnabled != value;
                this.inputEnabled = value;
                if (changed)
                {
                    this.OnPropertyChanged("InputEnabled");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlManager"/> class.
        /// </summary>
        public ControlManager()
        {
            this.currentMouseState.Buttons = new float[Constants.MaxMouseButtons];
            this.previousMouseState.Buttons = new float[Constants.MaxMouseButtons];
            this.inputManger = new InputManager();
            this.inputManger.Action += this.HandleAction;
            this.argumentsPool = new Stack<MouseEventArgs>();
        }


        /// <summary>
        /// Gets the binding names that the <see cref="ControlManager"/> is designed to work with.
        /// </summary>
        /// <remarks>Use these binding names to bind to device sources so the control manager can recieve data from them.</remarks>
        public string[] BindingNames
        {
            get
            {
                return new[]
                    {
                        "MouseX",
                        "MouseY",
                        "MouseLeft",
                        "MouseRight",
                        "MouseMiddle",
                    };
            }
        }

        //public List<Control> ScreenControls { get; set; }
        public Control Control { get; set; }

        /// <summary>
        /// Handles the action.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void HandleAction(object sender, BindingData e)
        {
            if (!this.InputEnabled)
            {
                return;
            }

            switch (e.Name)
            {
                case "MouseX":
                    this.previousMouseState.X = this.currentMouseState.X;
                    this.currentMouseState.X = e.Value;
                    this.ProcessMouseEvent();
                    break;

                case "MouseY":
                    this.previousMouseState.Y = this.currentMouseState.Y;
                    this.currentMouseState.Y = e.Value;
                    this.ProcessMouseEvent();
                    break;

                case "MouseLeft":
                    this.previousMouseState.Buttons[Constants.LeftMouseButton] = this.currentMouseState.Buttons[Constants.LeftMouseButton];
                    this.currentMouseState.Buttons[Constants.LeftMouseButton] = e.Value;
                    this.ProcessMouseEvent();
                    break;

                case "MouseRight":
                    this.previousMouseState.Buttons[Constants.RightMouseButton] = this.currentMouseState.Buttons[Constants.RightMouseButton];
                    this.currentMouseState.Buttons[Constants.RightMouseButton] = e.Value;
                    this.ProcessMouseEvent();
                    break;

                case "MouseMiddle":
                    this.previousMouseState.Buttons[Constants.MiddleMouseButton] = this.currentMouseState.Buttons[Constants.MiddleMouseButton];
                    this.currentMouseState.Buttons[Constants.MiddleMouseButton] = e.Value;
                    this.ProcessMouseEvent();
                    break;
            }
        }

        /// <summary>
        /// Processes mouse events like MouseEnter, MouseLeave, MouseMove, MouseDown, MouseUp etc.
        /// </summary>
        private void ProcessMouseEvent()
        {
            var control = this.Control;

            // if the control is null not visible or disabled just ignore
            if (control == null || control.Visibility != Visibility.Visible || !control.IsEnabled)
            {
                return;
            }

            // detect if a button state changed
            var buttonStates = new float[Constants.MaxMouseButtons];
            var buttonsUp = new float[Constants.MaxMouseButtons];
            var buttonsDown = new float[Constants.MaxMouseButtons];
            var buttonStatesHaveChanged = false;
            var buttonsReleased = false;
            var buttonsPressed = false;
            for (var i = 0; i < Constants.MaxMouseButtons; i++)
            {
                var currentState = this.currentMouseState.Buttons[i];
                var delta = currentState - this.previousMouseState.Buttons[i];
                buttonsUp[i] = delta < float.Epsilon ? currentState : 0;
                buttonsDown[i] = delta > float.Epsilon ? currentState : 0;
                buttonStates[i] = currentState;
                buttonStatesHaveChanged = buttonStatesHaveChanged || Math.Abs(delta) > float.Epsilon;
                buttonsReleased = buttonsReleased || delta > float.Epsilon;
                buttonsPressed = buttonsPressed || delta < float.Epsilon;
            }

            // check if mouse moved (it should this method is begin called by HandleAction method)
            if (!(Math.Abs(this.currentMouseState.X - this.previousMouseState.X) > float.Epsilon) && 
                !(Math.Abs(this.currentMouseState.Y - this.previousMouseState.Y) > float.Epsilon) && 
                !buttonStatesHaveChanged)
            {
                return;
            }

            // create a new MouseEventArgs or fetch from the pool
            MouseEventArgs args;
            lock (this.argumentsPool)
            {
                args = this.argumentsPool.Count > 0 ? this.argumentsPool.Pop() : new MouseEventArgs();
            }

            // set initial argument state
            var mousePosition = new Point(this.currentMouseState.X, this.currentMouseState.Y);
            args.Buttons = buttonStates;
            args.X = mousePosition.X;
            args.Y = mousePosition.Y;

            // try to get a control at the current mouse position
            var mouseOverControl = control.FindControlAtPoint(args.X, args.Y);

            // check for mouse button released event
            if (buttonStatesHaveChanged && buttonsReleased && this.lastMouseControl != null)
            {
                args.Type = MouseEventType.MouseUp;
                args.Buttons = buttonsUp;
                this.lastMouseControl.OnMouseEvent(args);
            }

            // handle mouse enter, leave, move
            if (mouseOverControl != this.lastMouseControl && !buttonStatesHaveChanged)
            {
                if (this.lastMouseControl != null && this.mouseEntered)
                {
                    this.RestrictMousePositionToControlBounds(args, this.lastMouseControl, this.lastMouseControl.PointToClient(mousePosition));
                    args.Type = MouseEventType.MouseLeave;
                    this.lastMouseControl.OnMouseEvent(args);
                    this.lastMouseControl.Properties[Control.IsMouseOverKey] = false;
                    this.lastMouseControl = null;
                    this.mouseEntered = false;
                }

                if (mouseOverControl != null && this.mouseEntered == false)
                {
                    this.RestrictMousePositionToControlBounds(args, mouseOverControl, mouseOverControl.PointToClient(mousePosition));
                    args.Type = MouseEventType.MouseEnter;
                    mouseOverControl.OnMouseEvent(args);
                    this.mouseEntered = true;
                    this.lastMouseControl = mouseOverControl;
                    this.lastMouseControl.Properties[Control.IsMouseOverKey] = true;
                }
            }
            else
            {
                if (mouseOverControl != null)
                {
                    this.RestrictMousePositionToControlBounds(args, mouseOverControl, mouseOverControl.PointToClient(mousePosition));
                    args.Type = MouseEventType.MouseMove;
                    mouseOverControl.OnMouseEvent(args);
                }
            }

            // check for mouse button pressed event
            if (buttonStatesHaveChanged && buttonsPressed && mouseOverControl != null)
            {
                args.Type = MouseEventType.MouseDown;
                args.Buttons = buttonsDown;
                mouseOverControl.OnMouseEvent(args);
            }

            // Push arguemnts to be reused at a later time.
            // Note that if an exception was previously thrown this will never be executed and 
            // it's possible that another MouseEventArgs instant may get created.
            lock (this.argumentsPool)
            {
                this.argumentsPool.Push(args);
            }                                 
        }

        /// <summary>
        /// Restricts the mouse position to control bounds.
        /// </summary>
        /// <param name="args">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        /// <param name="control">The control.</param>
        /// <param name="mousePosition">The mouse position.</param>
        private void RestrictMousePositionToControlBounds(MouseEventArgs args, Control control, Point mousePosition)
        {
            args.X = Math.Max(0, mousePosition.X);
            args.Y = Math.Max(0, mousePosition.Y);
            args.X = Math.Min(control.Width, args.X);
            args.Y = Math.Min(control.Height, args.Y);
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

        /// <summary>
        /// Occurs when a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when a property has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                if (!this.propertyArgs.ContainsKey(propertyName))
                {
                    this.propertyArgs[propertyName] = new PropertyChangedEventArgs(propertyName);
                }

                handler(this, this.propertyArgs[propertyName]);
            }
        }
    }
}
