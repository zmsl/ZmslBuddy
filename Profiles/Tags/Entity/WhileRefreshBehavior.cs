using ff14bot.NeoProfiles;
using System;
using TreeSharp;

namespace ZmslBuddy.Profiles.Tags.Entity
{
    public class WhileRefreshBehavior : WhileTag
    {
        private readonly GenericRefreshBehavior behavior;

        /// <summary>
        /// Instantiates a new instance of the <see cref="WhileRefreshBehavior"/> class
        /// </summary>
        /// <param name="behavior"></param>
        public WhileRefreshBehavior(WhileTag behavior)
        {
            if (behavior == null)
            {
                throw new ArgumentNullException("behavior");
            }

            this.behavior = new GenericRefreshBehavior(behavior);
        }

        #region Protected overrides
        /// <summary>
        /// Invokes the CreateBehavior method on the behavior passed into the constructor
        /// </summary>
        /// <returns></returns>
        protected override Composite CreateBehavior()
        {
            return this.behavior.CreateBehavior();
        }

        /// <summary>
        /// Invokes the OnDone method on the behavior passed into the constructor
        /// </summary>
        protected override void OnDone()
        {
            this.behavior.OnDone();
        }

        /// <summary>
        /// Invokes the OnResetCachedDone method on the behavior passed into the constructor
        /// </summary>
        protected override void OnResetCachedDone()
        {
            this.behavior.OnResetCachedDone();
        }

        /// <summary>
        /// Invokes the OnStart method on the behavior passed into the constructor
        /// </summary>
        protected override void OnStart()
        {
            this.behavior.OnStart();
        }
        #endregion

        #region Public overrides
        /// <summary>
        /// Gets the HighPriority property on the behavior passed into the constructor
        /// </summary>
        public override bool HighPriority
        {
            get
            {
                return this.behavior.HighPriority;
            }
        }

        /// <summary>
        /// Gets the IsDone property on the behavior passed into the constructor
        /// </summary>
        public override bool IsDone
        {
            get
            {
                return this.behavior.IsDone;
            }
        }

        /// <summary>
        /// Gets the IsDoneCache property on the behavior passed into the constructor
        /// </summary>
        public override bool IsDoneCache
        {
            get
            {
                return this.behavior.IsDoneCache;
            }
        }

        /// <summary>
        /// Gets the StatusText property on the behavior passed into the constructor
        /// </summary>
        public override string StatusText
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
