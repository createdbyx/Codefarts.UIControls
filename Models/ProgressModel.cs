namespace Codefarts.UIControls.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides a progress class for reporting progress data.
    /// </summary>
    /// <typeparam name="T">The type used for the result data.</typeparam>
    public class ProgressModel<T>
    {
        /// <summary>
        /// Gets the results queue.
        /// </summary>
        public Queue<T> Results { get; private set; }

        /// <summary>
        /// Gets the message queue.
        /// </summary>
        public Queue<string> Messages { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is completed.
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// Flags the response model as canceled.
        /// </summary>
        public void Cancel()
        {
            this.IsCanceled = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressModel{T}"/> class.
        /// </summary>
        public ProgressModel()
        {
            this.Results = new Queue<T>();
            this.Messages = new Queue<string>();
        }

        /// <summary>
        /// Gets a value indicating whether this instance is canceled.
        /// </summary>
        public bool IsCanceled { get; private set; }

        /// <summary>
        /// Gets or sets the progress.
        /// </summary>
        public float Progress { get; set; }

        /// <summary>
        /// Gets or sets the information that may have been thrown.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is faulted.
        /// </summary>
        public bool IsFaulted
        {
            get
            {
                return this.Exception != null;
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }
    }
}