using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
    {
        //using empty constractor for working without expression
        protected BaseSpecification() : this(null) { }

        public Expression<Func<T, bool>>? Criteria => criteria;

        public Expression<Func<T, object>>? OrderBy { get; private set; }  

        public Expression<Func<T, object>>? OrderByDescnding { get; private set; }

        public bool IsDistinct { get; private set; }

        //we'll deal with the setting of the expression inside here
        protected void AddOrderBy(Expression<Func<T,object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        protected void AddOrderByDescinding(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescnding = orderByDescExpression;
        }
        protected void ApplyDistinct()
        {
            IsDistinct = true;
        }
    }
    //this takes two type parameters instead of just one
    public class BaseSpecification<T, TResult>(Expression<Func<T, bool>> criteria) :
        BaseSpecification<T>(criteria), ISpecification<T, TResult>
    {
        protected BaseSpecification() : this(null!) { }

        public Expression<Func<T, TResult>>? Select { get; private set; }
        protected void AddSelect(Expression<Func<T,TResult>> selectExpression)
        {
            Select = selectExpression;
        }
    }

}
