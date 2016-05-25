using System;

namespace InterviewPrep.OdeToFoodRc2.DataAccess
{
    public static class ContextBuilder
    {
        public static FoodContext GetFoodContextWithParams(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(null, nameof(connectionString));
            var context = new FoodContext(connectionString, ConnectionType.Sql);
            return context;
        }
    }

    public enum ConnectionType
    {
        None,
        Sql,
        Mongo,
        SqLite
    }
}
