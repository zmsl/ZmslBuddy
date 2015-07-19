using System;
using System.Collections.Generic;
using System.Linq;
using Clio.Utilities;
using ff14bot;
using ff14bot.Behavior;
using ff14bot.NeoProfiles;
using TreeSharp;
using ZmslBuddy.Profiles.Tags.Behavior;
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
            Composite bodyBehavior;
            
            if (ifBehavior != null)
            {
                bodyBehavior = new Decorator(
                    (r) => ScriptManager.GetCondition(ifBehavior.Condition).Invoke(),
                    new Sequence(
                        this.GetProfileBehaviorCollectionBehavior(ifBehavior.Body).ToArray()
                        )
                    );
            }
            else if (whileBehavior != null)
            {
                bodyBehavior = new WhileLoop(
                    (r) => ScriptManager.GetCondition(whileBehavior.Condition).Invoke(),
                    this.GetProfileBehaviorCollectionBehavior(whileBehavior.Body).ToArray()
                );
            }
            else
            {
                bodyBehavior = (Composite) behavior.InvokePrivateMethod<Composite>("CreateBehavior", null);
            }

            return new PrioritySelector(
                new ActionRunOnce(
                    (r) =>
                    {
                        try
                        {
                            behavior.InvokePrivateMethod("UpdateBehavior");
                            behavior.Start();
                        }
                        catch
                        {
                            // TODO: Get a better understanding of how RB starts the profile behaviors during behavior execution
                        }

                        return RunStatus.Failure;
                    }
                ),
                bodyBehavior,
                new ActionAlwaysSucceed()
            );
        }
    }
}