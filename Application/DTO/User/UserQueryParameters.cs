using Application.DTO.Common;
using Core.Enums;

namespace Application.DTO.User
{
    public record UserQueryParameters : PagedQueryParameters
    {
        public string? Search { get; set; }
        public UserType? UserType { get; set; }
        public bool? IsBlocked { get; set; }
    }
}
