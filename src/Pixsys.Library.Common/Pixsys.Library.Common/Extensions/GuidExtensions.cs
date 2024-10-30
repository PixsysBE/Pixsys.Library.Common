// -----------------------------------------------------------------------
// <copyright file="GuidExtensions.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Pixsys.Library.Common.Extensions
{
    /// <summary>
    /// <see cref="Guid"/> extensions.
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Converts a guid to string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The converted guid as a string.</returns>
        public static string ToGuidString(this Guid value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Parses a string to unique identifier.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The converted guid.</returns>
        /// <exception cref="FormatException">Impossible to parse current value.</exception>
        public static Guid ParseToGuid(this string value)
        {
            value = value.Replace(" ", string.Empty).Replace(",", string.Empty).Replace(" ", string.Empty);
            return !Guid.TryParse(value, out Guid result)
                ? throw new FormatException("Impossible to parse current value into Guid:" + value)
                : result;
        }
    }
}