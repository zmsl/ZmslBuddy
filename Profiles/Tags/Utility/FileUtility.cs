using System;
using System.IO;

namespace ZmslBuddy.Profiles.Tags.Utility
{
    public static class FileUtility
    {
        /// <summary>
        /// Gets the update timestamp on a file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DateTime GetLastWriteTime(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File path is invalid", path);
            }

            return File.GetLastWriteTimeUtc(path);
        }

        /// <summary>
        /// Gets the absolute path based on the base path
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="relativeOrAbsolutePath"></param>
        /// <returns></returns>
        public static string GetAbsolutePath(string basePath, string relativeOrAbsolutePath)
        {
            if (!File.Exists(relativeOrAbsolutePath))
            {
                var withRelative = System.IO.Path.Combine(ff14bot.Helpers.Utils.AssemblyDirectory, relativeOrAbsolutePath);

                if (!File.Exists(withRelative))
                {
                    throw new FileNotFoundException("Invalid profile path", relativeOrAbsolutePath);
                }

                return withRelative;
            }

            return relativeOrAbsolutePath;
        }
    }
}
