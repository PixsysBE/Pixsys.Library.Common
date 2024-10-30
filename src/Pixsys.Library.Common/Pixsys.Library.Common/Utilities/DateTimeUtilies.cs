// -----------------------------------------------------------------------
// <copyright file="DateTimeUtilies.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Pixsys.Library.Common.Utilities
{
    /// <summary>
    /// <see cref="DateTime"/> utilities.
    /// </summary>
    public static class DateTimeUtilies
    {
        /// <summary>
        /// Calculates the date difference.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The number of years, months and days.</returns>
        public static (int Years, int Months, int Days) CalculateDateDifference(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                // Invert dates if endDate is earlier than startDate to avoid negative values.
                (endDate, startDate) = (startDate, endDate);
            }

            // Calculate the initial difference in years, months, and days.
            int years = endDate.Year - startDate.Year;
            int months = endDate.Month - startDate.Month;
            int days = endDate.Day - startDate.Day;

            // Adjust the months and years if necessary.
            if (days < 0)
            {
                // Borrow one month and recalculate the days difference.
                months--;
                DateTime previousMonth = endDate.AddMonths(-1);
                days += DateTime.DaysInMonth(previousMonth.Year, previousMonth.Month);
            }

            if (months < 0)
            {
                // Borrow one year and adjust the months.
                years--;
                months += 12;
            }

            return (years, months, days);
        }
    }
}