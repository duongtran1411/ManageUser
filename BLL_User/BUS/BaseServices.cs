using System.Linq;
using System.Data.Entity;
using DAL_User;
using AutoMapper;
using System;
using System.Linq.Expressions;
using BLL_User.Model;

namespace BLL_User.BUS
{
    public class BaseServices<T> where T : class
    {
        protected AdminPageEntities dbContext = new AdminPageEntities();
        public IMapper Mapper;
        public BaseServices()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User,UserDTO>().ReverseMap();
                cfg.CreateMap<Permission, PermissionDTO>().ReverseMap();
                cfg.CreateMap<Role, RoleDTO>().ReverseMap();
            });
            Mapper = config.CreateMapper();
        }

        public IQueryable<T> GetAll()
        {
            return dbContext.Set<T>();
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

