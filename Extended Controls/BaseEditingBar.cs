namespace Codefarts.UIControls.Controls
{
    using System;

    /// <summary>
    /// Provides a base control for a horizontal bar that has a path text box and New/Open/Save/SaveAs buttons.
    /// </summary>
    /// <seealso cref="Codefarts.UIControls.Grid" />
    public abstract class BaseEditingBar : Grid
    {
        private TextBox txtPath;
        private Button btnNew;
        private Button btnOpen;
        private Button btnSave;
        private Button btnSaveAs;

        public event EventHandler New;

        public event EventHandler Open;

        public event EventHandler Save;

        public event EventHandler<PropertyChangedEventArgs<string>> SaveAs;

        public string FileExtension { get; set; }

        /// <summary>
        /// Gets or sets the selected path.
        /// </summary>
        public string Path
        {
            get
            {
                return this.txtPath.Text;
            }

            set
            {
                this.txtPath.Text = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEditingBar"/> class.
        /// </summary>
        public BaseEditingBar() : base(6, 1)
        {
            this.txtPath = new TextBox("txtPath")
            {
                AcceptsReturn = false,
                AcceptsTab = false,
                IsEnabled = false,
            };

            this.btnNew = new Button("txtNew", "New") { VerticalAlignment = VerticalAlignment.Stretch, Size = new Size(30, this.Height) };
            this.btnOpen = new Button("btnOpen", "Open") { VerticalAlignment = VerticalAlignment.Stretch, Size = new Size(30, this.Height) };
            this.btnSave = new Button("btnSave", "Save") { VerticalAlignment = VerticalAlignment.Stretch, Size = new Size(30, this.Height) };
            this.btnSaveAs = new Button("btnSaveAs", "Save As") { VerticalAlignment = VerticalAlignment.Stretch, Size = new Size(40, this.Height) };

            this.btnNew.Click += this.NewOnClick;
            this.btnOpen.Click += this.OpenOnClick;
            this.btnSave.Click += this.SaveOnClick;
            this.btnSaveAs.Click += this.SaveAsOnClick;

            this.ColumnDefinitions[0].Width = 30;
            this.ColumnDefinitions[2].Width = 40;
            this.ColumnDefinitions[3].Width = 40;
            this.ColumnDefinitions[4].Width = 40;
            this.ColumnDefinitions[5].Width = 60;

            this.SetCell(0, 0, new TextBlock("File:"));
            this.SetCell(1, 0, this.txtPath);
            this.SetCell(2, 0, this.btnNew);
            this.SetCell(3, 0, this.btnOpen);
            this.SetCell(4, 0, this.btnSave);
            this.SetCell(5, 0, this.btnSaveAs);
        }

        private void NewOnClick(object sender, EventArgs e)
        {
            this.txtPath.Text = null;
            var handler = this.New;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OpenOnClick(object sender, EventArgs e)
        {
            var file = this.ShowOpenPathDialog("Open", string.Empty);
            if (file.Length == 0)
            {
                return;
            }

            this.txtPath.Text = file;
            var handler = this.Open;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected abstract string ShowOpenPathDialog(string title, string path);

        protected abstract string ShowSavePathDialog(string title, string path);

        private void SaveOnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPath.Text))
            {
                return;
            }

            var handler = this.Save;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void SaveAsOnClick(object sender, EventArgs e)
        {
#if UNITY_5
            var file = this.ShowSavePathDialog("Save As", Environment.CurrentDirectory);
#else
            var file = this.ShowSavePathDialog("Save As", string.Empty);
#endif
            if (file.Length == 0)
            {
                return;
            }

            var oldValue = this.txtPath.Text;
            this.txtPath.Text = file;

            var handler = this.SaveAs;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs<string>(oldValue, this.txtPath.Text));
            }
        }
    }
}