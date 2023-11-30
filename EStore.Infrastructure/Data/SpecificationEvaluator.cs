using System;
using EStore.Core.Entities;
using EStore.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace EStore.Infrastructure.Data
{
	public class SpecificationEvaluator<T> where T : BaseEntity
	{
		public static IQueryable<T> GetQuery(IQueryable<T> input,ISpecification<T> spec)
		{
			var query = input;

			if (spec.Criteria != null)
			{
				query = query.Where(spec.Criteria);
			}

			if(spec.OrderBy != null)
			{
				query = query.OrderBy(spec.OrderBy);
			}

			if(spec.OrderByDescending != null)
			{
				query = query.OrderByDescending(spec.OrderByDescending);
			}

			query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));


			return query;
		}
	}
}

