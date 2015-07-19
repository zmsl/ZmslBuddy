using System;
using Clio.XmlEngine;
using ff14bot.Behavior;
using ff14bot.NeoProfiles;
using TreeSharp;
using Action = TreeSharp.Action;

namespace ZmslBuddy.Profiles.Tags
{
    public abstract class BaseCacheTag<T> : ProfileBehavior
    {
        private bool isDone = false;

        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets the value of the count key
        /// </summary>
        protected T Value
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.Name))
                {
                    throw new MemberAccessException("Object property Name cannot be accessed when null or empty");
                }

                return ProfileStateCache<T>.Instance[this.Name];
            }
        }

        /// <summary>
        /// Returns a behavior tree which encapsulates child class logic with enveloping logic
        /// </summary>
        /// <returns></returns>
        protected sealed override Composite CreateBehavior()
        {
            var bodyBehavior = this.CreateBodyBehavior() ?? new ActionAlwaysSucceed();

            return new Sequence(
                new PrioritySelector(
                    new Decorator(
                        (r) => string.IsNullOrWhiteSpace(this.Name),
                        new ActionAlwaysSucceed()
                    ),
                    bodyBehavior
                ),
                new Action(
                    (r) => this.isDone = true
                )
            );
        }

        /// <summary>
        /// Creates and returns the behavior tree used after the base behavior executes
        /// </summary>
        /// <returns></returns>
        protected abstract Composite CreateBodyBehavior();

        /// <summary>
        /// Gets whether the tag execution has finished
        /// </summary>
        public override bool IsDone
        {
            get { return this.isDone; }
        }
    }
}
