namespace InterviewPrep.Core.Inheritance
{
    public class Base
    {
        public virtual int GetOverride()
        {
            return 0;
        }

        public virtual int GetNew()
        {
            return 0;
        }
    }

    public class ChildOne : Base
    {
        public override int GetOverride()
        {
            return 1;
        }

        public new int GetNew()
        {
            return 1;
        }
    }

    public class ChildTwo : Base
    {
        public override int GetOverride()
        {
            return 1;
        }

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public int GetNew()
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            return 1;
        }
    }
}