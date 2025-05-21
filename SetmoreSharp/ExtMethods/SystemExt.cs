namespace System
{
    public static class SystemExt
    {
        public static bool HasValue(this string val)
        {
            return !string.IsNullOrEmpty(val);
        }
    }
}