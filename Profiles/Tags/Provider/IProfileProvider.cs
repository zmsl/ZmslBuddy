using ff14bot.NeoProfiles;
using ZmslBuddy.Profiles.Tags.Entity;

namespace ZmslBuddy.Profiles.Tags.Provider
{
    public interface IProfileProvider
    {
        FlattenedProfile GetProfile(string path);
    }
}
