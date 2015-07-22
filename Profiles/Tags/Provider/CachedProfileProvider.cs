using ff14bot.NeoProfiles;
using ZmslBuddy.Profiles.Tags.Entity;
using ZmslBuddy.Profiles.Tags.Utility;

namespace ZmslBuddy.Profiles.Tags.Provider
{
    public class CachedProfileProvider : IProfileProvider
    {
        /// <summary>
        /// Gets a <see cref="NeoProfile"/> from the cache. If the cache
        /// object has not been instantiated, the specified file will
        /// be loaded.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public CachedProfile GetProfile(string path)
        {
            var normalizedPath = PathUtility.NormalizeFilePath(path);

            // Get an existing profile
            var existingProfile = ObjectCache<CachedProfile>.Instance[normalizedPath];

            // Get the last write time on the file
            var lastWrite = FileUtility.GetLastWriteTime(path);

            // Compare the update datetimes and load a fresh copy if it has been updated
            if (existingProfile != null && lastWrite <= existingProfile.UpdatedOn)
            {
                return existingProfile;
            }

            return ObjectCache<CachedProfile>.Instance[normalizedPath] = new CachedProfile(NeoProfile.Load(normalizedPath), lastWrite);
        }
    }
}
