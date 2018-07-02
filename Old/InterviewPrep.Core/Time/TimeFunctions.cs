using System;
using System.IO;
using System.Linq;

namespace InterviewPrep.Core.Time
{
    public static class TimeFunctions
    {
        public static string GetRemainingTime(string departureTimeString)
        {
            var now = DateTime.Now;
            var departureTime = DateTime.Parse(departureTimeString);
            var remainingTime = departureTime - now;
            return remainingTime.ToString(@"hh\:mm");
        }

        public static string GetRemainingTimeHard(string departureTimeString)
        {
            departureTimeString = departureTimeString.ToLowerInvariant();
            var now = DateTime.Now;
            var amIndex = departureTimeString.IndexOf("am", StringComparison.Ordinal);
            var isAm = amIndex > 0;
            var pmIndex = departureTimeString.IndexOf("pm", StringComparison.Ordinal);
            var isPm = pmIndex > 0;
            if (isAm)
                departureTimeString = departureTimeString.Remove(amIndex, 2);
            else if (isPm)
                departureTimeString = departureTimeString.Remove(pmIndex, 2);
            var split = departureTimeString.Split(':');
            if (split.All(s => s != null) && split.Length != 2)
                throw new InvalidDataException("the time string must only contain hours, minutes, " +
                    "and am/pm in the format '9:20pm' or '21:20'");
            int hours;
            var isNumber = int.TryParse(split.FirstOrDefault(), out hours);
            if (!isNumber)
                throw new InvalidCastException("a valid hours value could not be extracted from the string");
            hours = isPm ? hours + 12 : hours;
            int minutes;
            isNumber = int.TryParse(split.LastOrDefault(), out minutes);
            if (!isNumber)
                throw new InvalidCastException("a valid minutes value could not be extracted from the string");
            var newHours = hours - now.Hour;
            var newMinutes = minutes - now.Minute;
            if (newMinutes < 0)
            {
                newMinutes += 60;
                newHours -= 1;
            }
            return $"{newHours}:{newMinutes}";
        }

        public static string GetRemainingTime(DateTime departureTime)
        {
            var now = DateTime.Now;
            var remainingTime = departureTime - now;
            return remainingTime.ToString(@"hh\:mm");
        }
    }
}
