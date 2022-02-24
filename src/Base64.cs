using System;

namespace Aadev.Base64
{

    /// <summary>
    /// Base64 Encode/Decode class
    /// </summary>
    public static class Base64
    {
        private const string BASE64CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        private const string BASE64URLCHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";

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

            return new string(span);
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

            return new string(span);
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

        public static int EncodeToString(ReadOnlySpan<char> source, Span<char> buffer, Base64Standard standard = Base64Standard.Base64)
        {
            int offset = 0;
            if (standard is Base64Standard.Base64Url)
            {
                for (int i = 0; i < source.Length; i += 3)
                {
                    buffer[offset++] = BASE64URLCHARS[source[i + 0] >> 2];

                    if (i + 1 < source.Length)
                    {
                        buffer[offset++] = BASE64URLCHARS[(source[i + 0] & 0b00000011) << 4 | source[i + 1] >> 4];

                        if (i + 2 < source.Length)
                        {
                            buffer[offset++] = BASE64URLCHARS[(source[i + 1] & 0b00001111) << 2 | source[i + 2] >> 6];
                            buffer[offset++] = BASE64URLCHARS[source[i + 2] & 0b00111111];
                        }
                        else
                        {
                            buffer[offset++] = BASE64URLCHARS[(source[i + 1] & 0b00001111) << 2];
                            buffer[offset++] = '=';
                        }

                    }
                    else
                    {
                        buffer[offset++] = BASE64URLCHARS[(source[i + 0] & 0b00000011) << 4];
                        buffer[offset++] = '=';
                        buffer[offset++] = '=';
                    }
                }
            }
            else
            {
                for (int i = 0; i < source.Length; i += 3)
                {
                    buffer[offset++] = BASE64CHARS[source[i + 0] >> 2];

                    if (i + 1 < source.Length)
                    {
                        buffer[offset++] = BASE64CHARS[(source[i + 0] & 0b00000011) << 4 | source[i + 1] >> 4];

                        if (i + 2 < source.Length)
                        {
                            buffer[offset++] = BASE64CHARS[(source[i + 1] & 0b00001111) << 2 | source[i + 2] >> 6];
                            buffer[offset++] = BASE64CHARS[source[i + 2] & 0b00111111];
                        }
                        else
                        {
                            buffer[offset++] = BASE64CHARS[(source[i + 1] & 0b00001111) << 2];
                            buffer[offset++] = '=';
                        }

                    }
                    else
                    {
                        buffer[offset++] = BASE64CHARS[(source[i + 0] & 0b00000011) << 4];
                        buffer[offset++] = '=';
                        buffer[offset++] = '=';
                    }
                }
            }

            return offset;
        }
        public static int EncodeToString(ReadOnlySpan<byte> source, Span<char> buffer, Base64Standard standard = Base64Standard.Base64)
        {

            int offset = 0;
            if (standard is Base64Standard.Base64Url)
            {
                for (int i = 0; i < source.Length; i += 3)
                {
                    buffer[offset++] = BASE64URLCHARS[source[i + 0] >> 2];

                    if (i + 1 < source.Length)
                    {
                        buffer[offset++] = BASE64URLCHARS[(source[i + 0] & 0b00000011) << 4 | source[i + 1] >> 4];

                        if (i + 2 < source.Length)
                        {

                            buffer[offset++] = BASE64URLCHARS[(source[i + 1] & 0b00001111) << 2 | source[i + 2] >> 6];
                            buffer[offset++] = BASE64URLCHARS[source[i + 2] & 0b00111111];
                        }
                        else
                        {
                            buffer[offset++] = BASE64URLCHARS[(source[i + 1] & 0b00001111) << 2];
                            buffer[offset++] = '=';
                        }

                    }
                    else
                    {
                        buffer[offset++] = BASE64URLCHARS[(source[i + 0] & 0b00000011) << 4];
                        buffer[offset++] = '=';
                        buffer[offset++] = '=';
                    }
                }
            }
            else
            {
                for (int i = 0; i < source.Length; i += 3)
                {
                    buffer[offset++] = BASE64CHARS[source[i + 0] >> 2];

                    if (i + 1 < source.Length)
                    {
                        buffer[offset++] = BASE64CHARS[(source[i + 0] & 0b00000011) << 4 | source[i + 1] >> 4];

                        if (i + 2 < source.Length)
                        {

                            buffer[offset++] = BASE64CHARS[(source[i + 1] & 0b00001111) << 2 | source[i + 2] >> 6];
                            buffer[offset++] = BASE64CHARS[source[i + 2] & 0b00111111];
                        }
                        else
                        {
                            buffer[offset++] = BASE64CHARS[(source[i + 1] & 0b00001111) << 2];
                            buffer[offset++] = '=';
                        }

                    }
                    else
                    {
                        buffer[offset++] = BASE64CHARS[(source[i + 0] & 0b00000011) << 4];
                        buffer[offset++] = '=';
                        buffer[offset++] = '=';
                    }
                }
            }

            return offset;
        }
        public static int EncodeToByteArray(ReadOnlySpan<char> source, Span<byte> buffer, Base64Standard standard = Base64Standard.Base64)
        {
            int offset = 0;
            if (standard is Base64Standard.Base64Url)
            {
                for (int i = 0; i < source.Length; i += 3)
                {
                    buffer[offset++] = (byte)BASE64URLCHARS[source[i + 0] >> 2];

                    if (i + 1 < source.Length)
                    {
                        buffer[offset++] = (byte)BASE64URLCHARS[(source[i + 0] & 0b00000011) << 4 | source[i + 1] >> 4];

                        if (i + 2 < source.Length)
                        {
                            buffer[offset++] = (byte)BASE64URLCHARS[(source[i + 1] & 0b00001111) << 2 | source[i + 2] >> 6];
                            buffer[offset++] = (byte)BASE64URLCHARS[source[i + 2] & 0b00111111];
                        }
                        else
                        {
                            buffer[offset++] = (byte)BASE64URLCHARS[(source[i + 1] & 0b00001111) << 2];
                            buffer[offset++] = (byte)'=';
                        }

                    }
                    else
                    {
                        buffer[offset++] = (byte)BASE64URLCHARS[(source[i + 0] & 0b00000011) << 4];
                        buffer[offset++] = (byte)'=';
                        buffer[offset++] = (byte)'=';
                    }
                }
            }
            else
            {
                for (int i = 0; i < source.Length; i += 3)
                {
                    buffer[offset++] = (byte)BASE64CHARS[source[i + 0] >> 2];

                    if (i + 1 < source.Length)
                    {
                        buffer[offset++] = (byte)BASE64CHARS[(source[i + 0] & 0b00000011) << 4 | source[i + 1] >> 4];

                        if (i + 2 < source.Length)
                        {
                            buffer[offset++] = (byte)BASE64CHARS[(source[i + 1] & 0b00001111) << 2 | source[i + 2] >> 6];
                            buffer[offset++] = (byte)BASE64CHARS[source[i + 2] & 0b00111111];
                        }
                        else
                        {
                            buffer[offset++] = (byte)BASE64CHARS[(source[i + 1] & 0b00001111) << 2];
                            buffer[offset++] = (byte)'=';
                        }

                    }
                    else
                    {
                        buffer[offset++] = (byte)BASE64CHARS[(source[i + 0] & 0b00000011) << 4];
                        buffer[offset++] = (byte)'=';
                        buffer[offset++] = (byte)'=';
                    }
                }
            }

            return offset;

        }
        public static int EncodeToByteArray(ReadOnlySpan<byte> source, Span<byte> buffer, Base64Standard standard = Base64Standard.Base64)
        {

            int offset = 0;
            if (standard is Base64Standard.Base64Url)
            {
                for (int i = 0; i < source.Length; i += 3)
                {
                    buffer[offset++] = (byte)BASE64URLCHARS[source[i + 0] >> 2];

                    if (i + 1 < source.Length)
                    {
                        buffer[offset++] = (byte)BASE64URLCHARS[(source[i + 0] & 0b00000011) << 4 | source[i + 1] >> 4];

                        if (i + 2 < source.Length)
                        {
                            buffer[offset++] = (byte)BASE64URLCHARS[(source[i + 1] & 0b00001111) << 2 | source[i + 2] >> 6];
                            buffer[offset++] = (byte)BASE64URLCHARS[source[i + 2] & 0b00111111];
                        }
                        else
                        {
                            buffer[offset++] = (byte)BASE64URLCHARS[(source[i + 1] & 0b00001111) << 2];
                            buffer[offset++] = (byte)'=';
                        }

                    }
                    else
                    {
                        buffer[offset++] = (byte)BASE64URLCHARS[(source[i + 0] & 0b00000011) << 4];
                        buffer[offset++] = (byte)'=';
                        buffer[offset++] = (byte)'=';
                    }
                }
            }
            else
            {
                for (int i = 0; i < source.Length; i += 3)
                {
                    buffer[offset++] = (byte)BASE64CHARS[source[i + 0] >> 2];

                    if (i + 1 < source.Length)
                    {
                        buffer[offset++] = (byte)BASE64CHARS[(source[i + 0] & 0b00000011) << 4 | source[i + 1] >> 4];

                        if (i + 2 < source.Length)
                        {
                            buffer[offset++] = (byte)BASE64CHARS[(source[i + 1] & 0b00001111) << 2 | source[i + 2] >> 6];
                            buffer[offset++] = (byte)BASE64CHARS[source[i + 2] & 0b00111111];
                        }
                        else
                        {
                            buffer[offset++] = (byte)BASE64CHARS[(source[i + 1] & 0b00001111) << 2];
                            buffer[offset++] = (byte)'=';
                        }

                    }
                    else
                    {
                        buffer[offset++] = (byte)BASE64CHARS[(source[i + 0] & 0b00000011) << 4];
                        buffer[offset++] = (byte)'=';
                        buffer[offset++] = (byte)'=';
                    }
                }
            }

            return offset;

        }


        /// <summary>
        /// Convert Base64 string to normal string
        /// </summary>
        /// <param name="source">Base64 string</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Normal string</returns>
        public static string DecodeToString(ReadOnlySpan<char> source, Base64Standard standard = Base64Standard.Base64)
        {
            Span<char> span = stackalloc char[GetDecodedLenght(source)];

            DecodeToString(source, span, standard);

            return new string(span);
        }
        /// <summary>
        /// Convert Base64 byte array to normal string
        /// </summary>
        /// <param name="source">Base64 byte array</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Normal string</returns>
        public static string DecodeToString(ReadOnlySpan<byte> source, Base64Standard standard = Base64Standard.Base64)
        {
            Span<char> span = stackalloc char[GetDecodedLenght(source)];

            DecodeToString(source, span, standard);

            return new string(span);
        }
        /// <summary>
        /// Convert Base64 string to normal byte array
        /// </summary>
        /// <param name="source">Base64 string</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Normal byte array</returns>
        public static byte[] DecodeToByteArray(ReadOnlySpan<char> source, Base64Standard standard = Base64Standard.Base64)
        {
            Span<byte> span = stackalloc byte[GetDecodedLenght(source)];

            DecodeToByteArray(source, span, standard);

            return span.ToArray();
        }
        /// <summary>
        /// Convert Base64 byte array to normal byte array
        /// </summary>
        /// <param name="source">Base64 byte array</param>
        /// <param name="standard">Base64 Standard</param>
        /// <returns>Normal byte array</returns>
        public static byte[] DecodeToByteArray(ReadOnlySpan<byte> source, Base64Standard standard = Base64Standard.Base64)
        {
            Span<byte> span = stackalloc byte[GetDecodedLenght(source)];

            DecodeToByteArray(source, span, standard);

            return span.ToArray();
        }


        public static int DecodeToString(ReadOnlySpan<char> source, Span<char> buffer, Base64Standard standard = Base64Standard.Base64)
        {
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
                    buffer[offset++] = (char)(g0 << 2 | g1 >> 4);

                    if (g2 == -1) continue;

                    buffer[offset++] = (char)((g1 & 0b00001111) << 4 | (g2 >> 2));

                    if (g3 == -1) continue;

                    buffer[offset++] = (char)((g2 & 0b00000011) << 6 | g3);
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
                    buffer[offset++] = (char)(g0 << 2 | g1 >> 4);

                    if (g2 == -1) continue;

                    buffer[offset++] = (char)((g1 & 0b00001111) << 4 | (g2 >> 2));

                    if (g3 == -1) continue;

                    buffer[offset++] = (char)((g2 & 0b00000011) << 6 | g3);
                }
            }

            return offset;
        }
        public static int DecodeToString(ReadOnlySpan<byte> source, Span<char> buffer, Base64Standard standard = Base64Standard.Base64)
        {
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
                    buffer[offset++] = (char)(g0 << 2 | g1 >> 4);

                    if (g2 == -1) continue;

                    buffer[offset++] = (char)((g1 & 0b00001111) << 4 | (g2 >> 2));

                    if (g3 == -1) continue;

                    buffer[offset++] = (char)((g2 & 0b00000011) << 6 | g3);



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
                    buffer[offset++] = (char)(g0 << 2 | g1 >> 4);

                    if (g2 == -1) continue;

                    buffer[offset++] = (char)((g1 & 0b00001111) << 4 | (g2 >> 2));

                    if (g3 == -1) continue;

                    buffer[offset++] = (char)((g2 & 0b00000011) << 6 | g3);



                }
            }

            return offset;
        }
        public static int DecodeToByteArray(ReadOnlySpan<char> source, Span<byte> buffer, Base64Standard standard = Base64Standard.Base64)
        {
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
                    buffer[offset++] = (byte)(g0 << 2 | g1 >> 4);

                    if (g2 == -1) continue;

                    buffer[offset++] = (byte)((g1 & 0b00001111) << 4 | (g2 >> 2));

                    if (g3 == -1) continue;


                    buffer[offset++] = (byte)((g2 & 0b00000011) << 6 | g3);



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
                    buffer[offset++] = (byte)(g0 << 2 | g1 >> 4);

                    if (g2 == -1) continue;

                    buffer[offset++] = (byte)((g1 & 0b00001111) << 4 | (g2 >> 2));

                    if (g3 == -1) continue;


                    buffer[offset++] = (byte)((g2 & 0b00000011) << 6 | g3);



                }
            }

            return offset;
        }
        public static int DecodeToByteArray(ReadOnlySpan<byte> source, Span<byte> buffer, Base64Standard standard = Base64Standard.Base64)
        {
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
                    buffer[offset++] = (byte)(g0 << 2 | g1 >> 4);

                    if (g2 == -1) continue;

                    buffer[offset++] = (byte)((g1 & 0b00001111) << 4 | (g2 >> 2));

                    if (g3 == -1) continue;

                    buffer[offset++] = (byte)((g2 & 0b00000011) << 6 | g3);



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
                    buffer[offset++] = (byte)(g0 << 2 | g1 >> 4);

                    if (g2 == -1) continue;

                    buffer[offset++] = (byte)((g1 & 0b00001111) << 4 | (g2 >> 2));

                    if (g3 == -1) continue;

                    buffer[offset++] = (byte)((g2 & 0b00000011) << 6 | g3);



                }
            }

            return offset;
        }

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
        public static int GetDecodedLenght(ReadOnlySpan<char> source) => source.Length / 4 * 3 - (source[^1] == '=' ? 1 : 0) - (source[^2] == '=' ? 1 : 0);
        /// <summary>
        /// Returns lenght of decoded byte array
        /// </summary>
        /// <param name="source">Base64 byte array</param>
        /// <returns>Lenght of decoded string</returns>
        public static int GetDecodedLenght(ReadOnlySpan<byte> source) => source.Length / 4 * 3 - (source[^1] == (byte)'=' ? 1 : 0) - (source[^2] == (byte)'=' ? 1 : 0);
    }
}