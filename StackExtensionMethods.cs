namespace Codefarts.UIControls
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides various extension methods for the generic <see cref="Stack{T}"/> type.
    /// </summary>
    public static class StackExtensionMethods
    {
        /// <summary>
        /// Gets a new reference to a <see cref="StackPoolFetching{T}"/> type.
        /// </summary>
        /// <typeparam name="T">The type that is store in the stack.</typeparam>
        /// <param name="stack">The stack used to push and pull pooled items from.</param>
        /// <returns>A new reference to a <see cref="StackPoolFetching{T}"/> object that will handle fetching and storing items automatically.</returns>
        public static StackPoolFetching<T> GetFetcher<T>(this Stack<T> stack) where T : new()
        {
            return new StackPoolFetching<T>(stack, false);
        }

        /// <summary>
        /// Gets a new reference to a <see cref="StackPoolFetching{T}"/> type.
        /// </summary>
        /// <typeparam name="T">The type that is store in the stack.</typeparam>
        /// <param name="stack">The stack used to push and pull pooled items from.</param>
        /// <param name="lockStack">If set to <c>true</c> the <see cref="stack"/> will be locked before pulling or pushing items.</param>
        /// <returns>A new reference to a <see cref="StackPoolFetching{T}"/> object that will handle fetching and storing items automatically.</returns>
        public static StackPoolFetching<T> GetFetcher<T>(this Stack<T> stack, bool lockStack) where T : new()
        {
            return new StackPoolFetching<T>(stack, lockStack);
        }
    }
}