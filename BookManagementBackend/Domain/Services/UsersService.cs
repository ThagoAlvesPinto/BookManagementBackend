using BookManagementBackend.Classes;
using BookManagementBackend.Domain.Extensions;
using BookManagementBackend.Domain.Interfaces.Repositories;
using BookManagementBackend.Domain.Interfaces.Services;
using BookManagementBackend.Domain.Interfaces.Services.External;
using BookManagementBackend.Domain.Models;
using BookManagementBackend.Domain.Models.Requests;
using BookManagementBackend.Domain.Models.Responses;

namespace BookManagementBackend.Domain.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IEmailService _emailService;

        public UsersService(IUsersRepository usersRepository, IEmailService emailService)
        {
            _usersRepository = usersRepository;
            _emailService = emailService;
        }

        public async Task<ServiceResult<Users>> Login(string email, string password)
        {
            try
            {
                Users? user = await _usersRepository.GetUser(email);

                if (user is null)
                    return new(false, "Usuário não encontrado!");

                if (!user.Active)
                    return new(false, "Usuário desativado!");

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

        public async Task<ServiceResult<UserResponse>> GetUser(int userId)
        {
            try
            {
                Users? user = await _usersRepository.GetUser(userId);

                if (user is null)
                    return new(false, "Usuário não encontrado!");

                UserResponse userResponse = new(user);

                return new(userResponse);
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

                try
                {
                    string corpoEmail = MontaCorpoEmailSenhaTemporaria(user.FirstName, user.Email, temporaryPassword);

                    await _emailService.SendEmail(user.Email, "Cadastro DEVOLUWEB realizado com sucesso!", corpoEmail);
                }
                catch (Exception ex)
                {
                    return new(false, "Erro ao enviar email para o usuário. \n\n" + ex.GetExceptionMessage(), true);
                }

                await _usersRepository.AddUser(user);

                return new(true, "Usuário cadastrado com sucesso !");
            }
            catch (Exception ex)
            {
                return new(false, ex.GetExceptionMessage(), true);
            }
        }

        public async Task<ServiceResult> UpdateUser(UpdateUserRequest request, int userId)
        {
            try
            {
                Users? user = await _usersRepository.GetUser(userId);

                if (user is null)
                    return new(false, "Usuário não encontrado!");

                if (request.FirstName is not null)
                    user.FirstName = request.FirstName;

                if (request.LastName is not null)
                    user.LastName = request.LastName;

                if (request.Email is not null)
                    user.Email = request.Email;

                await _usersRepository.UpdateUser(user);

                return new();
            }
            catch (Exception ex)
            {
                return new(false, ex.GetExceptionMessage(), true);
            }
        }

        public async Task<ServiceResult> UpdatePassword(UpdatePasswordRequest request, int userId)
        {
            try
            {
                Users? user = await _usersRepository.GetUser(userId);

                if (user is null)
                    return new(false, "Usuário não encontrado!");

                bool passwordCheck = BCrypt.Net.BCrypt.Verify(request.OldPassword, user.Password);

                if (!passwordCheck)
                    return new(false, "Senha antiga incorreta!");

                user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

                await _usersRepository.UpdateUser(user);

                return new();
            }
            catch (Exception ex)
            {
                return new(false, ex.GetExceptionMessage(), true);
            }
        }

        public async Task<ServiceResult> DeactivateUser(int userId)
        {
            try
            {
                Users? user = await _usersRepository.GetUser(userId);

                if (user is null)
                    return new(false, "Usuário não encontrado!");

                user.Active = false;

                await _usersRepository.UpdateUser(user);

                return new();
            }
            catch (Exception ex)
            {
                return new(false, ex.GetExceptionMessage(), true);
            }
        }

        private static string MontaCorpoEmailSenhaTemporaria(string nome, string email, string senha)
        {
            return $@"<html>
                        <head>
                            <style>
                                body {{
                                    font-family: Arial, sans-serif;
                                }}
                            </style>
                        </head>
                        <body>
                            <h1>Olá, {nome}!</h1>
                            <p>Seu cadastro no sistema DEVOLUWEB foi realizado com sucesso!</p>
                            <p>Seus dados de acesso são:</p>
                            <p><strong>Email:</strong> {email}</p>
                            <p><strong>Senha:</strong> {senha}</p>
                            <p>Recomendamos que você altere sua senha no primeiro acesso.</p>
                            <p>Atenciosamente,</p>
                            <p>Equipe de suporte</p>
                        </body>
                    </html>";
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
