using System.Collections.Generic;
using ff14bot.NeoProfiles;
using TreeSharp;

namespace ZmslBuddy.Profiles.Tags.Provider
{
    public interface IProfileBehaviorProvider
    {
        IEnumerable<Composite> GetProfileBehavior(NeoProfile profile);
    }
}
