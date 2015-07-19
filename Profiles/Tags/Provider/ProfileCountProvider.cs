namespace ZmslBuddy.Profiles.Tags.Provider
{
    public class ProfileCountProvider : ICountProvider
    {
        public int GetCount(string name)
        {
            return ProfileStateCache<int>.Instance[name];
        }

        public void SetCount(string name, int count)
        {
            ProfileStateCache<int>.Instance[name] = count;
        }
    }
}
