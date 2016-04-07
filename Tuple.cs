using System;
using System.Collections.Generic;
using System.Text;

namespace Codefarts.UIControls
{
    /// <summary>
    /// Represents a 2-tuple, or pair. 
    /// </summary>
    /// <typeparam name="T1">
    /// The type of the tuple's first component.
    /// </typeparam>
    /// <typeparam name="T2">
    /// The type of the tuple's second component.
    /// </typeparam>
    /// <filterpriority>2</filterpriority>
    [Serializable]
    public class Tuple<T1, T2> : IComparable
    {
        /// <summary>
        /// The m_ item 1.
        /// </summary>
        private readonly T1 m_Item1;

        /// <summary>
        /// The m_ item 2.
        /// </summary>
        private readonly T2 m_Item2;

        /// <summary>Gets the value of the current <see cref="T:System.Tuple`2" /> object's first component.</summary>
        /// <returns>The value of the current <see cref="T:System.Tuple`2" /> object's first component.</returns>
        public T1 Item1
        {
            get
            {
                return this.m_Item1;
            }
        }

        /// <summary>Gets the value of the current <see cref="T:System.Tuple`2" /> object's second component.</summary>
        /// <returns>The value of the current <see cref="T:System.Tuple`2" /> object's second component.</returns>
        public T2 Item2
        {
            get
            {
                return this.m_Item2;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tuple{T1,T2}"/> class. 
        /// Initializes a new instance of the <see cref="T:System.Tuple`2"/> class.
        /// </summary>
        /// <param name="item1">
        /// The value of the tuple's first component.
        /// </param>
        /// <param name="item2">
        /// The value of the tuple's second component.
        /// </param>
        public Tuple(T1 item1, T2 item2)
        {
            this.m_Item1 = item1;
            this.m_Item2 = item2;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="sb">
        /// The sb.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string ToString(StringBuilder sb)
        {
            sb.Append(this.m_Item1);
            sb.Append(", ");
            sb.Append(this.m_Item2);
            sb.Append(")");
            return sb.ToString();
        }

        /// <summary>Returns a string that represents the value of this <see cref="T:System.Tuple`2" /> instance.</summary>
        /// <returns>The string representation of this <see cref="T:System.Tuple`2" /> object.</returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("(");
            return this.ToString(stringBuilder);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj"/> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj"/>. Greater than zero This instance follows <paramref name="obj"/> in the sort order. 
        /// </returns>
        /// <param name="obj">
        /// An object to compare with this instance. 
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="obj"/> is not the same type as this instance. 
        /// </exception>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            var tuple = obj as Tuple<T1, T2>;
            if (tuple == null)
            {
                throw new ArgumentException(string.Format("Argument must be of type {0}.", new object[] { this.GetType().ToString() }), "obj");
            }

            return Comparer<object>.Default.Compare(this.m_Item1, tuple.m_Item1);   
        }
    }
}