using Application.DTO.User;
using Core.Entities;
using AutoMapper;   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Loan;

namespace Application.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponseDto>().ReverseMap();
            CreateMap<Loan, LoanResponseDto>().ReverseMap();
        }
    }
}
