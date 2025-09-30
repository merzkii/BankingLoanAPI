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
            CreateMap<User, UserResponseDto>().ReverseMap();
            CreateMap<Loan, LoanResponseDto>().ReverseMap();
            CreateMap<AdminUsers,AdminUserResponseDTO>().ReverseMap();
        }
    }
}
