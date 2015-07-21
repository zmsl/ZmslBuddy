using System;
using System.IO;

namespace ZmslBuddy.Profiles.Tags.Utility
{
    public static class PathUtility
    {
        /// <summary>
        /// Normalizes the whitespace, casing, 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">Thrown when <paramref name="path"/> is not a valid file path</exception>
        public static string NormalizeFilePath(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File path is invalid", path);
            }

            return Path.GetFullPath(new Uri(path).LocalPath)
               .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
               .ToUpperInvariant();
        }
    }
}
