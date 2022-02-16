using System.Linq.Expressions;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
	private readonly DbSet<T> _dbSet;

	protected RepositoryBase(RepositoryContext repositoryContext)
	{
		_dbSet = repositoryContext.Set<T>();
	}
	
	public IQueryable<T> FindAll(bool trackChanges)
	{
		return !trackChanges ? _dbSet.AsNoTracking() : _dbSet;
	}

	public IQueryable<T> Find(Expression<Func<T, bool>> expression, bool trackChanges)
	{
		return !trackChanges ? _dbSet.Where(expression).AsNoTracking() : _dbSet.Where(expression);
	}

	public void Create(T entity)
	{
		_dbSet.Add(entity);
	}

	public void Update(T entity)
	{
		_dbSet.Update(entity);
	}

	public void Delete(T entity)
	{
		_dbSet.Remove(entity);
	}
}