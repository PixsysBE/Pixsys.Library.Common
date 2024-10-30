// -----------------------------------------------------------------------
// <copyright file="FolderUtilities.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

namespace Pixsys.Library.Common.Utilities
{
    /// <summary>
    /// Folder utilities.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "Reviewed.")]
    public static class FolderUtilities
    {
        /// <summary>
        /// Gets the size of the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The size in GB/MB/KB.</returns>
        public static (double GB, double MB, double KB) GetDirectorySize(string path)
        {
            long sizeInBytes = GetDirectorySizeInBytes(path);
            double kb = sizeInBytes / 1024.0;
            double mb = kb / 1024.0;
            double gb = mb / 1024.0;
            return (gb, mb, kb);
        }

        /// <summary>
        /// Checks if the folder exists and creates it if this is not the case.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        public static void CheckFolderExists(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                _ = Directory.CreateDirectory(folderPath);
            }
        }

        /// <summary>
        /// Gets the directory size in bytes.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The directory size.</returns>
        /// <exception cref="DirectoryNotFoundException">Directory not found.</exception>
        private static long GetDirectorySizeInBytes(string path)
        {
            // Check if the directory exists
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException("Directory not found: " + path);
            }

            long directorySize = 0;

            // Calculate the size of each file and add it to the directorySize
            foreach (string file in Directory.GetFiles(path))
            {
                FileInfo fileInfo = new(file);
                directorySize += fileInfo.Length;
            }

            // Recursively calculate the size of each subdirectory and add it to directorySize
            foreach (string subdirectory in Directory.GetDirectories(path))
            {
                directorySize += GetDirectorySizeInBytes(subdirectory);
            }

            return directorySize;
        }
    }
}