using Newtonsoft.Json;
using System.Collections.Generic;

namespace InterviewPrep.Angular2FirstLook.Api
{
    public class VehiclesController
    {
        public object GetData()
        {
            var obj = new
            {
                data = new List<BaseEntity>(){
                new BaseEntity { Id = 1, Name = "Millennium Falcon" },
                new BaseEntity { Id = 2, Name = "Y-Wing Fighter" } }
            };
            return obj;
        }
    }

    public class BaseEntity
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}