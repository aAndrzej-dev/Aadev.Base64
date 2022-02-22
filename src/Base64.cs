using System;
using System.Linq;
using System.Text;

namespace Aadev.Base64
{


    public static class Base64
    {
        private const string BASE64CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        private const string BASE64URLCHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";


        public static string Base64EncodeToString(this string orginal, Base64Standard standard = Base64Standard.Base64)
        {
            if (standard is Base64Standard.Base64Url)
            {
                return string.Create((int)(Math.Ceiling(orginal.Length / 3f) * 4), orginal, (span, str) =>
                {
                    int offset = 0;
                    for (int i = 0; i < str.Length; i += 3)
                    {
                        span[offset++] = BASE64URLCHARS[str[i + 0] >> 2];

                        if (i + 1 < str.Length)
                        {
                            span[offset++] = BASE64URLCHARS[(str[i + 0] & 0b00000011) << 4 | str[i + 1] >> 4];

                            if (i + 2 < str.Length)
                            {

                                span[offset++] = BASE64URLCHARS[(str[i + 1] & 0b00001111) << 2 | str[i + 2] >> 6];
                                span[offset++] = BASE64URLCHARS[str[i + 2] & 0b00111111];
                            }
                            else
                            {
                                span[offset++] = BASE64URLCHARS[(str[i + 1] & 0b00001111) << 2];
                                span[offset++] = '=';
                            }

                        }
                        else
                        {
                            span[offset++] = BASE64URLCHARS[(str[i + 0] & 0b00000011) << 4];
                            span[offset++] = '=';
                            span[offset++] = '=';
                        }
                    }
                });
            }
            return string.Create((int)(Math.Ceiling(orginal.Length / 3f) * 4), orginal, (span, str) =>
            {
                int offset = 0;
                for (int i = 0; i < str.Length; i += 3)
                {
                    span[offset++] = BASE64CHARS[str[i + 0] >> 2];

                    if (i + 1 < str.Length)
                    {
                        span[offset++] = BASE64CHARS[(str[i + 0] & 0b00000011) << 4 | str[i + 1] >> 4];

                        if (i + 2 < str.Length)
                        {
                            span[offset++] = BASE64CHARS[(str[i + 1] & 0b00001111) << 2 | str[i + 2] >> 6];
                            span[offset++] = BASE64CHARS[str[i + 2] & 0b00111111];
                        }
                        else
                        {
                            span[offset++] = BASE64CHARS[(str[i + 1] & 0b00001111) << 2];
                            span[offset++] = '=';
                        }

                    }
                    else
                    {
                        span[offset++] = BASE64CHARS[(str[i + 0] & 0b00000011) << 4];
                        span[offset++] = '=';
                        span[offset++] = '=';
                    }
                }
            });
        }
        public static string Base64EncodeToString(this byte[] orginal, Base64Standard standard = Base64Standard.Base64)
        {
            if (standard is Base64Standard.Base64Url)
            {
                return string.Create((int)(Math.Ceiling(orginal.Length / 3f) * 4), orginal, (span, val) =>
                {
                    int offset = 0;
                    for (int i = 0; i < val.Length; i += 3)
                    {
                        span[offset++] = BASE64URLCHARS[val[i + 0] >> 2];

                        if (i + 1 < val.Length)
                        {
                            span[offset++] = BASE64URLCHARS[(val[i + 0] & 0b00000011) << 4 | val[i + 1] >> 4];

                            if (i + 2 < val.Length)
                            {

                                span[offset++] = BASE64URLCHARS[(val[i + 1] & 0b00001111) << 2 | val[i + 2] >> 6];
                                span[offset++] = BASE64URLCHARS[val[i + 2] & 0b00111111];
                            }
                            else
                            {
                                span[offset++] = BASE64URLCHARS[(val[i + 1] & 0b00001111) << 2];
                                span[offset++] = '=';
                            }

                        }
                        else
                        {
                            span[offset++] = BASE64URLCHARS[(val[i + 0] & 0b00000011) << 4];
                            span[offset++] = '=';
                            span[offset++] = '=';
                        }
                    }
                });
            }
            return string.Create((int)(Math.Ceiling(orginal.Length / 3f) * 4), orginal, (span, val) =>
            {
                int offset = 0;
                for (int i = 0; i < val.Length; i += 3)
                {
                    span[offset++] = BASE64CHARS[val[i + 0] >> 2];

                    if (i + 1 < val.Length)
                    {
                        span[offset++] = BASE64CHARS[(val[i + 0] & 0b00000011) << 4 | val[i + 1] >> 4];

                        if (i + 2 < val.Length)
                        {

                            span[offset++] = BASE64CHARS[(val[i + 1] & 0b00001111) << 2 | val[i + 2] >> 6];
                            span[offset++] = BASE64CHARS[val[i + 2] & 0b00111111];
                        }
                        else
                        {
                            span[offset++] = BASE64CHARS[(val[i + 1] & 0b00001111) << 2];
                            span[offset++] = '=';
                        }

                    }
                    else
                    {
                        span[offset++] = BASE64CHARS[(val[i + 0] & 0b00000011) << 4];
                        span[offset++] = '=';
                        span[offset++] = '=';
                    }
                }
            });

        }

        public static byte[] Base64EncodeToByteArray(this string orginal, Base64Standard standard = Base64Standard.Base64)
        {
            Span<byte> span = stackalloc byte[(int)(Math.Ceiling(orginal.Length / 3f) * 4)];

            int offset = 0;
            if (standard == Base64Standard.Base64Url)
            {
                for (int i = 0; i < orginal.Length; i += 3)
                {
                    span[offset++] = (byte)BASE64URLCHARS[orginal[i + 0] >> 2];

                    if (i + 1 < orginal.Length)
                    {
                        span[offset++] = (byte)BASE64URLCHARS[(orginal[i + 0] & 0b00000011) << 4 | orginal[i + 1] >> 4];

                        if (i + 2 < orginal.Length)
                        {
                            span[offset++] = (byte)BASE64URLCHARS[(orginal[i + 1] & 0b00001111) << 2 | orginal[i + 2] >> 6];
                            span[offset++] = (byte)BASE64URLCHARS[orginal[i + 2] & 0b00111111];
                        }
                        else
                        {
                            span[offset++] = (byte)BASE64URLCHARS[(orginal[i + 1] & 0b00001111) << 2];
                            span[offset++] = (byte)'=';
                        }

                    }
                    else
                    {
                        span[offset++] = (byte)BASE64URLCHARS[(orginal[i + 0] & 0b00000011) << 4];
                        span[offset++] = (byte)'=';
                        span[offset++] = (byte)'=';
                    }
                }
            }
            else
            {
                for (int i = 0; i < orginal.Length; i += 3)
                {
                    span[offset++] = (byte)BASE64CHARS[orginal[i + 0] >> 2];

                    if (i + 1 < orginal.Length)
                    {
                        span[offset++] = (byte)BASE64CHARS[(orginal[i + 0] & 0b00000011) << 4 | orginal[i + 1] >> 4];

                        if (i + 2 < orginal.Length)
                        {
                            span[offset++] = (byte)BASE64CHARS[(orginal[i + 1] & 0b00001111) << 2 | orginal[i + 2] >> 6];
                            span[offset++] = (byte)BASE64CHARS[orginal[i + 2] & 0b00111111];
                        }
                        else
                        {
                            span[offset++] = (byte)BASE64CHARS[(orginal[i + 1] & 0b00001111) << 2];
                            span[offset++] = (byte)'=';
                        }

                    }
                    else
                    {
                        span[offset++] = (byte)BASE64CHARS[(orginal[i + 0] & 0b00000011) << 4];
                        span[offset++] = (byte)'=';
                        span[offset++] = (byte)'=';
                    }
                }
            }

            return span.ToArray();

        }
        public static byte[] Base64EncodeToByteArray(this byte[] orginal, Base64Standard standard = Base64Standard.Base64)
        {
            Span<byte> span = stackalloc byte[(int)(Math.Ceiling(orginal.Length / 3f) * 4)];

            int offset = 0;
            if (standard == Base64Standard.Base64Url)
            {
                for (int i = 0; i < orginal.Length; i += 3)
                {
                    span[offset++] = (byte)BASE64URLCHARS[orginal[i + 0] >> 2];

                    if (i + 1 < orginal.Length)
                    {
                        span[offset++] = (byte)BASE64URLCHARS[(orginal[i + 0] & 0b00000011) << 4 | orginal[i + 1] >> 4];

                        if (i + 2 < orginal.Length)
                        {
                            span[offset++] = (byte)BASE64URLCHARS[(orginal[i + 1] & 0b00001111) << 2 | orginal[i + 2] >> 6];
                            span[offset++] = (byte)BASE64URLCHARS[orginal[i + 2] & 0b00111111];
                        }
                        else
                        {
                            span[offset++] = (byte)BASE64URLCHARS[(orginal[i + 1] & 0b00001111) << 2];
                            span[offset++] = (byte)'=';
                        }

                    }
                    else
                    {
                        span[offset++] = (byte)BASE64URLCHARS[(orginal[i + 0] & 0b00000011) << 4];
                        span[offset++] = (byte)'=';
                        span[offset++] = (byte)'=';
                    }
                }
            }
            else
            {
                for (int i = 0; i < orginal.Length; i += 3)
                {
                    span[offset++] = (byte)BASE64CHARS[orginal[i + 0] >> 2];

                    if (i + 1 < orginal.Length)
                    {
                        span[offset++] = (byte)BASE64CHARS[(orginal[i + 0] & 0b00000011) << 4 | orginal[i + 1] >> 4];

                        if (i + 2 < orginal.Length)
                        {
                            span[offset++] = (byte)BASE64CHARS[(orginal[i + 1] & 0b00001111) << 2 | orginal[i + 2] >> 6];
                            span[offset++] = (byte)BASE64CHARS[orginal[i + 2] & 0b00111111];
                        }
                        else
                        {
                            span[offset++] = (byte)BASE64CHARS[(orginal[i + 1] & 0b00001111) << 2];
                            span[offset++] = (byte)'=';
                        }

                    }
                    else
                    {
                        span[offset++] = (byte)BASE64CHARS[(orginal[i + 0] & 0b00000011) << 4];
                        span[offset++] = (byte)'=';
                        span[offset++] = (byte)'=';
                    }
                }
            }

            return span.ToArray();

        }

        public static string Base64DecodeToString(this string base64string, Base64Standard standard = Base64Standard.Base64)
        {
            if (base64string.Length % 4 != 0)
            {
                throw new Base64Exception("Base64 string must be a multiple of 4");
            }





            StringBuilder sb = new StringBuilder();
            if (standard is Base64Standard.Base64Url)
            {
                base64string = base64string.Replace('-', '+').Replace('_', '/');
            }

            for (int i = 0; i < base64string.Length; i += 4)
            {
                int g0 = BASE64CHARS.IndexOf(base64string[i + 0]);
                int g1 = BASE64CHARS.IndexOf(base64string[i + 1]);
                int g2 = BASE64CHARS.IndexOf(base64string[i + 2]);
                int g3 = BASE64CHARS.IndexOf(base64string[i + 3]);
                sb.Append((char)(g0 << 2 | g1 >> 4));

                if (g2 == -1) continue;

                sb.Append((char)((g1 & 0b00001111) << 4 | (g2 >> 2)));

                if (g3 == -1) continue;


                sb.Append((char)((g2 & 0b00000011) << 6 | g3));



            }



            return sb.ToString();
        }

        public static string Base64DecodeToString(this byte[] base64, Base64Standard standard = Base64Standard.Base64)
        {
            if (base64.Length % 4 != 0)
            {
                throw new Base64Exception("Base64 string lenght must be multiplication of 4");
            }

            StringBuilder sb = new StringBuilder();
            if (standard is Base64Standard.Base64Url)
            {
                base64 = base64.Select(x => x is (byte)'-' ? (byte)'+' : (x is (byte)'_' ? (byte)'/' : x)).ToArray();
            }

            for (int i = 0; i < base64.Length; i += 4)
            {
                int g0 = BASE64CHARS.IndexOf((char)base64[i + 0]);
                int g1 = BASE64CHARS.IndexOf((char)base64[i + 1]);
                int g2 = BASE64CHARS.IndexOf((char)base64[i + 2]);
                int g3 = BASE64CHARS.IndexOf((char)base64[i + 3]);
                sb.Append((char)(g0 << 2 | g1 >> 4));

                if (g2 == -1) continue;

                sb.Append((char)((g1 & 0b00001111) << 4 | (g2 >> 2)));

                if (g3 == -1) continue;

                sb.Append((char)((g2 & 0b00000011) << 6 | g3));



            }



            return sb.ToString();
        }
    }
}