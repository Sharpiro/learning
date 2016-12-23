namespace InterviewPrep.Core.Inheritance
{
    public class Parent
    {
        public virtual int Get()
        {
            return Get<int>();
        }

        public virtual int Get<T>()
        {
            return 1;
        }
    }

    public class Child : Parent
    {
        //public override int Get()
        //{
        //    return Get<int>();
        //}

        public override int Get<T>()
        {
            return 3;
        }
    }
}