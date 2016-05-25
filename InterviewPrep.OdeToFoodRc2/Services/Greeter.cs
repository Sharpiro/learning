using Microsoft.Extensions.Configuration;

namespace InterviewPrep.OdeToFoodRc2
{
    public interface IGreeter
    {
        string GetGreeting();
    }

    public class ConfigurationGreeter : IGreeter
    {
        private readonly IConfiguration _config;

        public ConfigurationGreeter(IConfiguration config)
        {
            _config = config;
        }

        public string GetGreeting()
        {
            var greeting = _config["greeting"];
            return greeting;
        }
    }

    public class ConstantGreeter : IGreeter
    {
        public string GetGreeting()
        {
            const string greeting = "this is a constant greeting";
            return greeting;
        }
    }
}