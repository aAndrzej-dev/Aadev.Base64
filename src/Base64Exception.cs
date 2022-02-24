using System;

namespace Aadev.Base64
{
    /// <summary>
    /// Base64 Format Exception
    /// </summary>
    public class Base64Exception : Exception
    {
        internal Base64Exception(string msg) : base(msg) { }
    }
}