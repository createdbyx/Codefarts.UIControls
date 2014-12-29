namespace Codefarts.UIControls
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides a class for storing and retrieving named values.
    /// </summary>
    /// <remarks>This class is used by <see cref="Control"/>.</remarks>
    [Serializable]
    public class PropertyCollection
    {
        /// <summary>
        /// Stores the names of the stored values.
        /// </summary>
        private string[] names;

        /// <summary>
        /// Stores the values of the properties.
        /// </summary>
        private object[] values;

        /// <summary>
        /// The countStores how many items have been stored.
        /// </summary>
        private int count;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyCollection"/> class.
        /// </summary>
        public PropertyCollection()
        {
            this.names = new string[128];
            this.values = new object[128];
        }

        /// <summary>
        /// Gets the specified value for name.
        /// </summary>
        /// <param name="name">The name to retrieve the value for.</param>
        /// <returns>The value assigned to 'name'.</returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">If name could not be found.</exception>
        public object Get(string name)
        {
            var index = Array.IndexOf(this.names, name);
            if (index == -1)
            {
                throw new KeyNotFoundException();
            }

            return this.values[index];
        }                                   

        public void Add(string name, object value)
        {
            if (this.names.Length == this.count)
            {
                Array.Resize(ref this.names, this.names.Length * 2);
                Array.Resize(ref this.values, this.values.Length * 2);
            }

            this.names[this.count] = name;
            this.values[this.count] = value;
            this.count++;
        }

        /// <summary>
        /// Determines whether the specified name already exists.
        /// </summary>
        /// <param name="name">The name to check for.</param>
        /// <returns>true if the name is present.</returns>
        public bool ContainsName(string name)
        {
            return Array.IndexOf(this.names, name) != -1;
        }

        public string[] GetNames()
        {
            var tempArray = new string[this.count];
            Array.Copy(this.names, 0, tempArray, 0, this.count);
            return tempArray;
        }

        public bool Remove(string name)
        {
            var index = Array.IndexOf(this.names, name);
            if (index == -1)
            {
                return false;
            }

            return this.Remove(index);
        }

        public bool Remove(int index)
        {
            if ((index < 0 && index > this.count - 1) || this.count == 0)
            {
                return false;
            }

            Array.Copy(this.names, index + 1, this.names, index, this.count - index);
            Array.Copy(this.values, index + 1, this.values, index, this.count - index);

            this.count--;
            this.names[this.count] = null;
            this.values[this.count] = null;

            return true;
        }

        /// <summary>
        /// Gets the number of stored values.
        /// </summary>  
        public int Count
        {
            get
            {
                return this.count;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The stored <see cref="System.Object"/>.</returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">
        /// If <see cref="name"/> is not found when retrieving the value.
        /// </exception>
        /// <remarks>If <see cref="name"/> is not found it will be added and it's value assigned.</remarks>
        public object this[string name]
        {
            get
            {
                var index = Array.IndexOf(this.names, name);
                if (index == -1)
                {
                    throw new KeyNotFoundException();
                }

                return this.values[index];
            }

            set
            {
                var index = Array.IndexOf(this.names, name);
                if (index == -1)
                {
                    this.Add(name, value);
                    return;
                }

                this.values[index] = value;
            }
        }
    }
}
