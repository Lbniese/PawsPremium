namespace Paws.Core.Utilities
{
    public static class ConvertUtility
    {
        /// <summary>
        /// Convert a boolean value to "Yes" for true or "No" for false.
        /// </summary>
        public static string BoolToYesNoString(bool value)
        {
            return value == true ? "Yes" : "No";
        }

        /// <summary>
        /// Convert a TargetType value to a string representation.
        /// </summary>
        public static string TargetTypeToString(TargetType targetType)
        {
            string retStr = string.Empty;

            switch (targetType)
            {
                case TargetType.Me: retStr = "Me"; break;
                case TargetType.MyCurrentTarget: retStr = "My Current Target"; break;
                case TargetType.MyFocusTarget: retStr = "My Focus Target"; break;
            }

            return retStr;
        }
    }
}
