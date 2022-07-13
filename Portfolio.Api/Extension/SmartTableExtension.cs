using System;
using System.Linq;
using System.Linq.Expressions;

namespace Portfolio.Api.Extension
{
    public static class SmartTableExtension
    {
        public static SmartTableResult<TResult> ToSmartTableResult<TModel, TResult>(this IQueryable<TModel> query, SmartTableParam param, Expression<Func<TModel, TResult>> selector)
        {
            var totalRecord = query.Count();
            query = query.AppendSortAndPagingation(param);
            var items = query.Select(selector).ToList();

            return new SmartTableResult<TResult>
            {
                Items = items,
                TotalRecord = totalRecord,
                NumberOfPages = (int)Math.Ceiling((double)totalRecord / param.Pagination.Number)
            };
        }
        private static IQueryable<TModel> AppendSortAndPagingation<TModel>(this IQueryable<TModel> query, SmartTableParam param)
        {
            if (param.Pagination.Number <= 0)
            {
                param.Pagination.Number = 10;
            }

            if (!string.IsNullOrWhiteSpace(param.Sort.Predicate))
            {
                query = query.OrderByName(param.Sort.Predicate, param.Sort.Reverse);
            }
            else
            {
                query = query.OrderByName("Id", true);
            }

            query = query
                .Skip(param.Pagination.Start)
                .Take(param.Pagination.Number);

            return query;
        }
    }
}
