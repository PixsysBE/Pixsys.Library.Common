// -----------------------------------------------------------------------
// <copyright file="BitArrayExtensions.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections;

namespace Pixsys.Library.Common.Extensions
{
    /// <summary>
    /// <see cref="BitArray"/> extensions.
    /// </summary>
    public static class BitArrayExtensions
    {
        /// <summary>
        /// Parses int to bit array.
        /// </summary>
        /// <param name="value">The int value.</param>
        /// <returns>The converted <see cref="BitArray"/>.</returns>
        public static BitArray ParseToBitArray(this int value)
        {
            // Convert int into a byte array
            byte[] byteArray = BitConverter.GetBytes(value);

            // Convert byte array into a bool array
            bool[] boolArray = new bool[byteArray.Length * 8];
            for (int i = 0; i < byteArray.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    boolArray[(i * 8) + j] = (byteArray[i] & (1 << (7 - j))) != 0;
                }
            }

            // Create a BitArray from the bool array
            return new BitArray(boolArray);
        }

        /// <summary>
        /// Converts to int, allowing up to 32 different flags.
        /// </summary>
        /// <remarks>
        /// Equivalent to int in MSSQL.
        /// </remarks>
        /// <param name="bitArray">The bit array.</param>
        /// <returns>The converted <see cref="int"/>.</returns>
        /// <exception cref="ArgumentException">BitArray Size must not exceed 32.</exception>
        public static int ToInt(this BitArray bitArray)
        {
            if (bitArray.Length > 32)
            {
                throw new ArgumentException("BitArray Size must not exceed 32.");
            }

            // Convert BitArray into a bool array
            bool[] boolArray = new bool[bitArray.Length];
            bitArray.CopyTo(boolArray, 0);

            // Create a binary string from the bool array
            string binaryString = string.Concat(boolArray.Select(bit => bit ? "1" : "0"));

            // Convert the binary string into an int by specifying the binary base (2)
            return Convert.ToInt32(binaryString, 2);
        }

        /// <summary>
        /// Parses short to bit array.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The converted <see cref="BitArray"/>.</returns>
        public static BitArray ParseToBitArray(this short value)
        {
            // Convert int into a byte array
            byte[] byteArray = BitConverter.GetBytes(value);

            // Convert byte array into a bool array
            bool[] boolArray = new bool[byteArray.Length * 8];
            for (int i = 0; i < byteArray.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    boolArray[(i * 8) + j] = (byteArray[i] & (1 << (7 - j))) != 0;
                }
            }

            // Create a BitArray from the bool array
            return new BitArray(boolArray);
        }

        /// <summary>
        /// Converts to short, allowing up to 16 different flags.
        /// </summary>
        /// <remarks>
        /// Equivalent to smallint in MSSQL.
        /// </remarks>
        /// <param name="bitArray">The bit array.</param>
        /// <returns>The converted <see cref="short"/>.</returns>
        /// <exception cref="ArgumentException">BitArray Size must not exceed 16.</exception>
        public static short ToShort(this BitArray bitArray)
        {
            if (bitArray.Length > 16)
            {
                throw new ArgumentException("BitArray Size must not exceed 16. Consider switching to int type instead.");
            }

            // Convert BitArray into a bool array
            bool[] boolArray = new bool[bitArray.Length];
            bitArray.CopyTo(boolArray, 0);

            // Create a binary string from the bool array
            string binaryString = string.Concat(boolArray.Select(bit => bit ? "1" : "0"));

            // Convert the binary string into an short by specifying the binary base (2)
            return Convert.ToInt16(binaryString, 2);
        }

        /// <summary>
        /// Parses byte to bit array.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The converted <see cref="BitArray"/>.</returns>
        public static BitArray ParseToBitArray(this byte value)
        {
            return new BitArray(new byte[] { value });
        }

        /// <summary>
        /// Converts to byte, allowing up to 8 different flags.
        /// </summary>
        /// <remarks>
        /// Equivalent to tinyint in MSSQL.
        /// </remarks>
        /// <param name="bitArray">The bit array.</param>
        /// <returns>The converted <see cref="byte"/>.</returns>
        /// <exception cref="ArgumentException">BitArray Size must not exceed 8.</exception>
        public static byte ToByte(this BitArray bitArray)
        {
            if (bitArray.Length > 8)
            {
                throw new ArgumentException("BitArray Size must not exceed 8. Consider switching to short or int instead.");
            }

            byte[] byteArray = new byte[1];
            bitArray.CopyTo(byteArray, 0);

            return byteArray[0];
        }
    }
}