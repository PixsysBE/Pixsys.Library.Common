// -----------------------------------------------------------------------
// <copyright file="DecimalExtensions.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;

namespace Pixsys.Library.Common.Extensions
{
    /// <summary>
    /// <see cref="decimal"/> extensions.
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// Parses string to decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The parsed decimal.</returns>
        /// <exception cref="FormatException">Impossible to parse current value.</exception>
        public static decimal ParseToDecimal(this string value)
        {
            /*
             * Values currently supported:
             * 9000
             * 9,000
             * 9,000.00
             * 9 000
             * 9 000,00
             * 9 000.00
             */

            // Initial cleanup: Remove all types of spaces (including regular and non-breaking spaces)
            value = new string(value.Where(c => !char.IsWhiteSpace(c)).ToArray());

            // Try parsing with common cultures
            if (decimal.TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out decimal result) ||
                decimal.TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.GetCultureInfo("en-US"), out result) ||
                decimal.TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out result))
            {
                return result;
            }

            // Handle mixed formats (e.g., "9,000.00" or "9.000,00")
            if (value.Contains(',') && value.Contains('.'))
            {
                // If the comma is before the dot, assume the comma is a thousands separator
                if (value.IndexOf(',') < value.IndexOf('.'))
                {
                    value = value.Replace(",", string.Empty);
                }
                else
                {
                    // If the dot is before the comma, assume the dot is a thousands separator
                    value = value.Replace(".", string.Empty).Replace(",", ".");
                }
            }
            else if (value.Contains(','))
            {
                // If there are only commas, replace them with dots to consider them as decimal separators
                value = value.Replace(",", ".");
            }

            // Final attempt to parse after modifications
            if (decimal.TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out result))
            {
                return result;
            }

            // If no conversion succeeded, throw a more specific exception
            throw new FormatException($"Impossible to parse current value into decimal: {value}");
        }
    }
}