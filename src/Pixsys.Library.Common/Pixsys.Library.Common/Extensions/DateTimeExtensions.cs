// -----------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Pixsys.Library.Common.Models;
using System.Globalization;

namespace Pixsys.Library.Common.Extensions
{
    /// <summary>
    /// <see cref="DateTime"/> extensions.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts to UTC.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The converted <see cref="DateTime"/>.</returns>
        public static DateTime ConvertToUtc(this DateTime date)
        {
            return TimeZoneInfo.ConvertTimeToUtc(date);
        }

        /// <summary>
        /// Converts from UTC.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The converted <see cref="DateTime"/>.</returns>
        public static DateTime ConvertFromUtc(this DateTime date)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.Local);
        }

        /// <summary>
        /// Parses string to date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="replaceBlanks">if set to <c>true</c> [replace blanks].</param>
        /// <returns>The converted <see cref="DateTime"/>.</returns>
        /// <exception cref="FormatException">Impossible to parse current value.</exception>
        public static DateTime ParseToDateTime(this string value, bool replaceBlanks = true)
        {
            if (replaceBlanks)
            {
                value = value.Trim().Replace(",", string.Empty);
            }

            // Try parsing in the current culture
            return !DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime result) &&

                // Then try in US english
                !DateTime.TryParse(value, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out result) &&

                // Then in neutral language
                !DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out result)
                ? throw new FormatException("Impossible to parse current value into DateTime:" + value)
                : result;
        }

        /// <summary>
        /// Gets the first day of week.
        /// </summary>
        /// <param name="sourceDateTime">The source date time.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The first day of the week.</returns>
        public static DateTime GetFirstDayOfWeek(this DateTime sourceDateTime, CultureInfo culture)
        {
            // Get the first day of the week from the provided culture
            DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;

            // Calculate the difference in days between the source date's DayOfWeek and the desired first day
            int delta = (sourceDateTime.DayOfWeek - firstDayOfWeek + 7) % 7;

            // Subtract the delta to get the first day of the week
            return sourceDateTime.AddDays(-delta);
        }

        /// <summary>
        /// Gets the last day of week.
        /// </summary>
        /// <param name="sourceDateTime">The source date time.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The last day of week.</returns>
        public static DateTime GetLastDayOfWeek(this DateTime sourceDateTime, CultureInfo culture)
        {
            // Get the first day of the week from the provided culture
            DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;

            // Calculate the last day of the week based on the first day
            DayOfWeek lastDayOfWeek = (DayOfWeek)(((int)firstDayOfWeek + 6) % 7);

            // Calculate the difference in days between the source date's DayOfWeek and the last day
            int delta = (lastDayOfWeek - sourceDateTime.DayOfWeek + 7) % 7;

            // Add the delta to get the last day of the week
            return sourceDateTime.AddDays(delta);
        }

        /// <summary>
        /// Returns the first day of the month based on the given date. The time is set to midnight (00:00:00).
        /// </summary>
        /// <param name="sourceDateTime">The source date time.</param>
        /// <returns>The first day of month.</returns>
        public static DateTime GetFirstDayOfMonth(this DateTime sourceDateTime)
        {
            return new DateTime(sourceDateTime.Year, sourceDateTime.Month, 1, 0, 0, 0, sourceDateTime.Kind);
        }

        /// <summary>
        /// Returns the last day of the month based on the given date.
        /// <para>If `endOfDay` is true, the time is set to 23:59:59 to represent the end of the day.
        /// Otherwise, the time is set to midnight (00:00:00).
        /// </para>
        /// </summary>
        /// <param name="sourceDateTime">The source date time.</param>
        /// <param name="endOfDay">if set to <c>true</c> [end of day].</param>
        /// <returns>The last day of the month.</returns>
        public static DateTime GetLastDayOfMonth(this DateTime sourceDateTime, bool endOfDay = false)
        {
            int lastDay = DateTime.DaysInMonth(sourceDateTime.Year, sourceDateTime.Month);
            return new DateTime(
                sourceDateTime.Year,
                sourceDateTime.Month,
                lastDay,
                endOfDay ? 23 : 0,
                endOfDay ? 59 : 0,
                endOfDay ? 59 : 0,
                sourceDateTime.Kind);
        }

        /// <summary>
        /// Returns the first day of the quarter based on the given date. The time is set to midnight (00:00:00).
        /// <para>Quarters are defined as:</para>
        /// <para>Q1: January - March.</para>
        /// <para>Q2: April - June.</para>
        /// <para>Q3: July - September.</para>
        /// <para>Q4: October - December.</para>
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The first day of the quarter.</returns>
        public static DateTime GetFirstDayOfQuarter(this DateTime date)
        {
            int quarterNumber = ((date.Month - 1) / 3) + 1;
            int firstMonthOfQuarter = ((quarterNumber - 1) * 3) + 1;
            return new DateTime(date.Year, firstMonthOfQuarter, 1, 0, 0, 0, date.Kind);
        }

        /// <summary>
        /// Returns the day with the time set to midnight (00:00:00) to represent the beginning of the day.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The converted date.</returns>
        public static DateTime GetBeginningOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }

        /// <summary>
        /// Returns the day with the time set to 23:59:59 to represent the end of the day.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The converted date.</returns>
        public static DateTime GetEndOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }

        /// <summary>
        /// Returns the first day of the week following the given date. It uses the specified culture to determine the first day of the week.
        /// </summary>
        /// <param name="sourceDateTime">The source date time.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The first day of the week.</returns>
        public static DateTime GetFirstDayOfNextWeek(this DateTime sourceDateTime, CultureInfo culture)
        {
            /*
             * Returns the first day of the week following the given date.
             * Uses the specified culture to determine the first day of the week.
             * For example, if the culture's first day is Monday, it will return the next Monday after the given date.
             */

            // Get the first day of the week from the provided culture
            DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;

            // Calculate the number of days to add to reach the next first day of the week
            int daysUntilNextWeek = ((int)firstDayOfWeek - (int)sourceDateTime.DayOfWeek + 7) % 7;

            // If the source day is already the first day of the week, move to the next week's first day
            daysUntilNextWeek = daysUntilNextWeek == 0 ? 7 : daysUntilNextWeek;

            // Add the calculated number of days to get to the first day of the next week
            return sourceDateTime.AddDays(daysUntilNextWeek);
        }

        /// <summary>
        /// Gets the labeled day from today.
        /// </summary>
        /// <remarks>
        /// <example>Yesterday, Today, Tomorrow,...</example>
        /// </remarks>
        /// <param name="dateTime">The date time.</param>
        /// <returns>The labeled day.</returns>
        public static string GetLabeledDayFromToday(this DateTime dateTime)
        {
            return (double)(double)(DateTime.Today - dateTime.Date).TotalDays switch
            {
                0 => "Today",
                1 => "Yesterday",
                -1 => "Tomorrow",
                _ => dateTime.ToShortDateString(),
            };
        }

        /// <summary>
        /// Converts to moment.js date.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>The converted moment.js date as a string.</returns>
        public static string ConvertToMomentJsDate(this DateTime dateTime)
        {
            return $"{dateTime:s}.000Z";
        }

        /// <summary>
        /// Converts to Javascript date UTC.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <remarks>
        /// <example>Example: Date.UTC(2021, 1, 20).</example>
        /// </remarks>
        /// <returns>The converted date as a string.</returns>
        public static string ConvertToJavascriptDateUtc(this DateTime dateTime)
        {
            return $"Date.UTC({dateTime.Year}, {dateTime.Month - 1}, {dateTime.Day})";
        }

        /// <summary>
        /// Calculates the difference between 2 dates.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The difference in years, months, days, hours, minutes and seconds.</returns>
        public static DateDifference CalculateDifference(this DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                (endDate, startDate) = (startDate, endDate);
            }

            // Calcul initial des années, mois et jours
            int years = endDate.Year - startDate.Year;
            int months = endDate.Month - startDate.Month;
            int days = endDate.Day - startDate.Day;

            // Ajuster les mois et années si nécessaire
            if (days < 0)
            {
                months--;
                days += DateTime.DaysInMonth(startDate.Year, startDate.Month);
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            // Calcul des heures, minutes et secondes
            TimeSpan timeDifference = endDate - startDate.AddYears(years).AddMonths(months).AddDays(days);

            return new DateDifference
            {
                Years = years,
                Months = months,
                Days = days,
                Hours = timeDifference.Hours,
                Minutes = timeDifference.Minutes,
                Seconds = timeDifference.Seconds,
            };
        }
    }
}