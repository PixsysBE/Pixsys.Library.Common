// -----------------------------------------------------------------------
// <copyright file="ListIntExtensions.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Pixsys.Library.Common.Extensions
{
    /// <summary>
    /// List int extensions.
    /// </summary>
    public static class ListIntExtensions
    {
        /// <summary>
        /// Get the closest element from a value in a list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="target">The target.</param>
        /// <returns>The closest value.</returns>
        public static int ClosestValueFrom(this List<int> list, int target)
        {
            // NB Method will return int.MaxValue for a sequence containing no elements.
            // Apply any defensive coding here as necessary.
            int closest = int.MaxValue;
            int minDifference = int.MaxValue;
            foreach (int element in list)
            {
                long difference = Math.Abs((long)element - target);
                if (minDifference > difference)
                {
                    minDifference = (int)difference;
                    closest = element;
                }
            }

            return closest;
        }

        /// <summary>
        /// Create groups of IEnumerable int containing consecutive elements from the source.
        /// </summary>
        /// <param name="iterable">The iterable.</param>
        /// <param name="ordering">The ordering.</param>
        /// <remarks>
        /// Example : [[1, 2, 3], [5, 6], [10]].
        /// </remarks>
        /// <returns>The sequences of consecutive numbers.</returns>
        public static IEnumerable<IEnumerable<int>> GroupConsecutive(this IEnumerable<int> iterable, Func<int, int>? ordering = null)
        {
            ordering ??= n => n;
            foreach (IGrouping<int, (int e, int i)> tg in iterable
                                 .Select((e, i) => (e, i))
                                 .GroupBy(t => t.i - ordering(t.e)))
            {
                yield return tg.Select(t => t.e);
            }
        }
    }
}