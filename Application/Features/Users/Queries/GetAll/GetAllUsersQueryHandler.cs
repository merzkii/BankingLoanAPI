using Application.DTO.Common;
using Application.DTO.User;
using AutoMapper;
using MediatR;

namespace Application.Features.Users.Queries.GetAll
{
    class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PagedResult<UserResponseDto>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<PagedResult<UserResponseDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var (users, totalCount) = await _userService.GetPagedAsync(request);
            var items = _mapper.Map<List<UserResponseDto>>(users);

            return new PagedResult<UserResponseDto>(items, totalCount, request.PageNumber, request.PageSize);
        }
    }
}

