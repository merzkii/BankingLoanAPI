using Application.DTO.AdminUser;
using Application.DTO.Loan;
using Application.DTO.Repayment;
using Application.DTO.User;
using Core.Entities.Admins;
using Core.Entities.Loans;
using Core.Entities.Users;
using Mapster;

namespace Application.Mapping
{
    public class MapsterConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, UserResponseDto>();
            config.NewConfig<UserResponseDto, User>()
                .Ignore(dest => dest.Password!)
                .Ignore(dest => dest.Loans!);

                 config.NewConfig<Loan, LoanResponseDto>()
                .Map(dest => dest.Id, src => src.LoanId)
                .Map(dest => dest.UserFullName, src => src.User != null ? src.User.FirstName + " " + src.User!.LastName : string.Empty)
                .Map(dest => dest.UserEmail, src => src.User != null ?  src.User!.Email : string.Empty)
                .Map(dest => dest.TermMonths, src => src.Period);
            config.NewConfig<LoanResponseDto, Loan>()
                .Map(dest => dest.LoanId, src => src.Id)
                .Ignore(dest => dest.User!);

            config.NewConfig<AdminUsers, AdminUserResponseDTO>();
            config.NewConfig<AdminUserResponseDTO, AdminUsers>()
                .Ignore(dest => dest.PasswordHash!);

            config.NewConfig<LoanRepayment, RepaymentResponseDto>();
        }
    }
}