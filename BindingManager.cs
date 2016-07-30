namespace Codefarts.UIControls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class BindingManager
    {
        private IDictionary<string, IExecuter> namedBindings = new Dictionary<string, IExecuter>();

        private interface IExecuter
        {
            void Execute();
            string Name { get; set; }
            INotifyPropertyChanged Source { get; set; }
        }

        private class BindingModel<T> : IExecuter
        {
            public Action<T> SetLeftValue;
            public Action<T> SetRightValue;

            public Func<T> GetLeftValue;
            public Func<T> GetRightValue;

            public T PreviousLeftValue;
            public T PreviousRightValue;

            public INotifyPropertyChanged Source { get; set; }

            public string Name { get; set; }

            public void Execute()
            {
                this.SetLeftValue(this.GetLeftValue());
            }
        }

        public int Count
        {
            get
            {
                return this.namedBindings.Count;
            }
        }
            
        public void Bind<T>(INotifyPropertyChanged source, string name, Func<T> getValue, Action<T> setValue)
        {
            if (getValue == null)
            {
                throw new ArgumentNullException("getValue");
            }

            if (setValue == null)
            {
                throw new ArgumentNullException("setValue");
            }

            this.namedBindings.Add(name, new BindingModel<T>() { Name = name, Source = source, GetLeftValue = getValue, SetLeftValue = setValue });
            source.PropertyChanged += this.OnSourceOnPropertyChanged;
            this.OnSourceOnPropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void OnSourceOnPropertyChanged(object s, PropertyChangedEventArgs e)
        {
            IExecuter model;
            if (this.namedBindings.TryGetValue(e.PropertyName, out model))
            {
                if (e.PropertyName == model.Name)
                {
                    model.Execute();
                }
            }
        }

        public void UnbindAll()
        {
            foreach (var pair in this.namedBindings)
            {
                this.Unbind(pair.Key);
            }

            this.namedBindings.Clear();
        }

        public void Unbind(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            IExecuter executer;
            if (this.namedBindings.TryGetValue(name, out executer))
            {
                executer.Source.PropertyChanged -= this.OnSourceOnPropertyChanged;
                this.namedBindings.Remove(name);
            }
        }
    }
}
