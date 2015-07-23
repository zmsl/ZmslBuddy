using System;
using ff14bot.NeoProfiles;
using ZmslBuddy.Profiles.Tags.Entity;
using ZmslBuddy.Profiles.Tags.Utility;

namespace ZmslBuddy.Profiles.Tags.Provider
{
    public class RealTimeProfileProvider : IProfileProvider
    {
        /// <summary>
        /// Returns a freshly loaded profile from disk
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public CachedProfile GetProfile(string path)
        {
            var normalizedPath = PathUtility.NormalizeFilePath(path);

            return new CachedProfile(NeoProfile.Load(normalizedPath), DateTime.Now);
        }
    }
}
