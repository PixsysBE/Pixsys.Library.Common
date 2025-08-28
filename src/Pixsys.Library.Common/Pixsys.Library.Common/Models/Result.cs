// -----------------------------------------------------------------------
// <copyright file="Result.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Pixsys.Library.Common.Models
{
    /// <summary>
    /// A generic result model.
    /// </summary>
    /// <typeparam name="T">The value Type.</typeparam>
    public sealed class Result<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        public Result()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="value">The value.</param>
        /// <param name="isSuccess">if set to <c>true</c> [is success].</param>
        public Result(string message, T value, bool isSuccess)
        {
            Message = message;
            Value = value;
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T? Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string? Message { get; set; }

        /// <summary>
        /// Sets the result as success.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result.</returns>
        public Result<T> Success(T? value)
        {
            Message = string.Empty;
            Value = value;
            IsSuccess = true;
            return this;
        }

        /// <summary>
        /// Sets the result as failure.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result.</returns>
        public Result<T> Failure(string message, T? value)
        {
            Message = message;
            Value = value;
            IsSuccess = false;
            return this;
        }
    }
}
