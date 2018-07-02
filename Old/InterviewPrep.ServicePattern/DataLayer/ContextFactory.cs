namespace InterviewPrep.ServicePattern.DataLayer
{
    public class ContextFactory
    {
        public AutoContext CreateContext()
        {
            return new AutoContext();
        }
    }
}
