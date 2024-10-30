// -----------------------------------------------------------------------
// <copyright file="FileUtilities.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Pixsys.Library.Common.Extensions;

namespace Pixsys.Library.Common.Utilities
{
    /// <summary>
    /// File utilities.
    /// </summary>
    public static class FileUtilities
    {
        /// <summary>
        /// Gets a unique file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The unique file name.</returns>
        public static string GetUniqueFileName(string fileName)
        {
            return !string.IsNullOrWhiteSpace(fileName) ? $"{DateTime.Now:yyyyMMddHHmmss-}{GetCleanedFileName(fileName)}" : string.Empty;
        }

        /// <summary>
        /// Cleans the file name.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <returns>The cleaned file name.</returns>
        public static string GetCleanedFileName(string fileName)
        {
            return GetCleanedFileName(fileName, false);
        }

        /// <summary>
        /// Cleans the file name.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="cleanAccent">Sets if accent mmust be cleaned.</param>
        /// <returns>The cleaned file name.</returns>
        public static string GetCleanedFileName(string fileName, bool cleanAccent)
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                foreach (char invalidChar in Path.GetInvalidFileNameChars())
                {
                    if (fileName.Contains(invalidChar))
                    {
                        fileName = fileName.Replace(invalidChar.ToString(), string.Empty);
                    }
                }

                string extension = Path.GetExtension(fileName);
                fileName = Path.GetFileNameWithoutExtension(fileName).Replace(" ", "_").Replace(".", "_").Replace("'", "_").Replace("!", string.Empty);

                if (cleanAccent)
                {
                    fileName = fileName.Replace("é", "e")
                                       .Replace("è", "e")
                                       .Replace("ê", "e")
                                       .Replace("ë", "e")
                                       .Replace("à", "a")
                                       .Replace("â", "a")
                                       .Replace("î", "i")
                                       .Replace("ï", "i")
                                       .Replace("ù", "u")
                                       .Replace("ç", "c")
                                       .Replace("ñ", "n")
                                       .Replace("+", "-")
                                       .Replace("?", "-")
                                       .Replace("&", "-");
                }

                fileName = fileName.Truncate(200);

                return (fileName + extension).ToLower(System.Globalization.CultureInfo.CurrentCulture);
            }

            return string.Empty;
        }
    }
}