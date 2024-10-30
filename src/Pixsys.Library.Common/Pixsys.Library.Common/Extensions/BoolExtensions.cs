// -----------------------------------------------------------------------
// <copyright file="BoolExtensions.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Pixsys.Library.Common.Extensions
{
    /// <summary>
    /// <see cref="bool"/> extensions.
    /// </summary>
    public static class BoolExtensions
    {
        /// <summary>
        /// Converts a boolean to an integer.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns>The converted <see cref="int"/>.</returns>
        public static int ToInt(this bool value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Parses a string to bool.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The converted <see cref="bool"/>.</returns>
        /// <exception cref="FormatException">Impossible to parse current value.</exception>
        public static bool ParseToBool(this string value)
        {
            return !bool.TryParse(value, out bool result)
                ? throw new FormatException("Impossible to parse current value into bool:" + value)
                : result;
        }
    }
}