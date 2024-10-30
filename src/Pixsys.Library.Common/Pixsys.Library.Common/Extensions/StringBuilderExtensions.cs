// -----------------------------------------------------------------------
// <copyright file="StringBuilderExtensions.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Pixsys.Library.Common.Extensions
{
    /// <summary>
    /// <see cref="StringBuilder"/> extensions.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "Reviewed.")]
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Copies the specified StringBuilder.
        /// </summary>
        /// <param name="sb">The StringBuilder.</param>
        /// <returns>The copied <see cref="StringBuilder"/>.</returns>
        public static StringBuilder Copy(this StringBuilder sb)
        {
            char[] originalChars = new char[sb.Length];
            sb.CopyTo(0, originalChars, 0, sb.Length);

            // Creates a new StringBuilder containing the char array elements.
            StringBuilder newBuilder = new(originalChars.Length);
            _ = newBuilder.Append(originalChars);
            return newBuilder;
        }
    }
}