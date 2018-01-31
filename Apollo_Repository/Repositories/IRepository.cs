using System;
using System.Collections.Generic;
using System.Linq;

namespace Apollo_Repository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Remove(T entity);
        T Find(T entity);
        T Find(ulong id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Update(T entity);
    }
}
