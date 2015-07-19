using Clio.XmlEngine;
using ZmslBuddy.Profiles.Tags.Provider;

namespace ZmslBuddy.Profiles.Tags
{
    [XmlElement("ResetCount")]
    public class ResetCountTag : BaseCountTag
    {
        /// <summary>
        /// Instantiates a new instance of the <see cref="ResetCountTag"/> class
        /// </summary>
        /// <remarks>Class is instantiated with the <see cref="ProfileCountProvider"/> class because no DI sucks</remarks>
        public ResetCountTag(ICountProvider countProvider) : base(countProvider)
        {
        }

        /// <summary>
        /// Returns 0
        /// </summary>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        protected override int GetNewCount(int oldValue)
        {
            return 0;
        }
    }
}
