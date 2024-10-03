using System.Linq;
using System.Data.Entity;
using DAL_User;
using AutoMapper;
using BLL_User.Model;
using System;
using System.Linq.Expressions;

namespace BLL_User.BUS
{
    public class BaseServices<T> where T : class
    {
        public AssignmentEntities2 dbContext = new AssignmentEntities2();
        public IMapper Mapper;
        public BaseServices()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>().ReverseMap();
            });
            Mapper = config.CreateMapper();
        }

        public IQueryable<T> GetAllList(Expression<Func<T,bool>> query)
        {
            return dbContext.Set<T>().Where(query);
        }
        public IQueryable<T> GetAll()
        {
            return dbContext.Set<T>();
        }

        public void Insert(T entity)
        {
            dbContext.Set<T>().Add(entity);
            dbContext.SaveChanges();
        }

        public T GetById()
        {
            return dbContext.Set<T>().Find();
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
            if (entry.CurrentValues != entry.OriginalValues)
            {
                dbContext.Entry(entity).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            else
            {
                dbContext.Entry(entity).State = EntityState.Unchanged;
            }
        }

    }
}

