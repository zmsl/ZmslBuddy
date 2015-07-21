using System;
using ZmslBuddy.Profiles.Tags.Enum;

namespace ZmslBuddy.Profiles.Tags.Provider
{
    public class ProfileCountProvider : ICountProvider
    {
        #region Static members
        private static readonly Lazy<ProfileCountProvider> _Instance = new Lazy<ProfileCountProvider>(() => new ProfileCountProvider(), true);

        /// <summary>
        /// Evaluates the named count against the value using the comparison specified
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="method"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static bool EvaluateCountCondition(string name, int value, ComparisonMethod method, ComparisonModifier modifier)
        {
            var currentCount = ProfileCountProvider.Instance.GetCount(name);
            var result =
                (method.HasFlag(ComparisonMethod.Equal) && currentCount == value) ||
                (method.HasFlag(ComparisonMethod.GreaterThan) && currentCount > value) ||
                (method.HasFlag(ComparisonMethod.GreaterThanOrEqual) && currentCount >= value) ||
                (method.HasFlag(ComparisonMethod.LessThan) && currentCount < value) ||
                (method.HasFlag(ComparisonMethod.LessThanOrEqual) && currentCount <= value);

            return modifier == ComparisonModifier.Not ? !result : result;
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="ObjectCache{T}"/> class
        /// </summary>
        public static ProfileCountProvider Instance
        {
            get { return ProfileCountProvider._Instance.Value; }
        }
        #endregion

        public int GetCount(string name)
        {
            return ObjectCache<int>.Instance[name];
        }

        public void SetCount(string name, int count)
        {
            ObjectCache<int>.Instance[name] = count;
        }
    }
}
