﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Linq.Expressions;

namespace SkyDal
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal SkyWebContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(SkyWebContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        //使用说明
        //var categorys = unitOfWork.categorysRepository.Get(filter: u => u.CategoryParentID == ParentID && u.id=id);
        //var categorys = unitOfWork.categorysRepository.Get(orderBy: q =>q.OrderBy(u=>u.CategoryId));
        //var categorys = unitOfWork.categorysRepository.Get(orderBy: q =>q.OrderByDescending(u=>u.CategoryId));
        

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        //使用原生的sql语句进行查询
        public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return dbSet.SqlQuery(query, parameters).ToList();
        }
        //使用原生的sql语句更新一个字段
        public virtual void UpdateWithRawSql(string sql)
        {
            context.Database.ExecuteSqlCommand(sql);
        
        }

    }
}