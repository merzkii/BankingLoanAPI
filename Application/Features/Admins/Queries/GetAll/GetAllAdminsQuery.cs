using Application.DTO.AdminUser;
using MediatR;

namespace Application.Features.Admins.Queries.GetAll
{
    public record GetAllAdminsQuery : IRequest<List<AdminUserResponseDTO>>;
}
