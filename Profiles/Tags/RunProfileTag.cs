using System.IO;
using Clio.XmlEngine;
using ff14bot.NeoProfiles;
using ZmslBuddy.Profiles.Tags.Extension;

namespace ZmslBuddy.Profiles.Tags
{
    [XmlElement("RunProfile")]
    public class RunProfileTag : IfTag
    {
        private string path;
        private bool isDone;

        /// <summary>
        /// Instantiates a new instance of the <see cref="RunProfileTag"/> class
        /// </summary>
        public RunProfileTag()
        {
            this.Condition = "True"; // Default condition to true so that user does not need to specify
        }

        /// <summary>
        /// Reloads the body elements in this tag
        /// </summary>
        private void ReloadBody()
        {
            this.Body = NeoProfile.Load(this.path).GetOrderBody();
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

                // Load the body
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
