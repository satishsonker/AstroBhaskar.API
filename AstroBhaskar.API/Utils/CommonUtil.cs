using System;

namespace AstroBhaskar.API.Utils
{
    public static class CommonUtil
    {
        public static string GetGUID()
        {
            return Guid.NewGuid().ToString().Replace("-", "").ToUpper();
        }

        public static string GetGUID(int length)
        {
            length = length < 1 ? 1 : length;
            string guid = string.Empty;
            for (int i = 1; i < length; i++)
            {
                guid += Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            }
            return guid;
        }
    }
}
