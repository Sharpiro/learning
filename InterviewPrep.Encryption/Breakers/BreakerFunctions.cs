using System.Collections.Generic;

namespace InterviewPrep.Encryption.Breakers
{
    public class BreakerFunctions
    {
        public static bool HasCommonWords(string inputString, List<string> commonWordsList)
        {
            foreach (var item in commonWordsList)
            {
                if (inputString.Contains(string.Format("{0}", item)))
                    return true;
            }
            return false;
        }

        public static bool HasNineLetterWords(string inputString, List<string> list)
        {
            foreach (var item in list)
            {
                if (inputString.Contains($" {item}"))
                    return true;
            }
            return false;
        }
    }
}
