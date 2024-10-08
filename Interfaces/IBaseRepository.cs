﻿using DentalClinic.Constants;
using System.Collections;
using System.Linq.Expressions;

namespace DentalClinic.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T? GetById(int id);

        T? GetById(string id);

        Task<T?> GetByIdAsync(int id);

        Task<T?> GetByIdAsync(string id);

        IQueryable<T> GetAllDeferred();

        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync();



        T? Find(Expression<Func<T,bool>> predicate, string[]? includes = null);

        Task<T?> FindAsync(Expression<Func<T, bool>> predicate, string[]? includes = null);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, string[]? includes = null);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, int take, int skip);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, int? take, int? skip, Expression<Func<T, object>>? orderBy = null, string orderByDirection = OrderBy.Ascending);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, string[]? includes = null);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, int skip, int take);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T,bool>> predicate,int? skip,int? take,Expression<Func<T,object>>? orderBy = null,string orderByDirection = OrderBy.Ascending);

        T Add(T entity);

        Task<T> AddAsync(T entity);

        IEnumerable<T> AddRange(IEnumerable<T> entities);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        T Update(T entity);

        void Delete(T entity);  

        void DeleteRange(IEnumerable<T> entities);

        void Attach(T entity);

        void AttachRange(IEnumerable<T> entities);

        int Count();

        int Count(Expression<Func<T, bool>> predicate);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);


    }
}
