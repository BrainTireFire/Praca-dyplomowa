﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace pracadyplomowa
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyFilter<T>(
            this IQueryable<T> query,
            string? filterValue,
            Expression<Func<T, string>> propertySelector,
            bool exactMatch = false,
            bool caseInsensitive = true
        )
        {
            if (!string.IsNullOrEmpty(filterValue))
            {
                var parameter = propertySelector.Parameters[0];
                var property = propertySelector.Body as MemberExpression;

                if (property == null)
                {
                    throw new ArgumentException("Invalid property selector expression", nameof(propertySelector));
                }

                var propertyAccess = property;
                var filterExpression = BuildFilterExpression(propertyAccess, filterValue, exactMatch, caseInsensitive);

                var lambda = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
                query = query.Where(lambda);
            }

            return query;
        }

        public static IQueryable<T> ApplyBooleanFilter<T>(
           this IQueryable<T> query,
           bool? filterValue,
           Expression<Func<T, bool>> propertySelector,
           bool exactMatch = false,
           bool caseInsensitive = true
       )
        {
            if (filterValue != null)
            {
                var parameter = propertySelector.Parameters[0];
                var property = propertySelector.Body as MemberExpression;

                if (property == null)
                {
                    throw new ArgumentException("Invalid property selector expression", nameof(propertySelector));
                }

                var propertyAccess = property;
                var filterExpression = BuildBooleanFilterExpression(propertyAccess, (bool)filterValue);

                var lambda = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
                query = query.Where(lambda);
            }

            return query;
        }
        
        public static IQueryable<T> ApplyEnumFilter<T, TEnum>(
            this IQueryable<T> query,
            string filterValue,
            Expression<Func<T, TEnum>> propertySelector
        ) where TEnum : struct, Enum
        {
            if (!string.IsNullOrWhiteSpace(filterValue))
            {
                if (Enum.TryParse<TEnum>(filterValue, true, out var enumValue))
                {
                    var parameter = propertySelector.Parameters[0];
                    var property = propertySelector.Body as MemberExpression;

                    if (property == null)
                    {
                        throw new ArgumentException("Invalid property selector expression", nameof(propertySelector));
                    }

                    var constant = Expression.Constant(enumValue, typeof(TEnum));
                    var equality = Expression.Equal(property, constant);

                    var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);
                    query = query.Where(lambda);
                }
                else
                {
                    throw new ArgumentException($"Invalid enum value: {filterValue}", nameof(filterValue));
                }
            }

            return query;
        }

        public static IQueryable<T> ApplyIntFilter<T>(
            this IQueryable<T> query,
            int? filterValue,
            Expression<Func<T, int>> propertySelector
        )
        {
            if (filterValue != null)
            {
                var parameter = propertySelector.Parameters[0];
                var property = propertySelector.Body as MemberExpression;

                if (property == null)
                {
                    throw new ArgumentException("Invalid property selector expression", nameof(propertySelector));
                }

                var propertyAccess = property;
                var filterExpression = BuildIntFilterExpression(propertyAccess, (int)filterValue);

                var lambda = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
                query = query.Where(lambda);
            }

            return query;
        }

        private static Expression BuildFilterExpression(MemberExpression propertyAccess, string filterValue, bool exactMatch, bool caseInsensitive)
        {
            Expression filterExpression;

            if (caseInsensitive)
            {
                // Convert both property and filterValue to lower case for case-insensitive comparison
                var toLowerProperty = Expression.Call(propertyAccess, typeof(string).GetMethod("ToLower", Type.EmptyTypes)!);
                var toLowerFilter = Expression.Constant(filterValue.ToLower());
                filterExpression = exactMatch
                    ? Expression.Equal(toLowerProperty, toLowerFilter)
                    : Expression.Call(toLowerProperty, typeof(string).GetMethod("Contains", new[] { typeof(string) })!, toLowerFilter);
            }
            else
            {
                filterExpression = exactMatch
                    ? Expression.Equal(propertyAccess, Expression.Constant(filterValue))
                    : Expression.Call(propertyAccess, typeof(string).GetMethod("Contains", new[] { typeof(string) })!, Expression.Constant(filterValue));
            }

            return filterExpression;
        }

        private static Expression BuildIntFilterExpression(MemberExpression propertyAccess, int filterValue)
        {
            return Expression.Equal(propertyAccess, Expression.Constant(filterValue));
        }

        private static Expression BuildBooleanFilterExpression(MemberExpression propertyAccess, bool filterValue)
        {
            return Expression.Equal(propertyAccess, Expression.Constant(filterValue));
        }
    }
}
