namespace ZmslBuddy.Profiles.Tags.Provider
{
    public interface ICountProvider
    {
        /// <summary>
        /// Gets the count using the name as the count reference
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int GetCount(string name);

        /// <summary>
        /// Sets the count to the value using the name as the count reference
        /// </summary>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        void SetCount(string name, int count);
    }
}