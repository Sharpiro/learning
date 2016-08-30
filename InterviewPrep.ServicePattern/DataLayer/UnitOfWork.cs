using InterviewPrep.ServicePattern.DataLayer.Entities;

namespace InterviewPrep.ServicePattern.DataLayer
{
    public class UnitOfWork
    {
        private readonly AutoContext _context;
        private GenericRepository<Car> _cars;
        private GenericRepository<Manufacturer> _manufacturers;

        public AutoContext Context { get { return _context; } }

        public GenericRepository<Car> Cars
        {
            get
            {
                if (_cars == null)
                {
                    _cars = new GenericRepository<Car>(_context);
                }
                return _cars;
            }
        }
        public GenericRepository<Manufacturer> Manufacturers
        {
            get
            {
                if (_manufacturers == null)
                {
                    _manufacturers = new GenericRepository<Manufacturer>(_context);
                }
                return _manufacturers;
            }
        }

        public UnitOfWork(AutoContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}