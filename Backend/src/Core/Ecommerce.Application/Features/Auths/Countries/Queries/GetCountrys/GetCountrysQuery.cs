using Ecommerce.Application.Features.Auths.Countries.Vms;
using MediatR;

namespace Ecommerce.Application.Features.Auths.Countries.Queries.GetCountrys
{
    public class GetCountrysQuery : IRequest<IReadOnlyList<CountryVm>>
    {
        
    }
}