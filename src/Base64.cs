using System;

namespace Aadev.Base64
{

    /// <summary>
    /// Base64 Encode/Decode class
    /// </summary>
    public static class Base64
    {
        private const string BASE64CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
        private const string BASE64URLCHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_=";

        #region Encode_simple

        /// <summary>
        /// Encode string to base64 string
        /// </summary>
        /// <param name="source">String to encode</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Base64 string</returns>
        public static string EncodeToString(ReadOnlySpan<char> source, Base64Standard standard = Base64Standard.Base64)
        {
            Span<char> span = stackalloc char[GetEncodedLenght(source)];

            EncodeToString(source, span, standard);

#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
            return new string(span);
#else
            unsafe
            {
                fixed (char* ch = &span.GetPinnableReference()) {
                    return new string(ch);
                }
            }
#endif
        }
        /// <summary>
        /// Encode byte array to base64 string
        /// </summary>
        /// <param name="source">Byte array to encode</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Base64 string</returns>
        public static string EncodeToString(ReadOnlySpan<byte> source, Base64Standard standard = Base64Standard.Base64)
        {
            Span<char> span = stackalloc char[GetEncodedLenght(source)];

            EncodeToString(source, span, standard);

#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
            return new string(span);
#else
            unsafe
            {
                fixed (char* ch = &span.GetPinnableReference()) {
                    return new string(ch);
                }
            }
#endif
        }
        /// <summary>
        /// Encode string to base64 byte array
        /// </summary>
        /// <param name="source">String to encode</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Base64 byte array</returns>
        public static byte[] EncodeToByteArray(ReadOnlySpan<char> source, Base64Standard standard = Base64Standard.Base64)
        {
            Span<byte> span = stackalloc byte[GetEncodedLenght(source)];

            EncodeToByteArray(source, span, standard);

            return span.ToArray();
        }
        /// <summary>
        /// Encode byte array to base64 byte array
        /// </summary>
        /// <param name="source">byte array to encode</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Base64 byte array</returns>
        public static byte[] EncodeToByteArray(ReadOnlySpan<byte> source, Base64Standard standard = Base64Standard.Base64)
        {
            Span<byte> span = stackalloc byte[GetEncodedLenght(source)];

            EncodeToByteArray(source, span, standard);

            return span.ToArray();
        }

        #endregion

        #region Encode_buffer

        /// <summary>
        /// Encode string to base64 string
        /// </summary>
        /// <param name="source">String to encode</param>
        /// <param name="destination">Destination buffer</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Number of chars written into buffer</returns>
        /// <exception cref="ArgumentException">buffer is to small</exception>
        public static int EncodeToString(ReadOnlySpan<char> source, Span<char> destination, Base64Standard standard = Base64Standard.Base64)
        {
            if (destination.Length < GetEncodedLenght(source))
                throw new ArgumentException($"{nameof(destination)} buffer is to small. Required {source.Length}, {destination.Length} given");
            int offset = 0;
            if (standard is Base64Standard.Base64Url)
            {
                for (int i = 0; i < source.Length; i += 3)
                {
                    destination[offset++] = BASE64URLCHARS[source[i + 0] >> 2];

                    if (i + 1 < source.Length)
                    {
                        destination[offset++] = BASE64URLCHARS[(source[i + 0] & 0b00000011) << 4 | source[i + 1] >> 4];

                        if (i + 2 < source.Length)
                        {
                            destination[offset++] = BASE64URLCHARS[(source[i + 1] & 0b00001111) << 2 | source[i + 2] >> 6];
                            destination[offset++] = BASE64URLCHARS[source[i + 2] & 0b00111111];
                        }
                        else
                        {
                            destination[offset++] = BASE64URLCHARS[(source[i + 1] & 0b00001111) << 2];
                            destination[offset++] = '=';
                        }

                    }
                    else
                    {
                        destination[offset++] = BASE64URLCHARS[(source[i + 0] & 0b00000011) << 4];
                        destination[offset++] = '=';
                        destination[offset++] = '=';
                    }
                }
            }
            else
            {
                for (int i = 0; i < source.Length; i += 3)
                {
                    destination[offset++] = BASE64CHARS[source[i + 0] >> 2];

                    if (i + 1 < source.Length)
                    {
                        destination[offset++] = BASE64CHARS[(source[i + 0] & 0b00000011) << 4 | source[i + 1] >> 4];

                        if (i + 2 < source.Length)
                        {
                            destination[offset++] = BASE64CHARS[(source[i + 1] & 0b00001111) << 2 | source[i + 2] >> 6];
                            destination[offset++] = BASE64CHARS[source[i + 2] & 0b00111111];
                        }
                        else
                        {
                            destination[offset++] = BASE64CHARS[(source[i + 1] & 0b00001111) << 2];
                            destination[offset++] = '=';
                        }

                    }
                    else
                    {
                        destination[offset++] = BASE64CHARS[(source[i + 0] & 0b00000011) << 4];
                        destination[offset++] = '=';
                        destination[offset++] = '=';
                    }
                }
            }

            return offset;
        }
        /// <summary>
        /// Encode byte array to base64 string
        /// </summary>
        /// <param name="source">Byte array to encode</param>
        /// <param name="destination">Destination buffer</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Number of chars written into buffer</returns>
        /// <exception cref="ArgumentException">buffer is to small</exception>
        public static int EncodeToString(ReadOnlySpan<byte> source, Span<char> destination, Base64Standard standard = Base64Standard.Base64)
        {
            if (destination.Length < GetEncodedLenght(source))
                throw new ArgumentException($"{nameof(destination)} buffer is to small. Required {source.Length}, {destination.Length} given");
            int offset = 0;
            if (standard is Base64Standard.Base64Url)
            {
                for (int i = 0; i < source.Length; i += 3)
                {
                    destination[offset++] = BASE64URLCHARS[source[i + 0] >> 2];

                    if (i + 1 < source.Length)
                    {
                        destination[offset++] = BASE64URLCHARS[(source[i + 0] & 0b00000011) << 4 | source[i + 1] >> 4];

                        if (i + 2 < source.Length)
                        {

                            destination[offset++] = BASE64URLCHARS[(source[i + 1] & 0b00001111) << 2 | source[i + 2] >> 6];
                            destination[offset++] = BASE64URLCHARS[source[i + 2] & 0b00111111];
                        }
                        else
                        {
                            destination[offset++] = BASE64URLCHARS[(source[i + 1] & 0b00001111) << 2];
                            destination[offset++] = '=';
                        }

                    }
                    else
                    {
                        destination[offset++] = BASE64URLCHARS[(source[i + 0] & 0b00000011) << 4];
                        destination[offset++] = '=';
                        destination[offset++] = '=';
                    }
                }
            }
            else
            {
                for (int i = 0; i < source.Length; i += 3)
                {
                    destination[offset++] = BASE64CHARS[source[i + 0] >> 2];

                    if (i + 1 < source.Length)
                    {
                        destination[offset++] = BASE64CHARS[(source[i + 0] & 0b00000011) << 4 | source[i + 1] >> 4];

                        if (i + 2 < source.Length)
                        {

                            destination[offset++] = BASE64CHARS[(source[i + 1] & 0b00001111) << 2 | source[i + 2] >> 6];
                            destination[offset++] = BASE64CHARS[source[i + 2] & 0b00111111];
                        }
                        else
                        {
                            destination[offset++] = BASE64CHARS[(source[i + 1] & 0b00001111) << 2];
                            destination[offset++] = '=';
                        }

                    }
                    else
                    {
                        destination[offset++] = BASE64CHARS[(source[i + 0] & 0b00000011) << 4];
                        destination[offset++] = '=';
                        destination[offset++] = '=';
                    }
                }
            }

            return offset;
        }
        /// <summary>
        /// Encode string to base64 byte array
        /// </summary>
        /// <param name="source">String to encode</param>
        /// <param name="destination">Destination buffer</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Number of chars written into buffer</returns>
        /// <exception cref="ArgumentException">buffer is to small</exception>
        public static int EncodeToByteArray(ReadOnlySpan<char> source, Span<byte> destination, Base64Standard standard = Base64Standard.Base64)
        {
            if (destination.Length < GetEncodedLenght(source))
                throw new ArgumentException($"{nameof(destination)} buffer is to small. Required {source.Length}, {destination.Length} given");
            int offset = 0;
            if (standard is Base64Standard.Base64Url)
            {
                for (int i = 0; i < source.Length; i += 3)
                {
                    destination[offset++] = (byte)BASE64URLCHARS[source[i + 0] >> 2];

                    if (i + 1 < source.Length)
                    {
                        destination[offset++] = (byte)BASE64URLCHARS[(source[i + 0] & 0b00000011) << 4 | source[i + 1] >> 4];

                        if (i + 2 < source.Length)
                        {
                            destination[offset++] = (byte)BASE64URLCHARS[(source[i + 1] & 0b00001111) << 2 | source[i + 2] >> 6];
                            destination[offset++] = (byte)BASE64URLCHARS[source[i + 2] & 0b00111111];
                        }
                        else
                        {
                            destination[offset++] = (byte)BASE64URLCHARS[(source[i + 1] & 0b00001111) << 2];
                            destination[offset++] = (byte)'=';
                        }

                    }
                    else
                    {
                        destination[offset++] = (byte)BASE64URLCHARS[(source[i + 0] & 0b00000011) << 4];
                        destination[offset++] = (byte)'=';
                        destination[offset++] = (byte)'=';
                    }
                }
            }
            else
            {
                for (int i = 0; i < source.Length; i += 3)
                {
                    destination[offset++] = (byte)BASE64CHARS[source[i + 0] >> 2];

                    if (i + 1 < source.Length)
                    {
                        destination[offset++] = (byte)BASE64CHARS[(source[i + 0] & 0b00000011) << 4 | source[i + 1] >> 4];

                        if (i + 2 < source.Length)
                        {
                            destination[offset++] = (byte)BASE64CHARS[(source[i + 1] & 0b00001111) << 2 | source[i + 2] >> 6];
                            destination[offset++] = (byte)BASE64CHARS[source[i + 2] & 0b00111111];
                        }
                        else
                        {
                            destination[offset++] = (byte)BASE64CHARS[(source[i + 1] & 0b00001111) << 2];
                            destination[offset++] = (byte)'=';
                        }

                    }
                    else
                    {
                        destination[offset++] = (byte)BASE64CHARS[(source[i + 0] & 0b00000011) << 4];
                        destination[offset++] = (byte)'=';
                        destination[offset++] = (byte)'=';
                    }
                }
            }

            return offset;

        }
        /// <summary>
        /// Encode byte array to base64 byte array
        /// </summary>
        /// <param name="source">Byte array to encode</param>
        /// <param name="destination">Destination buffer</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Number of chars written into buffer</returns>
        /// <exception cref="ArgumentException">buffer is to small</exception>
        public static int EncodeToByteArray(ReadOnlySpan<byte> source, Span<byte> destination, Base64Standard standard = Base64Standard.Base64)
        {
            if (destination.Length < GetEncodedLenght(source))
                throw new ArgumentException($"{nameof(destination)} buffer is to small. Required {source.Length}, {destination.Length} given");
            int offset = 0;
            if (standard is Base64Standard.Base64Url)
            {
                for (int i = 0; i < source.Length; i += 3)
                {
                    destination[offset++] = (byte)BASE64URLCHARS[source[i + 0] >> 2];

                    if (i + 1 < source.Length)
                    {
                        destination[offset++] = (byte)BASE64URLCHARS[(source[i + 0] & 0b00000011) << 4 | source[i + 1] >> 4];

                        if (i + 2 < source.Length)
                        {
                            destination[offset++] = (byte)BASE64URLCHARS[(source[i + 1] & 0b00001111) << 2 | source[i + 2] >> 6];
                            destination[offset++] = (byte)BASE64URLCHARS[source[i + 2] & 0b00111111];
                        }
                        else
                        {
                            destination[offset++] = (byte)BASE64URLCHARS[(source[i + 1] & 0b00001111) << 2];
                            destination[offset++] = (byte)'=';
                        }

                    }
                    else
                    {
                        destination[offset++] = (byte)BASE64URLCHARS[(source[i + 0] & 0b00000011) << 4];
                        destination[offset++] = (byte)'=';
                        destination[offset++] = (byte)'=';
                    }
                }
            }
            else
            {
                for (int i = 0; i < source.Length; i += 3)
                {
                    destination[offset++] = (byte)BASE64CHARS[source[i + 0] >> 2];

                    if (i + 1 < source.Length)
                    {
                        destination[offset++] = (byte)BASE64CHARS[(source[i + 0] & 0b00000011) << 4 | source[i + 1] >> 4];

                        if (i + 2 < source.Length)
                        {
                            destination[offset++] = (byte)BASE64CHARS[(source[i + 1] & 0b00001111) << 2 | source[i + 2] >> 6];
                            destination[offset++] = (byte)BASE64CHARS[source[i + 2] & 0b00111111];
                        }
                        else
                        {
                            destination[offset++] = (byte)BASE64CHARS[(source[i + 1] & 0b00001111) << 2];
                            destination[offset++] = (byte)'=';
                        }

                    }
                    else
                    {
                        destination[offset++] = (byte)BASE64CHARS[(source[i + 0] & 0b00000011) << 4];
                        destination[offset++] = (byte)'=';
                        destination[offset++] = (byte)'=';
                    }
                }
            }

            return offset;

        }

        #endregion

        #region Decode_simple

        /// <summary>
        /// Decode base64 string to decoded string
        /// </summary>
        /// <param name="source">Base64 string</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Decoded string</returns>
        /// <exception cref="Base64Exception">Base64 string must be a multiple of 4</exception>
        public static string DecodeToString(ReadOnlySpan<char> source, Base64Standard standard = Base64Standard.Base64)
        {
            Span<char> span = stackalloc char[GetDecodedLenght(source)];

            DecodeToString(source, span, standard);

#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
            return new string(span);
#else
            unsafe
            {
                fixed (char* ch = &span.GetPinnableReference()) {
                    return new string(ch);
                }
            }
#endif
        }
        /// <summary>
        /// Decode base64 byte array to string
        /// </summary>
        /// <param name="source">Base64 byte array</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Decoded string</returns>
        /// <exception cref="Base64Exception">Base64 string must be a multiple of 4</exception>
        public static string DecodeToString(ReadOnlySpan<byte> source, Base64Standard standard = Base64Standard.Base64)
        {
            Span<char> span = stackalloc char[GetDecodedLenght(source)];

            DecodeToString(source, span, standard);

#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
            return new string(span);
#else
            unsafe
            {
                fixed (char* ch = &span.GetPinnableReference()) {
                    return new string(ch);
                }
            }
#endif
        }
        /// <summary>
        /// Decode base64 string to byte array
        /// </summary>
        /// <param name="source">Base64 string</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Decoded byte array</returns>
        /// <exception cref="Base64Exception">Base64 string must be a multiple of 4</exception>
        public static byte[] DecodeToByteArray(ReadOnlySpan<char> source, Base64Standard standard = Base64Standard.Base64)
        {
            Span<byte> span = stackalloc byte[GetDecodedLenght(source)];

            DecodeToByteArray(source, span, standard);

            return span.ToArray();
        }
        /// <summary>
        /// Decode base64 byte array to byte array
        /// </summary>
        /// <param name="source">Base64 byte array</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Decoded byte array</returns>
        /// <exception cref="Base64Exception">Base64 string must be a multiple of 4</exception>
        public static byte[] DecodeToByteArray(ReadOnlySpan<byte> source, Base64Standard standard = Base64Standard.Base64)
        {
            Span<byte> span = stackalloc byte[GetDecodedLenght(source)];

            DecodeToByteArray(source, span, standard);

            return span.ToArray();
        }

        #endregion

        #region Decode_buffer

        /// <summary>
        /// Decode base64 string to string
        /// </summary>
        /// <param name="source">String to decode</param>
        /// <param name="destination">Destination buffer</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Number of chars written into buffer</returns>
        /// <exception cref="ArgumentException">buffer is to small</exception>
        /// <exception cref="Base64Exception">Base64 string must be a multiple of 4</exception>
        public static int DecodeToString(ReadOnlySpan<char> source, Span<char> destination, Base64Standard standard = Base64Standard.Base64)
        {
            if (destination.Length < GetDecodedLenght(source))
                throw new ArgumentException($"{nameof(destination)} buffer is to small. Required {source.Length}, {destination.Length} given");
            if (source.Length % 4 != 0)
            {
                throw new Base64Exception("Base64 string must be a multiple of 4");
            }

            int offset = 0;

            if (standard is Base64Standard.Base64Url)
            {
                for (int i = 0; i < source.Length; i += 4)
                {
                    int g0 = BASE64URLCHARS.IndexOf(source[i + 0]);
                    int g1 = BASE64URLCHARS.IndexOf(source[i + 1]);
                    int g2 = BASE64URLCHARS.IndexOf(source[i + 2]);
                    int g3 = BASE64URLCHARS.IndexOf(source[i + 3]);

                    if (g0 == -1 || g1 == -1 || g2 == -1 || g3 == -1) throw new Base64Exception($"Invalid char in base64 string");

                    destination[offset++] = (char)(g0 << 2 | g1 >> 4);

                    if (g2 == 64) continue;

                    destination[offset++] = (char)((g1 & 0b00001111) << 4 | (g2 >> 2));

                    if (g3 == 64) continue;

                    destination[offset++] = (char)((g2 & 0b00000011) << 6 | g3);
                }
            }
            else
            {
                for (int i = 0; i < source.Length; i += 4)
                {
                    int g0 = BASE64CHARS.IndexOf(source[i + 0]);
                    int g1 = BASE64CHARS.IndexOf(source[i + 1]);
                    int g2 = BASE64CHARS.IndexOf(source[i + 2]);
                    int g3 = BASE64CHARS.IndexOf(source[i + 3]);

                    if (g0 == -1 || g1 == -1 || g2 == -1 || g3 == -1) throw new Base64Exception($"Invalid char in base64 string");

                    destination[offset++] = (char)(g0 << 2 | g1 >> 4);

                    if (g2 == 64) continue;

                    destination[offset++] = (char)((g1 & 0b00001111) << 4 | (g2 >> 2));

                    if (g3 == 64) continue;

                    destination[offset++] = (char)((g2 & 0b00000011) << 6 | g3);
                }
            }

            return offset;
        }
        /// <summary>
        /// Decode base64 byte array to string
        /// </summary>
        /// <param name="source">Byte array to decode</param>
        /// <param name="destination">Destination buffer</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Number of chars written into buffer</returns>
        /// <exception cref="ArgumentException">buffer is to small</exception>
        /// <exception cref="Base64Exception">Base64 string must be a multiple of 4</exception>
        public static int DecodeToString(ReadOnlySpan<byte> source, Span<char> destination, Base64Standard standard = Base64Standard.Base64)
        {
            if (destination.Length < GetDecodedLenght(source))
                throw new ArgumentException($"{nameof(destination)} buffer is to small. Required {source.Length}, {destination.Length} given");
            if (source.Length % 4 != 0)
            {
                throw new Base64Exception("Base64 string lenght must be multiplication of 4");
            }

            int offset = 0;
            if (standard is Base64Standard.Base64Url)
            {
                for (int i = 0; i < source.Length; i += 4)
                {
                    int g0 = BASE64URLCHARS.IndexOf((char)source[i + 0]);
                    int g1 = BASE64URLCHARS.IndexOf((char)source[i + 1]);
                    int g2 = BASE64URLCHARS.IndexOf((char)source[i + 2]);
                    int g3 = BASE64URLCHARS.IndexOf((char)source[i + 3]);

                    if (g0 == -1 || g1 == -1 || g2 == -1 || g3 == -1) throw new Base64Exception($"Invalid char in base64 string");

                    destination[offset++] = (char)(g0 << 2 | g1 >> 4);

                    if (g2 == 64) continue;

                    destination[offset++] = (char)((g1 & 0b00001111) << 4 | (g2 >> 2));

                    if (g3 == 64) continue;

                    destination[offset++] = (char)((g2 & 0b00000011) << 6 | g3);



                }

            }
            else
            {
                for (int i = 0; i < source.Length; i += 4)
                {
                    int g0 = BASE64CHARS.IndexOf((char)source[i + 0]);
                    int g1 = BASE64CHARS.IndexOf((char)source[i + 1]);
                    int g2 = BASE64CHARS.IndexOf((char)source[i + 2]);
                    int g3 = BASE64CHARS.IndexOf((char)source[i + 3]);

                    if (g0 == -1 || g1 == -1 || g2 == -1 || g3 == -1) throw new Base64Exception($"Invalid char in base64 string");

                    destination[offset++] = (char)(g0 << 2 | g1 >> 4);

                    if (g2 == 64) continue;

                    destination[offset++] = (char)((g1 & 0b00001111) << 4 | (g2 >> 2));

                    if (g3 == 64) continue;

                    destination[offset++] = (char)((g2 & 0b00000011) << 6 | g3);



                }
            }

            return offset;
        }
        /// <summary>
        /// Decode base64 string to byte array
        /// </summary>
        /// <param name="source">String to decode</param>
        /// <param name="destination">Destination buffer</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Number of chars written into buffer</returns>
        /// <exception cref="ArgumentException">buffer is to small</exception>
        /// <exception cref="Base64Exception">Base64 string must be a multiple of 4</exception>
        public static int DecodeToByteArray(ReadOnlySpan<char> source, Span<byte> destination, Base64Standard standard = Base64Standard.Base64)
        {
            if (destination.Length < GetDecodedLenght(source))
                throw new ArgumentException($"{nameof(destination)} buffer is to small. Required {source.Length}, {destination.Length} given");
            if (source.Length % 4 != 0)
            {
                throw new Base64Exception("Base64 string must be a multiple of 4");
            }

            int offset = 0;
            if (standard is Base64Standard.Base64Url)
            {
                for (int i = 0; i < source.Length; i += 4)
                {
                    int g0 = BASE64URLCHARS.IndexOf(source[i + 0]);
                    int g1 = BASE64URLCHARS.IndexOf(source[i + 1]);
                    int g2 = BASE64URLCHARS.IndexOf(source[i + 2]);
                    int g3 = BASE64URLCHARS.IndexOf(source[i + 3]);

                    if (g0 == -1 || g1 == -1 || g2 == -1 || g3 == -1) throw new Base64Exception($"Invalid char in base64 string");

                    destination[offset++] = (byte)(g0 << 2 | g1 >> 4);

                    if (g2 == 64) continue;

                    destination[offset++] = (byte)((g1 & 0b00001111) << 4 | (g2 >> 2));

                    if (g3 == 64) continue;


                    destination[offset++] = (byte)((g2 & 0b00000011) << 6 | g3);



                }
            }
            else
            {
                for (int i = 0; i < source.Length; i += 4)
                {
                    int g0 = BASE64CHARS.IndexOf(source[i + 0]);
                    int g1 = BASE64CHARS.IndexOf(source[i + 1]);
                    int g2 = BASE64CHARS.IndexOf(source[i + 2]);
                    int g3 = BASE64CHARS.IndexOf(source[i + 3]);

                    if (g0 == -1 || g1 == -1 || g2 == -1 || g3 == -1) throw new Base64Exception($"Invalid char in base64 string");

                    destination[offset++] = (byte)(g0 << 2 | g1 >> 4);

                    if (g2 == 64) continue;

                    destination[offset++] = (byte)((g1 & 0b00001111) << 4 | (g2 >> 2));

                    if (g3 == 64) continue;


                    destination[offset++] = (byte)((g2 & 0b00000011) << 6 | g3);



                }
            }

            return offset;
        }
        /// <summary>
        /// Decode base64 byte array to byte array
        /// </summary>
        /// <param name="source">Byte array to decode</param>
        /// <param name="destination">Destination buffer</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Number of chars written into buffer</returns>
        /// <exception cref="ArgumentException">buffer is to small</exception>
        /// <exception cref="Base64Exception">Base64 string must be a multiple of 4</exception>
        public static int DecodeToByteArray(ReadOnlySpan<byte> source, Span<byte> destination, Base64Standard standard = Base64Standard.Base64)
        {
            if (destination.Length < GetDecodedLenght(source))
                throw new ArgumentException($"{nameof(destination)} buffer is to small. Required {source.Length}, {destination.Length} given");
            if (source.Length % 4 != 0)
            {
                throw new Base64Exception("Base64 string lenght must be multiplication of 4");
            }

            int offset = 0;
            if (standard is Base64Standard.Base64Url)
            {
                for (int i = 0; i < source.Length; i += 4)
                {
                    int g0 = BASE64URLCHARS.IndexOf((char)source[i + 0]);
                    int g1 = BASE64URLCHARS.IndexOf((char)source[i + 1]);
                    int g2 = BASE64URLCHARS.IndexOf((char)source[i + 2]);
                    int g3 = BASE64URLCHARS.IndexOf((char)source[i + 3]);

                    if (g0 == -1 || g1 == -1 || g2 == -1 || g3 == -1) throw new Base64Exception($"Invalid char in base64 string");

                    destination[offset++] = (byte)(g0 << 2 | g1 >> 4);

                    if (g2 == 64) continue;

                    destination[offset++] = (byte)((g1 & 0b00001111) << 4 | (g2 >> 2));

                    if (g3 == 64) continue;

                    destination[offset++] = (byte)((g2 & 0b00000011) << 6 | g3);



                }
            }
            else
            {
                for (int i = 0; i < source.Length; i += 4)
                {
                    int g0 = BASE64CHARS.IndexOf((char)source[i + 0]);
                    int g1 = BASE64CHARS.IndexOf((char)source[i + 1]);
                    int g2 = BASE64CHARS.IndexOf((char)source[i + 2]);
                    int g3 = BASE64CHARS.IndexOf((char)source[i + 3]);

                    if (g0 == -1 || g1 == -1 || g2 == -1 || g3 == -1) throw new Base64Exception($"Invalid char in base64 string");

                    destination[offset++] = (byte)(g0 << 2 | g1 >> 4);

                    if (g2 == 64) continue;

                    destination[offset++] = (byte)((g1 & 0b00001111) << 4 | (g2 >> 2));

                    if (g3 == 64) continue;

                    destination[offset++] = (byte)((g2 & 0b00000011) << 6 | g3);



                }
            }

            return offset;
        }

        #endregion

        #region Get_lenght


        /// <summary>
        /// Returns lenght of Base64 string when encoding
        /// </summary>
        /// <param name="source">String to encode</param>
        /// <returns>Lenght of encoded base64 string</returns>
        public static int GetEncodedLenght(ReadOnlySpan<char> source) => (int)(Math.Ceiling(source.Length / 3f) * 4);
        /// <summary>
        /// Returns lenght of Base64 byte array when encoding
        /// </summary>
        /// <param name="source">String to encode</param>
        /// <returns>Lenght of encoded base64 string</returns>
        public static int GetEncodedLenght(ReadOnlySpan<byte> source) => (int)(Math.Ceiling(source.Length / 3f) * 4);
        /// <summary>
        /// Returns lenght of decoded string
        /// </summary>
        /// <param name="source">Base64 string</param>
        /// <returns>Lenght of decoded string</returns>
        public static int GetDecodedLenght(ReadOnlySpan<char> source) => source.Length / 4 * 3 - (source[source.Length - 1] is '=' ? 1 : 0) - (source[source.Length - 2] is '=' ? 1 : 0);
        /// <summary>
        /// Returns lenght of decoded byte array
        /// </summary>
        /// <param name="source">Base64 byte array</param>
        /// <returns>Lenght of decoded string</returns>
        public static int GetDecodedLenght(ReadOnlySpan<byte> source) => source.Length / 4 * 3 - (source[source.Length - 1] is 0x3d/*=*/ ? 1 : 0) - (source[source.Length - 2] is 0x3d/*=*/ ? 1 : 0);

        #endregion
    }
}