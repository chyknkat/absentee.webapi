using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace Absentee.WebApi.Data
{
    public class Repository : IIRepository
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }
        public void Insert<DomainEntity>(DomainEntity entity)
        {
            _session.Save(entity);
            _session.Flush();
        }

        public DomainEntity Load<DomainEntity>(Expression<Func<DomainEntity, bool>> predicate)
        {
            return _session.Query<DomainEntity>().Where(predicate).FirstOrDefault();
        }

        public IQueryable<DomainEntity> Query<DomainEntity>(Expression<Func<DomainEntity, bool>> predicate)
        {
            var result = _session.Query<DomainEntity>().Where(predicate);
            return result;
        }

        public ICriteria QueryProjection<DomainEntity>(Expression<Func<DomainEntity, bool>> predicate, params string[] columns)
        {
            var queryDef = _session.CreateCriteria(typeof(DomainEntity));
            foreach (var column in columns)
            {
                queryDef.SetProjection(Projections.ProjectionList().Add(Projections.Property(column)));
            }
            return queryDef;
        }

        public IList<DomainEntity> GetAll<DomainEntity>()
        {
            _session.CacheMode = CacheMode.Normal;
            return _session.Query<DomainEntity>().ToList();
        }

        public DomainEntity Get<DomainEntity>(int id)
        {
            _session.CacheMode = CacheMode.Normal;
            return _session.Get<DomainEntity>(id);
        }

        public void Save<DomainEntity>(DomainEntity entity)
        {
            _session.SaveOrUpdate(entity);
            _session.Flush();
        }

        public void Delete<DomainEntity>(Expression<Func<DomainEntity, bool>> predicate)
        {
            var list = Query(predicate);
            if (list.Any())
            {
                foreach (var entity in list)
                {
                    _session.Delete(entity);
                }
                _session.Flush();
            }
        }

        public ICriteria CreateCriteria<T>() where T : class
        {
            return _session.CreateCriteria<T>();
        }
    }
}