// -----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Pixsys.Library.Common.Extensions
{
    /// <summary>
    /// <see cref="string"/> extensions.
    /// </summary>
    [SuppressMessage("Performance", "CA1865:Use char overload", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "Reviewed.")]
    public static partial class StringExtensions
    {
        /// <summary>
        /// Truncates text to a specific length.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="length">The length.</param>
        /// <returns>The truncated text.</returns>
        public static string Truncate(this string text, int length)
        {
            return text.Truncate(length, string.Empty);
        }

        /// <summary>
        /// Truncates text to a specific length and adds suffix to the output if the text is longer than the specified length.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="length">The length.</param>
        /// <param name="suffix">The suffix to add to the output.</param>
        /// <returns>The truncated text.</returns>
        [SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Reviewed.")]
        public static string Truncate(this string text, int length, string suffix)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            return text.Length < length ? text : string.Concat(text.AsSpan(0, length), suffix);
        }

        /// <summary>
        /// Replaces the last occurrence.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="toReplace">The text to replace.</param>
        /// <param name="replacement">The replacement text.</param>
        /// <returns>The replaced text.</returns>
        public static string ReplaceLastOccurrence(this string source, string toReplace, string replacement)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(toReplace))
            {
                return source; // Return the original string if it's null or empty
            }

            // Find the last occurrence of the substring
            int lastIndex = source.LastIndexOf(toReplace, StringComparison.OrdinalIgnoreCase);

            if (lastIndex == -1)
            {
                return source; // Return the original string if the substring is not found
            }

            // Replace the last occurrence
            return source.Remove(lastIndex, toReplace.Length).Insert(lastIndex, replacement);
        }

        /// <summary>
        /// Replaces the NTH occurrence.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="toReplace">To replace.</param>
        /// <param name="replacement">The replacement.</param>
        /// <param name="occurrence">The occurrence.</param>
        /// <returns>The replaced text.</returns>
        public static string ReplaceNthOccurrence(this string source, string toReplace, string replacement, int occurrence)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(toReplace) || occurrence <= 0)
            {
                return source; // Return the original string if input is invalid
            }

            int count = 0; // Initialize a counter to track occurrences
            int currentIndex = -1; // Start with -1 to find the first occurrence

            // Loop until we find the desired occurrence
            while (count < occurrence)
            {
                currentIndex = source.IndexOf(toReplace, currentIndex + 1, StringComparison.OrdinalIgnoreCase);
                if (currentIndex == -1)
                {
                    return source; // Return original string if the occurrence is not found
                }

                count++; // Increment the count for each found occurrence
            }

            // Replace the n-th occurrence
            return source.Remove(currentIndex, toReplace.Length).Insert(currentIndex, replacement);
        }

        /// <summary>
        /// Removes the curly brackets of a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="replaceBy">The replace by.</param>
        /// <returns>The replaced text.</returns>
        public static string RemoveCurlyBrackets(this string value, string replaceBy = "")
        {
            return value.RemoveTextBetweenBrackets('{', '}', replaceBy);
        }

        /// <summary>
        /// Removes the text between brackets.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="openingBracket">The opening bracket.</param>
        /// <param name="closingBracket">The closing bracket.</param>
        /// <param name="replaceBy">The replace by.</param>
        /// <returns>The modified text.</returns>
        public static string RemoveTextBetweenBrackets(this string input, char openingBracket = '[', char closingBracket = ']', string replaceBy = "")
        {
            string pattern = $@"{Regex.Escape(openingBracket.ToString())}.*?\{Regex.Escape(closingBracket.ToString())}";
            return Regex.Replace(input, pattern, replaceBy);
        }

        /// <summary>
        /// Removes the diacritics.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The modified text.</returns>
        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new();
            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    _ = stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Converts to title case. Titlecasing refers to a casing practice wherein the first letter of a word is an uppercase letter
        /// and the rest of the letters are lowercase.
        /// </summary>
        /// <remarks>
        ///  All major words are capitalized, while minor words are lowercased. A simple example would be: Lord of the Flies.
        ///  </remarks>
        /// <param name="text">The text.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The modified text.</returns>
        public static string ToTitleCase(this string text, CultureInfo culture)
        {
            return culture.TextInfo.ToTitleCase(text);
        }

        /// <summary>
        /// Removes the special characters.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The modified text.</returns>
        public static string RemoveSpecialCharacters(this string text)
        {
            return specialCharactersRegex().Replace(text, string.Empty);
        }

        /// <summary>
        /// Validates the query string input.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The validated text.</returns>
        public static string ValidateQueryStringInput(this string text)
        {
            return System.Text.Encodings.Web.HtmlEncoder.Default.Encode(text);
        }

        /// <summary>
        /// Removes the HTML tags.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>The modified text.</returns>
        public static string RemoveHtmlTags(this string content)
        {
            string[] split = HtmlTagsRegex().Split(content);
            return split.Aggregate(string.Empty, (current, item) => current + item);
        }

        /// <summary>
        /// Truncates at word HTML content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="length">The length.</param>
        /// <returns>The modified text.</returns>
        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1010:Opening square brackets should be spaced correctly", Justification = "Reviewed.")]
        public static string TruncateAtWordHtml(this string content, int length)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return content;
            }

            int necessaryCount = 0;

            if (HtmlTagsRegex().Replace(content, string.Empty).Length <= length + 50)
            {
                return content;
            }

            string[] split = HtmlTagsRegex().Split(content);
            string counter = string.Empty;

            foreach (string item in split)
            {
                if (counter.Length < length && counter.Length + item.Length >= length)
                {
                    int iNextSpace = item.IndexOf(" ", length - counter.Length, StringComparison.Ordinal);
                    necessaryCount = content.IndexOf(item, counter.Length, StringComparison.Ordinal) + (iNextSpace > 0 ? iNextSpace : item.Length);

                    break;
                }

                counter += item;
            }

            Match x = TagRegex().Match(content, necessaryCount);
            if (x.Value == ">")
            {
                necessaryCount = x.Index + 1;
            }

            string subs = content[..necessaryCount];
            MatchCollection openTags = OpenTagsRegex().Matches(subs);
            MatchCollection closeTags = CloseTagsRegex().Matches(subs);

            List<string> tags = [];
            foreach (object? item in openTags)
            {
                if (item != null)
                {
                    string? strItem = item.ToString();
                    if (!string.IsNullOrEmpty(strItem))
                    {
                        string? trans = AttributeRegex().Match(strItem).Value;
                        trans = $"</{trans.AsSpan(1, trans.Length - 1)}";
                        if (trans[^1] != '>')
                        {
                            trans += ">";
                        }

                        tags.Add(trans);
                    }
                }
            }

            foreach (Match close in closeTags.Cast<Match>())
            {
                _ = tags.Remove(close.Value);
            }

            for (int i = tags.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    subs += " ...";
                }

                subs += tags[i];
            }

            if (subs.Length < content.Length && !subs.Contains("..."))
            {
                subs += "...";
            }

            return subs;
        }

        /// <summary>
        /// Truncates at word.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="length">The length.</param>
        /// <returns>The modified text.</returns>
        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1008:Opening parenthesis should be spaced correctly", Justification = "Reviewed.")]
        public static string TruncateAtWord(this string input, int length)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Length < length)
            {
                return input;
            }

            int iNextSpace = input.LastIndexOf(" ", length, StringComparison.Ordinal);
            return $"{input[..(iNextSpace > 0 ? iNextSpace : length)].Trim()}...";
        }

        [GeneratedRegex(@"(\s+|@|&|'|\(|\)|<|>|#)")]
        private static partial Regex specialCharactersRegex();

        [GeneratedRegex("<[^>]*>")]
        private static partial Regex HtmlTagsRegex();

        [GeneratedRegex("<|>")]
        private static partial Regex TagRegex();

        [GeneratedRegex("<[^/][^>]*>")]
        private static partial Regex OpenTagsRegex();

        [GeneratedRegex("</[^>]*>")]
        private static partial Regex CloseTagsRegex();

        [GeneratedRegex("<[^ ]*")]
        private static partial Regex AttributeRegex();
    }
}