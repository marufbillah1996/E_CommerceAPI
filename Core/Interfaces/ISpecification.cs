using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    //Key Differences
    //Focus: Specification Pattern focuses on business rules and criteria, while Generics focus on type safety and reusability.

    //Usage: Specification is used for defining complex conditions and business logic, whereas Generics are used for creating versatile and type-safe code structures.

    //Context: Specification is more about behavior and logic, whereas Generics are about handling different data types efficiently.

    //Specification pattern is that we describe the query in an object. We decided what expressions we want to pass to our database via our repository inside an object. And we pass this object to the repository.then the repository passes it to an evaluator.The evaluator works out the expression tree, and then we passs that expression tree to the database via entity Framework to get the data that we're interested in.

    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? Criteria { get; } //just for filtering or work as where 
        Expression<Func<T, object>>? OrderBy { get; } //for sorting
        Expression<Func<T, object>>? OrderByDescnding { get; } //for sorting
        bool IsDistinct {  get; }
        //pagination
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
        IQueryable<T> ApplyCriteriaI(IQueryable<T> query);
    }
    /// <summary>
    /// This part one where we enhance our ISpecification to not just take type parameter , but also one that allow us to return type to the one that's using the specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface ISpecification<T, TResult> : ISpecification<T>
    {
        Expression<Func<T, TResult>>? Select { get; }
    }
}
