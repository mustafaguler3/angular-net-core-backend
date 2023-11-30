using System;
using EStore.Core.Specifications;

namespace EStore.Core.Interfaces
{
	public interface IGenericRepository<T> where T :class
	{
		Task<T> GetByIdAsync(int id);

		Task<IReadOnlyList<T>> ListAllAsync();

		Task<T> GetEntityWithSpec(ISpecification<T> spec);

		Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

		Task<int> CountAsync(ISpecification<T> spec);

		void Add(T entity);

		void Update(T entity);

		void Delete(T entity);
	}
}

