using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Persistence;
using Ecommerce.Application.Specifications.Users;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Auths.Users.Queries.PaginationUsers
{
    public class PaginationUsersQueryHandler : IRequestHandler<PaginationUsersQuery, PaginationVm<User>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public PaginationUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationVm<User>> Handle(PaginationUsersQuery request, CancellationToken cancellationToken)
        {

            UserSpecificationParams userSpecificationParams = new UserSpecificationParams
            {
                PageIndex = request.PageIndex,
                PageSize  = request.PageSize,
                Search    = request.Search,
                Sort      = request.Sort
            };

            UserSpecification spec                 = new UserSpecification(userSpecificationParams);
            IReadOnlyList<User> users              = await _unitOfWork.Repository<User>().GetAllWithSpec(spec);
            UserForCountingSpecification specCount = new UserForCountingSpecification(userSpecificationParams);
            int totalUsers                         = await _unitOfWork.Repository<User>().CountAsync(specCount);
            decimal rounded                        = Math.Ceiling(Convert.ToDecimal(totalUsers) / Convert.ToDecimal(request.PageSize));
            int totalPages                         = Convert.ToInt32(rounded);
            int usersByPage                        = users.Count();
            PaginationVm<User> pagination          = new PaginationVm<User>
            {
                Count        = totalUsers,
                Data         = users,
                PageCount    = totalPages,
                PageIndex    = request.PageIndex,
                PageSize     = request.PageSize,
                ResultByPage = usersByPage
            };

            return pagination;

        }
    }
}