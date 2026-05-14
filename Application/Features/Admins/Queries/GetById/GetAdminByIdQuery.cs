using Application.DTO.AdminUser;
using MediatR;

namespace Application.Features.Admins.Queries.GetById
{
    public record GetAdminByIdQuery(int AdminId) : IRequest<AdminUserResponseDTO>;

}
