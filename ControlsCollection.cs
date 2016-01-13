// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlsCollection.cs" company="">
//   
// </copyright>
// <summary>
//   The controls collection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Codefarts.UIControls
{
    using System;
    using System.Collections.ObjectModel;

    /// <summary>
    /// The controls collection.
    /// </summary>
    public class ControlsCollection : ObservableCollection<Control>
    {

        /// <summary>
        /// The control that owns this collection.
        /// </summary>
        private Control owner;

        /// <summary>Inserts an item into the collection at the specified index.</summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert.</param>
        /// <remarks>
        /// <para>If the control has already been added it will be sent to the back.</para>
        /// <para>If the control being added has a parent assigned it will remove itself from the parent controls <see cref="Control.Controls"/> collection before setting the parent.</para>
        /// </remarks>
        protected override void InsertItem(int index, Control item)
        {
            if (item.Parent == this.owner)
            {
                item.SendToBack();
                return;
            }

            if (item.Parent != null)
            {
                item.Parent.Controls.Remove(item);
            }

            base.InsertItem(index, item);
            item.AssignParent(this.owner);
        }

        /// <summary>Removes the item at the specified index of the collection.</summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        protected override void RemoveItem(int index)
        {
            var item = this[index];
            if (item.Parent == this.owner)
            {
                base.RemoveItem(index);
                item.AssignParent(null);
            }
        }

        /// <summary>
        /// Sets the index of the child.
        /// </summary>
        /// <param name="child">The control that is already stored in this collection.</param>
        /// <param name="newIndex">The new index location where the child should be stored.</param>
        public virtual void SetChildIndex(Control child, int newIndex)
        {
            this.SetChildIndexInternal(child, newIndex);
        }

        /// <summary>
        /// Sets the index of the child.
        /// </summary>
        /// <param name="child">The control that is already stored in this collection.</param>
        /// <param name="newIndex">The new index location where the child should be stored.</param>
        internal virtual void SetChildIndexInternal(Control child, int newIndex)
        {
            if (child == null)
            {
                throw new ArgumentNullException("child");
            }

            var childIndex = this.GetChildIndex(child);
            if (childIndex == newIndex)
            {
                return;
            }

            if (newIndex >= this.Count || newIndex == -1)
            {
                newIndex = this.Count - 1;
            }

            this.MoveElement(child, childIndex, newIndex);
        }

        internal void MoveElement(Control element, int fromIndex, int toIndex)
        {
            int num = toIndex - fromIndex;
            if (num == -1 || num == 1)
            {
                this[fromIndex] = this[toIndex];
            }
            else
            {
                int num1 = 0;
                int num2 = 0;
                if (num <= 0)
                {
                    num1 = toIndex;
                    num2 = toIndex + 1;
                    num = -num;
                }
                else
                {
                    num1 = fromIndex + 1;
                    num2 = fromIndex;
                }

                this.Copy(this, num1, this, num2, num);
            }
            this[toIndex] = element;  
        }

        private void Copy(ControlsCollection sourceList, int sourceIndex, ControlsCollection destinationList, int destinationIndex, int length)
        {
            if (sourceIndex >= destinationIndex)
            {
                while (length > 0)
                {
                    var innerList = destinationList;
                    var num = destinationIndex;
                    destinationIndex = num + 1;
                    var num1 = sourceIndex;
                    sourceIndex = num1 + 1;
                    innerList[num] = sourceList[num1];
                    length--;
                }

                return;
            }

            sourceIndex = sourceIndex + length;
            destinationIndex = destinationIndex + length;
            while (length > 0)
            {
                var item = destinationList;
                var num2 = destinationIndex - 1;
                destinationIndex = num2;
                var num3 = sourceIndex - 1;
                sourceIndex = num3;
                item[num2] = sourceList[num3];
                length--;
            }
        }

        public int GetChildIndex(Control child)
        {
            return this.GetChildIndex(child, true);
        }

        public virtual int GetChildIndex(Control child, bool throwException)
        {
            var num = this.IndexOf(child);
            if (num == -1 & throwException)
            {
                throw new ArgumentException("'child' is not a child control of this parent.");
            }

            return num;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlsCollection"/> class.
        /// </summary>
        /// <param name="owner">The owner control of the collection.</param>
        public ControlsCollection(Control owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// Gets the control owner of this collection.
        /// </summary>    
        public Control Owner
        {
            get
            {
                return this.owner;
            }
        }
    }
}
