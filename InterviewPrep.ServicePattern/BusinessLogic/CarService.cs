using InterviewPrep.ServicePattern.DataLayer;
using System.Collections.Generic;
using System.Linq;
using InterviewPrep.ServicePattern.DataLayer.Entities;

namespace InterviewPrep.ServicePattern.BusinessLogic
{
    public class CarService
    {
        //private readonly ContextFactory _contextFactory;
        private readonly UnitOfWork _unitOfWork;

        public CarService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_contextFactory = contextFactory;
        }

        public Car Get(int manufacturerId)
        {
            return _unitOfWork.Cars.Get(manufacturerId);
        }

        public Car Get(string manufacturerName)
        {
            return _unitOfWork.Cars.GetAll().FirstOrDefault(c => c.Manufacturer.Name.ToLower() == manufacturerName.ToLower());
        }

        public IEnumerable<Car> GetAllByManufacturer(string manufacturer)
        {
            return _unitOfWork.Cars.GetAll().Where(c => c.Manufacturer.Name.ToLower() == manufacturer.ToLower()).ToList();
        }

        public void AddCarToManfacturer(string newCarName, int manufacturerId)
        {
            var manufacturer = _unitOfWork.Manufacturers.Get(manufacturerId);
            manufacturer.AddCar(new Car { Name = newCarName, ManufacturerId = manufacturerId });
            _unitOfWork.Commit();
        }
    }
}