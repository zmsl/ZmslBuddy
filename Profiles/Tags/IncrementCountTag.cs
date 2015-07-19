using Clio.XmlEngine;
using ZmslBuddy.Profiles.Tags.Provider;

namespace ZmslBuddy.Profiles.Tags
{
    [XmlElement("IncrementCount")]
    public class IncrementCountTag : BaseCountTag
    {
        /// <summary>
        /// Instantiates a new instance of the <see cref="IncrementCountTag"/> class
        /// </summary>
        /// <remarks>Class is instantiated with the <see cref="ProfileCountProvider"/> class because no DI sucks</remarks>
        public IncrementCountTag() 
            : base(new ProfileCountProvider())
        {
        }

        /// <summary>
        /// Returns oldValue + 1
        /// </summary>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        protected override int GetNewCount(int oldValue)
        {
            return oldValue + 1;
        }
    }
}
