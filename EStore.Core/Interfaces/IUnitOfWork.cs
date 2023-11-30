using System;
using EStore.Core.Entities;

namespace EStore.Core.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<T> Repository<T>() where T : BaseEntity;

		Task CompleteAsync();
	}
}

