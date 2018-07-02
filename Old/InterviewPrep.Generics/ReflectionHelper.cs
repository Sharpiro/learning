using System;
using InterviewPrep.Generics.Entities;

namespace InterviewPrep.Generics
{
    public class ReflectionHelper
    {
        public void Do()
        {
            var employee = new Employee();
            var employeeType = typeof(Employee);
            var methodInfo = employeeType.GetMethod("Speak");
            methodInfo = methodInfo.MakeGenericMethod(typeof(DateTime));
            methodInfo.Invoke(employee, null);
        }
    }
}
