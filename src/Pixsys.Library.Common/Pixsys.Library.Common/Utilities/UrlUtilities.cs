// -----------------------------------------------------------------------
// <copyright file="UrlUtilities.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

namespace Pixsys.Library.Common.Utilities
{
    /// <summary>
    /// Url utilities.
    /// </summary>
    public static class UrlUtilities
    {
        /// <summary>
        /// Gets the response from URL asynchronously.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The response.</returns>
        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "Reviewed.")]
        public static async Task<string> GetResponseFromUrlAsync(string url)
        {
            using HttpClient client = new();
            return await client.GetStringAsync(url);
        }
    }
}