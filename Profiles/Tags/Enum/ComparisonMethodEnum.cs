using System;

namespace ZmslBuddy.Profiles.Tags.Enum
{
    [Flags]
    public enum ComparisonMethod
    {
        LessThan = 1,
        GreaterThan = 2,
        Equal = 4,
        LessThanOrEqual = 8,
        GreaterThanOrEqual = 16
    }
}
