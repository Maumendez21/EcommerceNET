using AutoMapper;
using Ecommerce.Application.Features.Auths.Countries.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Auths.Countries.Queries.GetCountrys
{
    public class GetCountrysQueryHandler : IRequestHandler<GetCountrysQuery, IReadOnlyList<CountryVm>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCountrysQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CountryVm>> Handle(GetCountrysQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<Country> countries = await _unitOfWork.Repository<Country>().GetAsync(
                null, 
                x => x.OrderBy(y => y.CountryName),
                string.Empty,
                false
            );

            return _mapper.Map<IReadOnlyList<CountryVm>>(countries);
        }
    }
}