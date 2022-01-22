using System.Text;

namespace Aadev.Base64;

public static class Base64
{
    private const string BASE64CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

    public static string Base64EncodeToString(this string orginal, Base64Standard standard = Base64Standard.Base64)
    {
        StringBuilder sb = new();

        for (int i = 0; i < orginal.Length; i += 3)
        {
            sb.Append(BASE64CHARS[orginal[i + 0] >> 2]);

            if (i + 1 < orginal.Length)
            {
                sb.Append(BASE64CHARS[(orginal[i + 0] & 0b00000011) << 4 | orginal[i + 1] >> 4]);

                if (i + 2 < orginal.Length)
                {

                    sb.Append(BASE64CHARS[(orginal[i + 1] & 0b00001111) << 2 | orginal[i + 2] >> 6]);
                    sb.Append(BASE64CHARS[orginal[i + 2] & 0b00111111]);
                }
                else
                {
                    sb.Append(BASE64CHARS[(orginal[i + 1] & 0b00001111) << 2]);
                    sb.Append('=');
                }

            }
            else
            {
                sb.Append(BASE64CHARS[(orginal[i + 0] & 0b00000011) << 4]);
                sb.Append("==");
            }
        }
        if (standard is Base64Standard.Base64Url)
        {
            return sb.ToString().Replace('+', '-').Replace('/', '_');
        }
        return sb.ToString();
    }
    public static string Base64EncodeToString(this byte[] orginal, Base64Standard standard = Base64Standard.Base64)
    {
        StringBuilder sb = new();

        for (int i = 0; i < orginal.Length; i += 3)
        {
            sb.Append(BASE64CHARS[orginal[i + 0] >> 2]);

            if (i + 1 < orginal.Length)
            {
                sb.Append(BASE64CHARS[(orginal[i + 0] & 0b00000011) << 4 | orginal[i + 1] >> 4]);

                if (i + 2 < orginal.Length)
                {

                    sb.Append(BASE64CHARS[(orginal[i + 1] & 0b00001111) << 2 | orginal[i + 2] >> 6]);
                    sb.Append(BASE64CHARS[orginal[i + 2] & 0b00111111]);
                }
                else
                {
                    sb.Append(BASE64CHARS[(orginal[i + 1] & 0b00001111) << 2]);
                    sb.Append('=');
                }

            }
            else
            {
                sb.Append(BASE64CHARS[(orginal[i + 0] & 0b00000011) << 4]);
                sb.Append("==");
            }
        }
        if (standard is Base64Standard.Base64Url)
        {
            return sb.ToString().Replace('+', '-').Replace('/', '_');
        }
        return sb.ToString();
    }
    public static string Base64DecodeToString(this string base64string, Base64Standard standard = Base64Standard.Base64)
    {
        if (base64string.Length % 4 != 0)
        {
            throw new Base64Exception("Base64 string must be multiplication of 4");
        }

        StringBuilder sb = new();
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

            if (g2 != -1)
            {
                sb.Append((char)((g1 & 0b00001111) << 4 | (g2 >> 2)));

                if (g3 != -1)
                {
                    sb.Append((char)((g2 & 0b00000011) << 6 | g3));

                }
            }

        }

        return sb.ToString();
    }
    public static string Base64DecodeToString(this byte[] base64, Base64Standard standard = Base64Standard.Base64)
    {
        if (base64.Length % 4 != 0)
        {
            throw new Base64Exception("Base64 string must be multiplication of 4");
        }

        StringBuilder sb = new();
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

            if (g2 != -1)
            {
                sb.Append((char)((g1 & 0b00001111) << 4 | (g2 >> 2)));

                if (g3 != -1)
                {
                    sb.Append((char)((g2 & 0b00000011) << 6 | g3));

                }
            }
        }

        return sb.ToString();
    }
}
public class Base64Exception : Exception
{
    public Base64Exception(string msg) : base(msg) { }
}

public enum Base64Standard
{
    Base64,
    Base64Url
}

