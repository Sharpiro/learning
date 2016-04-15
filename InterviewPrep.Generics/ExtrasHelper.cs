using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPrep.Generics
{
    public class ExtrasHelper
    {
        public ExtrasHelper()
        {
            var input = "Step1";
            var value = input.ParseEnum<Steps>();

            var list = new List<Item>
            {
                new Item<int>(),
                new Item<object>()
            };
            var genericItem = ((Item<int>)list.FirstOrDefault())?.Property2;
        }
    }

    public enum Steps
    {
        Step1,
        Step2,
        Step3
    }

    public static class Extensions
    {
        public static T ParseEnum<T>(this string input) where T : struct
        {
            var value = (T)Enum.Parse(typeof(T), input);
            return value;
        }
    }

    public class Item<T> : Item
    {
        public T Property2 { get; set; }
    }

    public class Item
    {
        public int Property1 { get; set; }
    }
}
