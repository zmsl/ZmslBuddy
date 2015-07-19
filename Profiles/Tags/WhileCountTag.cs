using System.Xml.Serialization;
using ff14bot.NeoProfiles;
using ZmslBuddy.Profiles.Tags.Enum;
using ZmslBuddy.Profiles.Tags.Provider;

namespace ZmslBuddy.Profiles.Tags
{
    [Clio.XmlEngine.XmlElement("WhileCount")]
    public class WhileCountTag : WhileTag
    {
        public WhileCountTag()
        {
            this.Conditional = GetConditionResult;
            this.Method = ComparisonMethod.Equal;
            this.Modifier = ComparisonModifier.Is;
        }

        [Clio.XmlEngine.XmlAttribute("name")]
        public string Name { get; set; }

        [Clio.XmlEngine.XmlAttribute("value")]
        public int Value { get; set; }

        [Clio.XmlEngine.XmlAttribute("method")]
        public ComparisonMethod Method { get; set; }

        [Clio.XmlEngine.XmlAttribute("modifier")]
        public ComparisonModifier Modifier { get; set; }

        [XmlIgnore()]
        public new string Condition { get; set; }

        /// <summary>
        /// Returns whether the value meets the criteria of the cached value
        /// </summary>
        /// <returns></returns>
        public bool GetConditionResult()
        {
            return ProfileCountProvider.EvaluateCountCondition(this.Name, this.Value, this.Method, this.Modifier);
        }
    }
}
