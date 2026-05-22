using backend.DTOs;
using backend.DTOs.Form;
using backend.Entities;
using backend.Enums;
using backend.Exceptions;
using backend.Repository;
using Microsoft.AspNetCore.Identity;

namespace backend.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTService _jwtService;


        public AuthService(IUserRepository userRepository, IJWTService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<LoggedUser> RegisterUser(RegisterUser registerUser)
        {
            if(await _userRepository.IsDuplicateEmail(registerUser.Email))
            {
                throw new BadRequestException("Acesta adresa de email este folosita de alt utilizator!");
            }

            var hasher = new PasswordHasher<User>();

            User user = new User
            {
                Name = registerUser.Name,
                Email = registerUser.Email,
                Password = hasher.HashPassword(new User(), registerUser.Password),
                Role = UserRole.Customer,
            };

            await _userRepository.AddUserAsync(user);

            return new LoggedUser
            {
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Token = _jwtService.GenerateToken(user)
            };
        }

        public async Task<LoggedUser> LoginUser(LoginUser loginUser)
        {
            var user = await _userRepository.GetUserByEmail(loginUser.Email);

            if (user == null)
            {
                throw new BadRequestException("Adresa de email sau parola sunt gresite!");
            }

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, loginUser.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Adresa de email sau parola sunt gresite!");
            }

            return new LoggedUser
            {
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Token = _jwtService.GenerateToken(user)
            };
        }

        //public async Task<UserDTO> GetUserData(int userId)
        //{
        //    var user = await _userRepository.GetUserById(userId);

        //    if (user == null)
        //    {
        //        throw new NotFoundException("User not found!");
        //    }

        //    return user;
        //}
    }
}
