namespace Aadev.Base64
{
    /// <summary>
    /// Base64 Standard
    /// </summary>
    public enum Base64Standard
    {
        /// <summary>
        /// Normal Base64 standard
        /// Chars: ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/
        /// </summary>
        Base64,
        /// <summary>
        /// Url safe Base64 standard
        /// Chars: ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_
        /// </summary>
        Base64Url
    }
}