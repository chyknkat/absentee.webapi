using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace Absentee.WebApi.Data
{
    public interface IIRepository
    {
        void Insert<T>(T entity);

        T Load<T>(Expression<Func<T, bool>> predicate);

        IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate);

        ICriteria QueryProjection<T>(Expression<Func<T, bool>> predicate, params string[] columns);

        IList<T> GetAll<T>();
        T Get<T>(int id);

        void Save<T>(T entity);

        void Delete<T>(Expression<Func<T, bool>> predicate);

        ICriteria CreateCriteria<T>() where T : class;
    }
}
