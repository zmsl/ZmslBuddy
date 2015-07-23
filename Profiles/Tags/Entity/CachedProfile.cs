using ff14bot.NeoProfiles;
using System;
using System.Collections.Generic;
using ZmslBuddy.Profiles.Tags.Extension;

namespace ZmslBuddy.Profiles.Tags.Entity
{
    public class CachedProfile
    {
        /// <summary>
        /// Instantiates a new instance of the <see cref="CachedProfile"/> class
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="updatedOn"></param>
        public CachedProfile(NeoProfile profile, DateTime updatedOn)
        {
            if (profile == null)
            {
                throw new ArgumentNullException("profile");
            }

            this.Profile = profile;
            this.Body = profile.GetOrderBody();
            this.UpdatedOn = updatedOn;
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

        /// <summary>
        /// Gets the date and time that the source file was last seen
        /// updated
        /// </summary>
        public DateTime UpdatedOn
        {
            get;
            private set;
        }
    }
}
