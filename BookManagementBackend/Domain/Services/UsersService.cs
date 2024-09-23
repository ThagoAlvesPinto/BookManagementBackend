using BookManagementBackend.Classes;
using BookManagementBackend.Domain.Extensions;
using BookManagementBackend.Domain.Interfaces.Repositories;
using BookManagementBackend.Domain.Interfaces.Services;
using BookManagementBackend.Domain.Models;
using BookManagementBackend.Domain.Models.Requests;
using BookManagementBackend.Domain.Models.Responses;

namespace BookManagementBackend.Domain.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<ServiceResult<Users>> Login(string email, string password)
        {
            try
            {
                Users? user = await _usersRepository.GetUser(email);

                if (user is null)
                    return new(false, "Usuário não encontrado!");

                bool passwordCheck = BCrypt.Net.BCrypt.Verify(password, user.Password);

                if (!passwordCheck)
                    return new(false, "Senha incorreta!");

                return new(user);
            }
            catch (Exception ex)
            {
                return new(false, ex.GetExceptionMessage(), true);
            }            
        }

        public async Task<ServiceResult<List<UserResponse>>> GetAllUsers()
        {
            try
            {
                List<UserResponse> users = (await _usersRepository.GetAllUsers()).Select(x => new UserResponse(x)).ToList();

                return new(users);
            }
            catch (Exception ex)
            {
                return new(false, ex.GetExceptionMessage(), true);
            }            
        }

        public async Task<ServiceResult> AddUser(AddUserRequest userRequest)
        {

           try
            {
                Users? user = await _usersRepository.GetUser(userRequest.Email);

                if (user is not null)
                    return new(false, "Usuário já cadastrado!");

                string temporaryPassword = GenerateTemporaryPassword();

                user = new()
                {
                    Email = userRequest.Email,
                    FirstName = userRequest.FirstName,
                    LastName = userRequest.LastName,
                    Password = BCrypt.Net.BCrypt.HashPassword(temporaryPassword),
                    IsAdministrator = userRequest.IsAdministrator,
                    Active = true
                };

                await _usersRepository.AddUser(user);

                return new(true, "Usuário cadastrado com sucesso !");
            }
            catch (Exception ex)
            {
                return new(false, ex.GetExceptionMessage(), true);
            }
        }

        private string GenerateTemporaryPassword()
        {
            Random random = new();

            string password = "";

            for (int i = 0; i < 8; i++)
            {
                password += (char)random.Next(65, 90);
            }

            return password;
        }
    }
}
