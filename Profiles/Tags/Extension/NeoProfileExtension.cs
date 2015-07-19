using System.Collections.Generic;
using ff14bot.NeoProfiles;

namespace ZmslBuddy.Profiles.Tags.Extension
{
    public static class NeoProfileExtension
    {
        /// <summary>
        /// Returns the body of an order profile
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public static List<ProfileBehavior> GetOrderBody(this NeoProfile profile)
        {
            return profile.GetPrivateField<List<ProfileBehavior>>("Order");
        }
    }
}
