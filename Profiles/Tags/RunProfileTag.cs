using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Clio.Utilities;
using Clio.XmlEngine;
using ff14bot.Behavior;
using ff14bot.NeoProfiles;
using TreeSharp;
using ZmslBuddy.Profiles.Tags.Extension;
using ZmslBuddy.Profiles.Tags.Provider;
using Action = TreeSharp.Action;

namespace ZmslBuddy.Profiles.Tags
{
    [XmlElement("RunProfile")]
    public class RunProfileTag : ProfileBehavior
    {
        // Dependencies because no DI = sadness
        private readonly IProfileBehaviorProvider behaviorProvider = new ProfileBehaviorProvider();

        private string path;
        private bool isDone;
            
        [XmlAttribute("path")]
        public string Path 
        {
            get
            {
                return this.path;
            }
            set
            {
                if (!File.Exists(value))
                {
                    var withRelative = System.IO.Path.Combine(ff14bot.Helpers.Utils.AssemblyDirectory, value);

                    if (!File.Exists(withRelative))
                    {
                        throw new FileNotFoundException("Invalid profile path", value);
                    }

                    this.path = withRelative;
                }

                this.path = value;
            }
        }

        protected override Composite CreateBehavior()
        {
            try
            {
                var behaviorTree = this.behaviorProvider.GetProfileBehavior(
                    NeoProfile.Load(path)
                );

                // Return a sequence of all the behaviors plus the done flag
                return new Sequence(
                    new Action(
                        (r) => Log("In")  
                    ),
                    new PrioritySelector(
                        new Sequence(
                            behaviorTree.ToArray()
                        ),
                        new ActionAlwaysSucceed()
                    ),
                    new Action(
                        (r) => this.isDone = true
                    )
                );
            }
            catch (AggregateException aggEx)
            {
                foreach (var ex in aggEx.InnerExceptions)
                {
                    LogError(ex.ToString());
                }

                return new ActionAlwaysFail();
            }
            catch (Exception ex)
            {
                LogError(ex.ToString());

                return new ActionAlwaysFail();
            }
        }

        protected override void OnResetCachedDone()
        {
            this.isDone = false;
        }

        public override bool IsDone
        {
            get { return this.isDone; }
        }
    }
}
