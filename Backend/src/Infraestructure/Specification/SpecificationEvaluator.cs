using Ecommerce.Application.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Specification
{
    public class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            if (spec.Criteria != null)
            {
                inputQuery = inputQuery.Where(spec.Criteria);
            }

            /*PAGINACIÓN INICIO*/
            if (spec.OrderBy != null)
            {
                inputQuery = inputQuery.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescending != null)
            {
                inputQuery = inputQuery.OrderBy(spec.OrderByDescending);
            }

            if (spec.IsPagingEnable)
            {
                // Skip indica desde que posición va a empezar a tomar registros de la tabla
                // Take indica cuantos elementos quieres tomar desde que posición
                inputQuery = inputQuery.Skip(spec.Skip).Take(spec.Take);
            }
            /*PAGINACIÓN FIN*/

            inputQuery = spec.Includes!
            .Aggregate(inputQuery, (current, include) => current.Include(include))
            .AsSplitQuery()
            .AsNoTracking();

            return inputQuery;
        }
    }
}