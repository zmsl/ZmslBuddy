using System;
using System.Collections.Generic;

namespace ZmslBuddy.Profiles.Tags
{
    public class ProfileStateCache<T>
    {
        #region Static members
        private static readonly Lazy<ProfileStateCache<T>> _Instance = new Lazy<ProfileStateCache<T>>(() => new ProfileStateCache<T>(), true);

        /// <summary>
        /// Gets the singleton instance of the <see cref="ProfileStateCache{T}"/> class
        /// </summary>
        public static ProfileStateCache<T> Instance
        {
            get { return ProfileStateCache<T>._Instance.Value; }
        }

        private readonly Lazy<Dictionary<string, T>> cache = new Lazy<Dictionary<string, T>>(() => new Dictionary<string, T>(), true);
        #endregion

        /// <summary>
        /// Get or set a value in the cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>The key will be set with the type default if trying to get the value before it was set</remarks>
        public T this[string key]
        {
            get { return this.cache.Value.ContainsKey(key) ? this.cache.Value[key] : default(T); }
            set
            {
                if (!this.cache.Value.ContainsKey(key))
                {
                    this.cache.Value.Add(key, value);
                    return;
                }

                this.cache.Value[key] = value;
            }
        }

        /// <summary>
        /// Clears all values from the cache
        /// </summary>
        public void Clear()
        {
            this.cache.Value.Clear();
        }
    }
}
