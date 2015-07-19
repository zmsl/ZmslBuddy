using System;
using System.Collections.Generic;
using System.Linq;
using Clio.Utilities;
using ff14bot.Behavior;
using ff14bot.NeoProfiles;
using TreeSharp;
using ZmslBuddy.Profiles.Tags.Extension;
using Action = TreeSharp.Action;

namespace ZmslBuddy.Profiles.Tags.Provider
{
    public class ProfileBehaviorProvider : IProfileBehaviorProvider
    {
        /// <summary>
        /// Gets the profile behavior tree
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public IEnumerable<Composite> GetProfileBehavior(NeoProfile profile)
        {
            var body = profile.GetPrivateField<List<ProfileBehavior>>("Order");

            return this.GetProfileBehaviorCollectionBehavior(body);
        }

        /// <summary>
        /// Gets the behavior tree for a collection of profile behaviors
        /// </summary>
        /// <param name="behaviors"></param>
        /// <returns></returns>
        private IEnumerable<Composite> GetProfileBehaviorCollectionBehavior(IEnumerable<ProfileBehavior> behaviors)
        {
            var compositeList = new List<Composite>();
            var exceptions = new List<Exception>();

            foreach (var behavior in behaviors)
            {
                try
                {
                    compositeList.Add(
                        this.GetProfileBehaviorTree(behavior)
                    );
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            // Throw an aggregate exception if we have exceptions
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }

            return compositeList;
        }

        /// <summary>
        /// Attempts to get complex behavior from the tag. Will return null if tags are not
        /// <see cref="IfTag"/> or <see cref="WhileTag"/> implementations of the <see cref="ProfileBehavior"/> class
        /// </summary>
        /// <param name="behavior"></param>
        /// <returns></returns>
        private Composite GetProfileBehaviorTree(ProfileBehavior behavior)
        {
            var ifBehavior = behavior as IfTag;
            var whileBehavior = behavior as WhileTag;
            
            // Return null if both behaviors turn up null
            if (ifBehavior == null && whileBehavior == null)
            {
                return (Composite) typeof (ProfileBehavior)
                    .GetMethod(
                        "CreateBehavior",
                        System.Reflection.BindingFlags.Instance
                        | System.Reflection.BindingFlags.NonPublic
                    )
                    .Invoke(
                        behavior,
                        null
                    );
            }

            // Get the condition & body to use for our behavior tree
            var condition = ifBehavior == null
                ? whileBehavior.Condition
                : ifBehavior.Condition;
            var body = ifBehavior == null
                ? whileBehavior.Body
                : ifBehavior.Body;

            return ifBehavior == null
                ? (Composite) new WhileLoop(
                    (r) => ScriptManager.GetCondition(condition).Invoke(),
                    this.GetProfileBehaviorCollectionBehavior(body).ToArray()
                    )
                : (Composite) new Decorator(
                    (r) => ScriptManager.GetCondition(condition).Invoke(),
                    new Sequence(
                        this.GetProfileBehaviorCollectionBehavior(body).ToArray()
                    )
                );
        }
    }
}