using System.Numerics;

namespace Versiones01.Controllers
{
    public static class VersionComparer
    {
        public static int Compare(string version1, string version2)
        {
            
            var parts1 = version1.Split('.');
            var parts2 = version2.Split('.');

            for (int i = 0; i < Math.Max(parts1.Length, parts2.Length); i++)
            {
                if (parts2.Length <= i)
                { 
                    return -3;
                }

                if (!IsInteger(parts2[i]))
                    throw new Exception("Number must be integer");

                int value1 = i < parts1.Length ? int.Parse(parts1[i]) : 0;
                int value2 = i < parts2.Length ? int.Parse(parts2[i]) : 0;

                if (value1 > value2)
                    return 1;
                else if (value1 < value2)
                    return -1;
            }

            return 0;
        }

        private static bool IsInteger(string version2)
        {
            try
            {
                int.Parse(version2);
                return true;
            }
            catch { }

            return false;
        }
    }
}
