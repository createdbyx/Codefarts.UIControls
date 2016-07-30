namespace Codefarts.UIControls.Models
{
    using System;                      

    /// <summary>
    /// Provides a result class for reporting result data.
    /// </summary>
    /// <typeparam name="T">The type used for the result data.</typeparam>
    public class ResultModel<T>
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultModel{T}"/> class.
        /// </summary>
        public ResultModel()
        {   
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultModel{T}"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public ResultModel(T result)
        {
            this.Result = result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultModel{T}"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public ResultModel(Exception exception)
        {
            this.Exception = exception;
        }

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
    }
}