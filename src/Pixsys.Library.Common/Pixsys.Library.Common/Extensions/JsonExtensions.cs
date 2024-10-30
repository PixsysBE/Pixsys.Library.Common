// -----------------------------------------------------------------------
// <copyright file="JsonExtensions.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;

namespace Pixsys.Library.Common.Extensions
{
    /// <summary>
    /// Json extensions.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "Reviewed.")]
    public static class JsonExtensions
    {
        private static readonly JsonSerializerOptions SerializerIndented = new()
        {
            WriteIndented = true,
        };

        /// <summary>
        /// Serializes <paramref name="obj"/> to a json string using a <see cref="DataContractJsonSerializer"/>.
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="obj"/>.</typeparam><param name="obj">The CLR object to serialize.</param><param name="encoding">Optional encoding of the JSON text. When <c>null</c> UTF8 is used.</param>
        /// <returns>
        /// The JSON representation of <paramref name="obj"/>.
        /// </returns>
        public static string? SerializeToJsonString<T>(this T obj, Encoding? encoding = null)
        {
            return obj?.ToJson(typeof(T), encoding);
        }

        /// <summary>
        /// Deserializes <paramref name="json"/> to an generic object using a <see cref="DataContractJsonSerializer"/>.
        /// </summary>
        /// <typeparam name="T">The type of the serialized object.</typeparam><param name="json">The JSON representation of a serialized object.</param><param name="encoding">Optional encoding of the JSON text. When <c>null</c> UTF8 is used.</param>
        /// <returns>
        /// The deserialized CLR object.
        /// </returns>
        public static T? DeserializeJsonToObject<T>(this string json, Encoding? encoding = null)
        {
            return (T?)FromJson(json, typeof(T), encoding);
        }

        /// <summary>
        /// Converts an enumeration to JSON string.
        /// </summary>
        /// <param name="enumobj">The enum.</param>
        /// <returns>
        /// A JSON string representing the enum.
        /// </returns>
        public static string ToJson(this Enum enumobj)
        {
            string str1 = Enum.GetNames(enumobj.GetType()).Aggregate("{", (current, str2) => current + (current == "{" ? string.Empty : ", ") + str2 + ":\"" + str2 + "\"");
            return str1 + "}";
        }

        /// <summary>
        /// Converts an enumeration to JSON string containing the values.
        /// </summary>
        /// <param name="enumobj">The enum.</param>
        /// <param name="formatProvider">The forma provider.</param>
        /// <returns>
        /// A JSON string representing the enum values.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="enumobj"/> is <c>null</c>.</exception>
        public static string ToJsonValues(this Enum enumobj, IFormatProvider? formatProvider)
        {
            if (enumobj == null)
            {
                throw new ArgumentNullException(nameof(enumobj), "Enum object cannot be null.");
            }

            Type enumType = enumobj.GetType();
            Array values = Enum.GetValues(enumType);
            List<string> jsonEntries = new(values.Length);
            StringBuilder sb = new("{");

            foreach (object? value in values)
            {
                string? name = Enum.GetName(enumType, value);
                if (name != null)
                {
                    // Add each entry as "Name": "Value"
                    jsonEntries.Add($"\"{name}\": \"{Convert.ToInt32(value, formatProvider)}\"");
                }
            }

            // Join entries with a comma and add to the StringBuilder
            _ = sb.AppendJoin(", ", jsonEntries);
            _ = sb.Append('}');

            return sb.ToString();
        }

        /// <summary>
        /// Pretty print the json input.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>A prettier output.</returns>
        public static string JsonPrettyPrint(this string json)
        {
            JsonElement doc = JsonDocument.Parse(json).RootElement;
            return JsonSerializer.Serialize(doc, SerializerIndented);
        }

        /// <summary>
        /// Serializes <paramref name="obj"/> using a <see cref="DataContractJsonSerializer"/>.
        ///
        /// </summary>
        /// <param name="obj">The CLR object to serialize.</param><param name="type">The type of <paramref name="obj"/>.</param><param name="encoding">Optional encoding of the JSON text. When <c>null</c> UTF8 is used.</param>
        /// <returns>
        /// The JSON representation of <paramref name="obj"/>.
        /// </returns>
        private static string ToJson(this object obj, Type type, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;
            using MemoryStream memoryStream = new();
            new DataContractJsonSerializer(type).WriteObject(memoryStream, obj);
            return encoding.GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        /// <summary>
        /// Deserializes <paramref name="json"/> using a <see cref="DataContractJsonSerializer"/>.
        ///
        /// </summary>
        /// <param name="json">The JSON representation of a serialized object.</param><param name="type">The type of the serialized object.</param><param name="encoding">Optional encoding of the JSON text. When <c>null</c> UTF8 is used.</param>
        /// <returns>
        /// The deserialized CLR object.
        /// </returns>
        private static object? FromJson(string json, Type type, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;
            using MemoryStream memoryStream = new(encoding.GetBytes(json));
            if (memoryStream.Length > 0)
            {
                DataContractJsonSerializer serializer = new(type);
                return type is null ? null : serializer.ReadObject(memoryStream);
            }

            return null;
        }
    }
}