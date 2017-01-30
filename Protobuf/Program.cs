using ProtoBuf;
using System.Collections.Generic;
using System.IO;

namespace Protobuf
{
    class Program
    {
        static void Main(string[] args)
        {
            var bytes = File.ReadAllBytes("./temp.bin");
            var list = new List<string>();
            foreach (var @byte in bytes)
            {
                var hex = string.Format("{0:X2}", @byte);
                list.Add(hex);
            }
            var hexList = string.Join(" ", list);
            hexList = hexList.Insert(0, "<");
            hexList = hexList.Insert(hexList.Length, ">");


            using (var file = File.OpenRead("./temp.bin"))
            {
                var person = Serializer.Deserialize<Person>(file);
            }

            using (var file = File.Create("./temp.bin"))
            {
                var person = new Person { Id = 1, Name = "David", Address = new Address { Line1 = "line1" } };
                //var obj = new { ABC123 = 150 };
                Serializer.Serialize(file, person);
            }
        }
    }

    [ProtoContract]
    class Person
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public Address Address { get; set; }
    }
    [ProtoContract]
    class Address
    {
        [ProtoMember(1)]
        public string Line1 { get; set; }
        [ProtoMember(2)]
        public string Line2 { get; set; }
    }
}