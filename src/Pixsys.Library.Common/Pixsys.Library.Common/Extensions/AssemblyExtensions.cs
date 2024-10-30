// -----------------------------------------------------------------------
// <copyright file="AssemblyExtensions.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace Pixsys.Library.Common.Extensions
{
    /// <summary>
    /// <see cref="Assembly"/> extensions.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "Reviewed.")]
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Reads the embedded resource.
        /// </summary>
        /// <remarks>
        /// <see href="https://stackoverflow.com/questions/3314140/how-to-read-embedded-resource-text-file">https://stackoverflow.com/questions/3314140/how-to-read-embedded-resource-text-file</see>.
        /// </remarks>
        /// <param name="assembly">The assembly.</param>
        /// <param name="name">The name.</param>
        /// <returns>The embedded resource text.</returns>
        /// <exception cref="ArgumentException">Resource not found in assembly.</exception>
        public static string ReadEmbeddedResource(this Assembly assembly, string name)
        {
            string? resourcePath = assembly.GetManifestResourceNames()
                .FirstOrDefault(str => str.EndsWith(name, StringComparison.OrdinalIgnoreCase)) ?? throw new ArgumentException($"Resource with name {name} has not been found in assembly.");
            using Stream? stream = assembly.GetManifestResourceStream(resourcePath);
            if (stream == null)
            {
                return string.Empty;
            }

            using StreamReader reader = new(stream, Encoding.UTF8);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// Gets non-abstract sub classes of type T in an assembly.
        /// </summary>
        /// <typeparam name="T">The generic object.</typeparam>
        /// <param name="assembly">The assembly.</param>
        /// <param name="serviceProvider">[Nullable]The service provider.</param>
        /// <returns>The list of generic objects.</returns>
        public static IEnumerable<T> GetSubClassesOfType<T>(this Assembly assembly, IServiceProvider serviceProvider)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(T)) && !type.IsAbstract)
                {
                    if (serviceProvider is null)
                    {
                        object? instance = Activator.CreateInstance(type);
                        if (instance != null)
                        {
                            yield return (T)instance;
                        }
                    }
                    else
                    {
                        yield return (T)ActivatorUtilities.CreateInstance(serviceProvider, type);
                    }
                }
            }
        }
    }
}