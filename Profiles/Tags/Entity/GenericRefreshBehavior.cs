using System;
using ff14bot.NeoProfiles;
using TreeSharp;
using ZmslBuddy.Profiles.Tags.Extension;

namespace ZmslBuddy.Profiles.Tags.Entity
{
    internal class GenericRefreshBehavior
    {
        private readonly ProfileBehavior behavior;

        /// <summary>
        /// Instantiates a new instance of the <see cref="GenericRefreshBehavior"/> class
        /// </summary>
        /// <param name="behavior"></param>
        public GenericRefreshBehavior(ProfileBehavior behavior)
        {
            if (behavior == null)
            {
                throw new ArgumentNullException("behavior");
            }

            this.behavior = behavior;
        }

        #region Protected overrides
        /// <summary>
        /// Invokes the CreateBehavior method on the behavior passed into the constructor
        /// </summary>
        /// <returns></returns>
        public Composite CreateBehavior()
        {
            return (Composite) this.behavior.InvokePrivateMethod("CreateBehavior", null);
        }

        /// <summary>
        /// Invokes the OnDone method on the behavior passed into the constructor
        /// </summary>
        public void OnDone()
        {
            this.behavior.InvokePrivateMethod("OnDone", null);
        }

        /// <summary>
        /// Invokes the OnResetCachedDone method on the behavior passed into the constructor
        /// </summary>
        public void OnResetCachedDone()
        {
            this.behavior.InvokePrivateMethod("OnResetCachedDone", null);
        }

        /// <summary>
        /// Invokes the OnStart method on the behavior passed into the constructor
        /// </summary>
        public void OnStart()
        {
            this.behavior.ResetCachedDone();
            this.behavior.InvokePrivateMethod("OnStart", null);
        }
        #endregion

        #region Public overrides
        /// <summary>
        /// Gets the HighPriority value on the behavior passed into the constructor
        /// </summary>
        public bool HighPriority
        {
            get
            {
                return this.behavior.HighPriority;
            }
        }

        /// <summary>
        /// Gets the IsDone value on the behavior passed into the constructor
        /// </summary>
        public bool IsDone
        {
            get
            {
                return this.behavior.IsDone;
            }
        }

        /// <summary>
        /// Gets the IsDoneCache value on the behavior passed into the constructor
        /// </summary>
        public bool IsDoneCache
        {
            get
            {
                return this.behavior.IsDoneCache;
            }
        }

        /// <summary>
        /// Gets or sets the StatusText value on the behavior passed into the constructor
        /// </summary>
        public string StatusText
        {
            get
            {
                return this.behavior.StatusText;
            }

            set
            {
                this.behavior.StatusText = value;
            }
        }
        #endregion
    }
}
