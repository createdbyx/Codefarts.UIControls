namespace Codefarts.UIControls.Models
{
    using System;

    internal class CounterModel
    {
        private int count;

        private int total;

        public event EventHandler Changed;

        public int Count
        {
            get
            {
                return this.count;
            }

            set
            {
                var changed = this.count != value;
                this.count = value;
                if (changed)
                {
                    this.OnChanged();
                }
            }
        }

        public int Total
        {
            get
            {
                return this.total;
            }

            set
            {
                var changed = this.total != value;
                this.total = value;
                if (changed)
                {
                    this.OnChanged();
                }
            }
        }

        protected virtual void OnChanged()
        {
            var handler = this.Changed;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }
    }
}