using System;
using InterviewPrep.Generics;
using InterviewPrep.Generics.Entities;
using System.Linq;

namespace InterviewPrep.ConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            var context = new CarContext("123");
            using (var makeRepo = new CarRepository<Make>(context))
            {
                using (var modelRepo = new CarRepository<Model>(context))
                {
                    var make1 = new Make { Name = "Ford" };
                    var model1 = new Model { Name = "Mustang" };
                    makeRepo.Insert(make1);
                    modelRepo.Insert(model1);
                    makeRepo.Commit();
                    modelRepo.Commit();
                    var makes = makeRepo.GetAll().ToList();
                    var models = modelRepo.GetAll().ToList();
                }
            }
            Console.ReadLine();
        }
    }
}