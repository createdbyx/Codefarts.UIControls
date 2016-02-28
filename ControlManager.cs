namespace Codefarts.UIControls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    using Codefarts.Input;
    using Codefarts.Input.Models;

    using UnityEngine;

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
            //  this.ScreenControls = new List<Control>();
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
                    // e.Value = this.Control == null ? e.Value : this.Control.PointToClient(new Point(e.Value, this.currentMouseState.Y)).X;
                    this.previousMouseState.X = this.currentMouseState.X;
                    this.currentMouseState.X = e.Value;//- (this.Control != null ? this.Control.Left : 0);
                  //  Debug.Log("MouseX: " + e.Value);// + " - " + (e.Value - (this.Control != null ? this.Control.Left : 0)));
                    this.ProcessMouseEvent();
                    break;

                case "MouseY":
                    //  e.Value = this.Control == null ? e.Value : this.Control.PointToClient(new Point(this.currentMouseState.X, e.Value)).Y;
                    this.previousMouseState.Y = this.currentMouseState.Y;
                    this.currentMouseState.Y = e.Value;// - (this.Control != null ? this.Control.Top : 0);
                   // Debug.Log("MouseY: " + e.Value);//+ " - " + (e.Value - (this.Control != null ? this.Control.Top : 0)));
                    this.ProcessMouseEvent();
                    break;
            }
        }

        private Stack<MouseEventArgs> argumentsPool;
        private Control lastMouseControl;
        private bool mouseEntered = false;

        private void ProcessMouseEvent()
        {
            //// check each screen control
            //foreach (var screen in this.ScreenControls)
            //{
            //    // try to find a control at the current mouse position
            //    var control = screen.FindControlAtPoint(this.currentMouseState.X, this.currentMouseState.Y);

            var control = this.Control;
            // if the control is null not visible or disabled just ignore
            if (control == null || control.Visibility != Visibility.Visible || !control.IsEnabled)
            {
                return;
            }

            // fetch a arguments reference
            MouseEventArgs args;
            lock (this.argumentsPool)
            {
                args = this.argumentsPool.Count > 0 ? this.argumentsPool.Pop() : new MouseEventArgs();
            }

            // set initial argument state
            var mousePos = new Point(this.currentMouseState.X, this.currentMouseState.Y);
            //  var relativeMousePosition = control.PointToClient(mousePos) - control.Location;
            args.Buttons[Constants.LeftMouseButton] = this.currentMouseState.Buttons[Constants.LeftMouseButton];
            args.Buttons[Constants.RightMouseButton] = this.currentMouseState.Buttons[Constants.RightMouseButton];
            args.Buttons[Constants.MiddleMouseButton] = this.currentMouseState.Buttons[Constants.MiddleMouseButton];
            args.Buttons[Constants.MouseButton4] = this.currentMouseState.Buttons[Constants.MouseButton4];
            args.Buttons[Constants.MouseButton5] = this.currentMouseState.Buttons[Constants.MouseButton5];
            args.X = this.currentMouseState.X;
            args.Y = this.currentMouseState.Y;

            // check if mouse moved (it should this method is begin called by HandleAction method)
            if (Math.Abs(this.currentMouseState.X - this.previousMouseState.X) > float.Epsilon ||
                Math.Abs(this.currentMouseState.Y - this.previousMouseState.Y) > float.Epsilon)
            {
                var ctrl = control.FindControlAtPoint(this.currentMouseState.X, this.currentMouseState.Y);
                if (ctrl != this.lastMouseControl)
                {
                    if (this.lastMouseControl != null && this.mouseEntered)
                    {
                        this.RestrictMousePositionToControlBounds(args, this.lastMouseControl, this.lastMouseControl.PointToClient(mousePos));
                        args.Type = MouseEventType.MouseLeave;
                        this.lastMouseControl.OnMouseEvent(args);
                        this.lastMouseControl.Properties[Control.IsMouseOverKey] = false;
                        this.lastMouseControl = null;
                        this.mouseEntered = false;
                    }

                    if (ctrl != null && this.mouseEntered == false)
                    {
                        this.RestrictMousePositionToControlBounds(args, ctrl, ctrl.PointToClient(mousePos));
                        args.Type = MouseEventType.MouseEnter;
                        ctrl.OnMouseEvent(args);
                        this.mouseEntered = true;
                        this.lastMouseControl = ctrl;
                        this.lastMouseControl.Properties[Control.IsMouseOverKey] = true;
                    }
                }
                else
                {
                    if (ctrl != null)
                    {
                        this.RestrictMousePositionToControlBounds(args, ctrl, ctrl.PointToClient(mousePos));
                        args.Type = MouseEventType.MouseMove;
                        ctrl.OnMouseEvent(args);
                    }
                }
            }

            lock (this.argumentsPool)
            {
                this.argumentsPool.Push(args);
            }
            //  }
        }

        private void RestrictMousePositionToControlBounds(MouseEventArgs args, Control control, Point mousePosition)
        {
            var rel = mousePosition;//- control.Location;
            args.X = Math.Max(0, rel.X);
            args.Y = Math.Max(0, rel.Y);
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
