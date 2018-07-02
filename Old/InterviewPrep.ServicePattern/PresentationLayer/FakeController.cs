using InterviewPrep.ServicePattern.BusinessLogic;
using InterviewPrep.ServicePattern.DataLayer.Entities;
using System;

namespace InterviewPrep.ServicePattern.PresentationLayer
{
    public class FakeController
    {
        private readonly CarService _carService;
        private readonly GenericService<Manufacturer> _manufacturerService;

        public FakeController(CarService carService, GenericService<Manufacturer> manufacturerService)
        {
            _carService = carService;
            _manufacturerService = manufacturerService;
        }

        public int GetData()
        {
            var contextOneHash = _carService.UnitOfWork.Context.GetHashCode();
            var contextTwoHash = _manufacturerService.Context.GetHashCode();
            var isEqual = contextOneHash == contextTwoHash;
            if (!isEqual)
                throw new Exception();
            return contextOneHash;
        }
    }
}
