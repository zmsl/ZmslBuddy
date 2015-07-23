using ff14bot.NeoProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using ZmslBuddy.Profiles.Tags.Extension;

namespace ZmslBuddy.Profiles.Tags.Entity
{
    public class FlattenedProfile
    {
        /// <summary>
        /// Intantiates a new instance of the <see cref="Profile"/> class
        /// </summary>
        /// <param name="profile"></param>
        public FlattenedProfile(NeoProfile profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException("profile");
            }

            this.Profile = profile;
            this.Body = profile
                .GetOrderBody()
                .Select(
                    b => {
                        var whileTag = b as WhileTag;
                        return whileTag != null
                        ? (ProfileBehavior)new WhileRefreshBehavior(whileTag)
                        : (ProfileBehavior)new ProfileRefreshBehavior(b);
                    })
                .ToList();
        }

        /// <summary>
        /// Gets the cached profile object
        /// </summary>
        public NeoProfile Profile
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the cached profile body
        /// </summary>
        public List<ProfileBehavior> Body
        {
            get;
            private set;
        }
    }
}
