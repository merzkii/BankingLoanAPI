using Application.DTO.User;
using Core.Entities;
using AutoMapper;   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Loan;
using Application.DTO.AdminUser;

namespace Application.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponseDto>().ReverseMap().ForMember(dest => dest.Loans, opt => opt.MapFrom(src => src.Loans)); ;
            CreateMap<Loan, LoanResponseDto>().ReverseMap().ForMember(dest => dest.User, opt => opt.Ignore()); 
            CreateMap<AdminUsers,AdminUserResponseDTO>().ReverseMap();
        }
    }
}
