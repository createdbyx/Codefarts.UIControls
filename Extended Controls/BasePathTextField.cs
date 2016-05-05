namespace Codefarts.UIControls.Controls
{
    using System;
    using System.IO;

    using Codefarts.UIControls;

    /// <summary>
    /// Provides a text field with a button attached to the right.
    /// </summary>
    public abstract class BasePathTextField : StackPanel
    {
        /// <summary>
        /// The text box to display the path.
        /// </summary>
        protected TextBox txtPath;

        /// <summary>
        /// The select button.
        /// </summary>
        protected Button btnSelect;

        /// <summary>
        /// The folder select backing field for the <see cref="FolderSelect"/> property.
        /// </summary>
        private bool folderSelect;

        /// <summary>
        /// The container that holds the text box and button.
        /// </summary>
        private StackPanel container;

        /// <summary>
        /// The label that is used to display text above the text field.
        /// </summary>
        private TextBlock lblText;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePathTextField"/> class.
        /// </summary>
        /// <param name="label">The value to set the <see cref="Label"/> property to.</param>
        protected BasePathTextField(string label)
            : this()
        {
            this.Label = label;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePathTextField"/> class.
        /// </summary>
        protected BasePathTextField() : base(Orientation.Vertical)
        {
            this.lblText = new TextBlock();
            this.Controls.Add(this.lblText);
            this.container = new StackPanel(Orientation.Horizontal);
            this.txtPath = new TextBox();
            this.container.Controls.Add(this.txtPath);
            this.btnSelect = new Button("...");
            this.container.Controls.Add(this.btnSelect);
            this.btnSelect.Click += (s, e) => this.ShowDialog();  
        }

        /// <summary>
        /// Shows the folder dialog.
        /// </summary>
        /// <remarks>Override this method to handle different environments.</remarks>
        protected abstract void ShowDialog();

        /// <summary>
        /// Gets or sets a value indicating whether the path is a folder path.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the path is supposed to be a folder path; otherwise, <c>false</c> indicating a file path.
        /// </value>
        public virtual bool FolderSelect
        {
            get
            {
                return this.folderSelect;
            }

            set
            {
                var changed = this.folderSelect != value;
                this.folderSelect = value;
                if (changed)
                {
                    this.OnPropertyChanged("FolderSelect");
                }
            }
        }

        /// <summary>
        /// Selecteds the path is valid.
        /// </summary>
        /// <returns>true if the path in the text box is valid; otherwise false.</returns>
        public virtual bool SelectedPathIsValid()
        {
#if PORTABLE
            throw new NotImplementedException();
#else
            return this.FolderSelect ? Directory.Exists(this.Path) : File.Exists(this.Path);
#endif
        }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <remarks>Is just a wrapper for <see cref="txtPath"/> <see cref="TextBox.Text"/> property.</remarks>
        public virtual string Path
        {
            get
            {
                return this.txtPath.Text;
            }

            set
            {
                var changed = this.txtPath.Text != value;
                this.txtPath.Text = value;
                if (changed)
                {
                    this.OnPropertyChanged("Path");
                }
            }
        }

        /// <summary>
        /// Gets or sets the laber header text.
        /// </summary>
        /// <remarks>Is just a wrapper for <see cref="lblText"/> <see cref="TextBlock.Text"/> property.</remarks>
        public virtual string Label
        {
            get
            {
                return this.lblText.Text;
            }

            set
            {
                var changed = this.lblText.Text != value;
                this.lblText.Text = value;
                if (changed)
                {
                    this.OnPropertyChanged("Label");
                }
            }
        }

        #region Overrides of StackPanel

        /// <returns>
        /// The default <see cref="Size" /> of the control.
        /// </returns>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(200, 32);
            }
        }

        #endregion
    }
}