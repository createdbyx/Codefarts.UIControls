namespace Codefarts.UIControls
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides a class for pushing and pulling
    /// </summary>
    public sealed class StackPoolFetching<T> : IDisposable where T : new()
    {
        /// <summary>
        /// Stores a reference to the stack we are
        /// </summary>  
        private readonly Stack<T> stackReference;

        /// <summary>
        /// Gets a value indicating whether or not to use locks when pulling or pushing items from the stack.
        /// </summary>                      
        /// <remarks></remarks>
        public bool LockStack { get; private set; }

        /// <summary>
        /// Gets the pool item that was created or fetched from the stack.
        /// </summary>
        public T PoolItem { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StackPoolFetching{T}" /> class.
        /// </summary>
        /// <param name="stack">A reference to the stack that stores the items.</param>
        /// <param name="lockStack">If set to <c>true</c> the <see cref="stack"/> will be locked before pulling or pushing items.</param>
        /// <exception cref="System.ArgumentNullException">stack</exception>
        public StackPoolFetching(Stack<T> stack, bool lockStack)
        {
            if (stack == null)
            {
                throw new ArgumentNullException("stack");
            }

            // store varibles
            this.stackReference = stack;
            this.LockStack = lockStack;

            // fetch or create a new item
            T args;
            if (lockStack)
            {
                lock (stack)
                {
                    args = stack.Count > 0 ? stack.Pop() : new T();
                }
            }
            else
            {
                args = stack.Count > 0 ? stack.Pop() : new T();
            }

            this.PoolItem = args;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Push back onto stack to be reused at a later time.
            if (this.LockStack)
            {
                lock (this.stackReference)
                {
                    this.stackReference.Push(this.PoolItem);
                }
            }
            else
            {
                this.stackReference.Push(this.PoolItem);
            }
        }
    }
}