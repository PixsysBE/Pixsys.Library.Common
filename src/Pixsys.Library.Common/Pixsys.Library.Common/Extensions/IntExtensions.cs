// -----------------------------------------------------------------------
// <copyright file="IntExtensions.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;

namespace Pixsys.Library.Common.Extensions
{
    /// <summary>
    /// <see cref="int"/> extensions.
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// Converts int to string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted int as a string.</returns>
        public static string ToIntString(this int value, CultureInfo culture)
        {
            return value.ToString("N0", culture.NumberFormat);
        }

        /// <summary>
        /// Parses string to int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The converted int.</returns>
        /// <exception cref="FormatException">Impossible to parse current value.</exception>
        public static int ParseToInt(this string value)
        {
            // Remove all spaces, commas, and non-breaking spaces to clean up the input
            value = value.Replace(" ", string.Empty).Replace(",", string.Empty).Replace(" ", string.Empty);

            // Try parsing using the current culture settings
            if (!int.TryParse(value, NumberStyles.Integer, CultureInfo.CurrentCulture, out int result) &&

                // If that fails, try parsing using US English culture
                !int.TryParse(value, NumberStyles.Integer, CultureInfo.GetCultureInfo("en-US"), out result) &&

                // If that also fails, try parsing using the invariant culture
                !int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
            {
                // If all parsing attempts fail, throw a specific exception indicating the error
                throw new FormatException($"Impossible to parse current value into Int32: {value}");
            }

            // Return the successfully parsed integer result
            return result;
        }
    }
}