using System;
using System.Data.Entity;
using System.Linq;

namespace InterviewPrep.Concepts.ChangeTracking
{
    public class ChangeTracker
    {
        private readonly AutoContext _context;

        public ChangeTracker(AutoContext context)
        {
            _context = context;
        }

        public void CheckChanges()
        {
            var changes = _context.ChangeTracker.Entries().ToList();
            foreach (var change in changes)
            {
                if (change.State == EntityState.Modified)
                {
                    foreach (var propertyName in change.CurrentValues.PropertyNames)
                    {
                        var changedEntity = change.Entity;
                        var changedValue = change.CurrentValues.GetValue<object>(propertyName);
                        var temp = changedEntity.GetType();
                    }
                }
            }
        }

        public T GetTrackedObject<T>() where T : class
        {
            var instance = Activator.CreateInstance<T>();
            _context.Set<T>().Attach(instance);
            return instance;
        }

        public void TrackObject<T>(T instance) where T : class
        {
            var temp = _context.Set<T>().GetType();
            _context.Set<T>().Attach(instance);
        }
    }
}
