using InterviewPrep.Generics.Entities;
using InterviewPrep.Generics.RepositoryPattern;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPrep.Generics
{
    public class RepositoryUser
    {
        public void UseRepositoryPattern()
        {
            using (var makeRepo = new CarRepository<Make>(new CarContext()))
            {
                var make1 = new Make { Name = "Ford" };
                makeRepo.Insert(make1);
                makeRepo.Commit();
                IEnumerable<Base> makes = makeRepo.GetAll().ToList();
                IRepository<Base> baseRepo = (IRepository<Base>)makeRepo;
                DoCovariance(makeRepo);
                DoContravariance(baseRepo);
            }
            using (var modelRepo = new CarRepository<Model>(new CarContext()))
            {
                var model1 = new Model { Name = "Mustang" };
                modelRepo.Insert(model1);
                modelRepo.Commit();
                var models = modelRepo.GetAll().ToList();
            }
        }

        /// <summary>
        /// Example of interface co-variance "out" modifier
        /// </summary>
        /// <param name="baseRepo"></param>
        private void DoCovariance(IReadOnlyRepository<Base> baseRepo)
        {
            var allBaseItems = baseRepo.GetAll();
        }

        /// <summary>
        /// Example of interface contra-variance "in" modifier
        /// </summary>
        /// <param name="makeRepo"></param>
        private void DoContravariance(IWriteOnlyRepository<Make> makeRepo)
        {
            makeRepo.Insert(new Make());
            makeRepo.Commit();
        }
    }
}
