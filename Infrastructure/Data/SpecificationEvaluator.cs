﻿using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> query,ISpecification<T> spec)//we can call this method without creating a new instance of our specification evaluator.
        {
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); // x=>x.Brand == brand
            }
            if(spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescnding != null) 
            { 
                query = query.OrderByDescending(spec.OrderByDescnding);
            }
            if (spec.IsDistinct)
            {
                query = query.Distinct();
            }
            return query;
        }
        public static IQueryable<TResult> GetQuery<TSpec,TResult>
            (IQueryable<T> query, ISpecification<T,TResult> spec)//we can call this method without creating a new instance of our specification evaluator.
        {
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); // x=>x.Brand == brand
            }
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescnding != null)
            {
                query = query.OrderByDescending(spec.OrderByDescnding);
            }
            var selectQuery = query as IQueryable<TResult>;
            if (spec.Select != null)
            {
                selectQuery = query.Select(spec.Select);
            }
            if (spec.IsDistinct)
            {
                selectQuery = selectQuery?.Distinct();
            }
            return selectQuery ?? query.Cast<TResult>();
        }
    }
}