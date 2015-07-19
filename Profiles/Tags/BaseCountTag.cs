using System;
using TreeSharp;
using ZmslBuddy.Profiles.Tags.Provider;
using Action = TreeSharp.Action;

namespace ZmslBuddy.Profiles.Tags
{
    public abstract class BaseCountTag : BaseCacheTag<int>
    {
        private readonly ICountProvider countProvider;

        /// <summary>
        /// Instantiates the base class <see cref="BaseCountTag"/>
        /// </summary>
        /// <param name="countProvider"></param>
        protected BaseCountTag(ICountProvider countProvider)
        {
            if (countProvider == null)
            {
                throw new ArgumentNullException("countProvider");
            }

            this.countProvider = countProvider;
        }

        /// <summary>
        /// Creates behavior that will set the cache value to the return value of the method override GetNewValue
        /// </summary>
        /// <returns></returns>
        protected override Composite CreateBodyBehavior()
        {
            return new Action(
                (r) =>
                {
                    var newCount = this.GetNewCount(this.countProvider.GetCount(this.Name));
                    this.countProvider.SetCount(this.Name, newCount > 0 ? newCount : 0);
                }
            );
        }

        protected abstract int GetNewCount(int oldCount);
    }
}
