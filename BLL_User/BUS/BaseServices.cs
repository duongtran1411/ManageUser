using System.Linq;
using System.Data.Entity;
using DAL_User;
using AutoMapper;
using System;
using System.Linq.Expressions;

namespace BLL_User.BUS
{
    public class BaseServices<T> where T : class
    {
        private AssignmentEntities2 dbContext = new AssignmentEntities2();
        public IMapper Mapper;
        public BaseServices()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            });
            Mapper = config.CreateMapper();
        }

        public IQueryable<T> GetAllList(Expression<Func<T, bool>> query)
        {
            return dbContext.Set<T>().Where(query);
        }

        public void Insert(T entity)
        {
            dbContext.Set<T>().Add(entity);
            dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Attach(entity);
            var entry = dbContext.Entry(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();

        }

        public T FirstOrDeFault(Expression<Func<T, bool>> query)
        {
            return dbContext.Set<T>().FirstOrDefault(query);
        }

    }
}

