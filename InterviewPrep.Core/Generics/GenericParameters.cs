using System;

namespace InterviewPrep.Core.Generics
{
    /// <summary>
    /// Open: 1 or more generic type arguments' actual type is still unknown
    /// Closed: All generic type arguments have a known type given to them.
    /// Bound: Type Arguments are specified, even if the implementation is still unknown.
    /// Unbound: Generic class used without type arguments.  Only valid with "typeof"
    /// </summary>
    /// <typeparam name="TOne"></typeparam>
    /// <typeparam name="TTwo"></typeparam>
    public class BasicClass<TOne, TTwo> : IBasicClass
    {
        public static void Create()
        {
            IBasicClass instance;
            Type type;

            //Null
            //Bound
            instance = new BasicClass();

            //Open
            //Bound
            instance = new BasicClass<TOne, TTwo>();

            //Open
            //Bound
            instance = new BasicClass<int, TTwo>();

            //Closed
            //Bound
            instance = new BasicClass<int, string>();
            
            //Null
            //Unbound
            type = typeof(BasicClass<,>);

        }
    }

    public class BasicClass : IBasicClass
    {

    }

    public interface IBasicClass
    {

    }
}