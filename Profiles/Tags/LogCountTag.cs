using System.Globalization;
using Clio.XmlEngine;
using TreeSharp;

namespace ZmslBuddy.Profiles.Tags
{
    [XmlElement("LogCount")]
    public class LogCountTag : BaseCacheTag<int>
    {
        /// <summary>
        /// Logs the value of the named count cache
        /// </summary>
        /// <returns></returns>
        protected override Composite CreateBodyBehavior()
        {
            return new Action(
                (r) => Log(string.Format("Count \"{0}\" = {1}", this.Name, ObjectCache<int>.Instance[this.Name].ToString(CultureInfo.InvariantCulture)))
            );
        }
    }
}
