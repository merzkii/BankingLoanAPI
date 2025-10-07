using Application.DTO.User;
using AutoMapper;
using Core.Interfaces;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Update
{
    class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponseDto>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            user.FirstName = request.UserData.FirstName;
            user.LastName = request.UserData.LastName;
            user.Username = request.UserData.Username;
            user.Age = request.UserData.Age;
            user.Email = request.UserData.Email;

            await _userService.UpdateAsync(user);

            return _mapper.Map<UserResponseDto>(user);
        }

    }
}
