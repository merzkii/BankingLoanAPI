using Application.DTO.AdminUser;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admins.Queries.GetById
{
    public class GetAdminByIdQueryHandler: IRequestHandler<GetAdminByIdQuery, AdminUserResponseDTO>
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IMapper _mapper;
        public GetAdminByIdQueryHandler(IAdminUserRepository adminUserRepository, IMapper mapper)
        {
            _adminUserRepository = adminUserRepository;
            _mapper = mapper;
        }

        public async Task<AdminUserResponseDTO> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
        {
            var adminUser = await _adminUserRepository.GetByIdAsync(request.AdminId);
            if (adminUser == null)
            {
                throw new NotFoundException("Admin user not found.");
            }
            return _mapper.Map<AdminUserResponseDTO>(adminUser);
        }
    }
}
