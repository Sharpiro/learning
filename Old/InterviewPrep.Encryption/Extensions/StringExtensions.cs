using System.Collections.Generic;
using System.Text;

namespace InterviewPrep.Encryption.Extensions
{
    public static class StringExtensions
    {
        public static string AddSpacesToString(this string temp, List<int> spacePositions)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < temp.Length; i++)
            {
                if (spacePositions.Contains(i))
                    builder.Append($" {temp[i]}");
                else
                {
                    builder.Append(temp[i]);
                }
            }
            return builder.ToString();
        }
    }
}