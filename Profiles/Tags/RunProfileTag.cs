using Clio.XmlEngine;
using ff14bot.NeoProfiles;
using ZmslBuddy.Profiles.Tags.Provider;
using ZmslBuddy.Profiles.Tags.Utility;

namespace ZmslBuddy.Profiles.Tags
{
    [XmlElement("RunProfile")]
    public class RunProfileTag : IfTag
    {
        private readonly IProfileProvider profileProvider;

        private string path;
        private bool isDone;

        /// <summary>
        /// Instantiates a new instance of the <see cref="RunProfileTag"/> class
        /// </summary>
        public RunProfileTag()
        {
            this.profileProvider = new RealTimeProfileProvider(); // No DI = sadness
            this.Condition = "True"; // Default condition to true so that user does not need to specify
        }

        /// <summary>
        /// Reloads the body elements in this tag
        /// </summary>
        private void ReloadBody()
        {
            this.Body = this.profileProvider.GetProfile(this.Path).Body;
        }

        /// <summary>
        /// Resets the is done flag
        /// </summary>
        protected override void OnResetCachedDone()
        {
            this.isDone = false;
        }

        /// <summary>
        /// Gets or sets the path of the profile to run
        /// </summary>
        [XmlAttribute("path")]
        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = FileUtility.GetAbsolutePath(ff14bot.Helpers.Utils.AssemblyDirectory, value);
                this.ReloadBody();
            }
        }

        /// <summary>
        /// Gets whether the tag is finished or not
        /// </summary>
        public override bool IsDone
        {
            get
            {
                return this.isDone;
            }
        }
    }
}
