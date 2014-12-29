namespace Codefarts.UIControls
{
    using System;

    public class ListBoxItemInformationArgs : EventArgs
    {
        public object Item { get; set; }

        public int Index { get; set; }

        public float ItemWidth { get; set; }

        public float ItemHeight { get; set; }

        public float ElapsedGameTime { get; set; }
        public float TotalGameTime { get; set; }
    }
}