using ff14bot.NeoProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using ZmslBuddy.Profiles.Tags.Extension;

namespace ZmslBuddy.Profiles.Tags.Entity
{
    public class CachedProfile : FlattenedProfile
    {
        /// <summary>
        /// Instantiates a new instance of the <see cref="CachedProfile"/> class
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="updatedOn"></param>
        public CachedProfile(NeoProfile profile, DateTime updatedOn)
            : base(profile)
        {
            this.UpdatedOn = updatedOn;
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
