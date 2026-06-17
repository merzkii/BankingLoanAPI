using Application.DTO.User;
using AutoMapper;
using Application.DTO.Loan;
using Application.DTO.AdminUser;
using Core.Entities.Admins;
using Core.Entities.Users;
using Core.Entities.Loans;
using Application.DTO.Repayment;

namespace Application.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponseDto>()
                .ReverseMap()
                .ForMember(dest => dest.Loans, opt => opt.MapFrom(src => src.Loans))
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<Loan, LoanResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.LoanId))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User!.FirstName + " " + src.User!.LastName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User!.Email))
                .ForMember(dest => dest.TermMonths, opt => opt.MapFrom(src => src.Period))
                .ReverseMap()
                .ForMember(dest => dest.LoanId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.User, opt => opt.Ignore());
              

            CreateMap<AdminUsers, AdminUserResponseDTO>()
                .ReverseMap()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<LoanRepayment, RepaymentResponseDto>();
        }
    }
}
