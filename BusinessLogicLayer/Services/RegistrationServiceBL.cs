using BusinessLogicLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.Entities;
using ModelLayer.MailSender;
using ModelLayer.RequestDTO.Registration;
using ModelLayer.Validation;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{

    public class RegistrationServiceBL:IRegistrationBL
    {
        private readonly IRegistrationRepo repo;
        private readonly IConfiguration _configuration;

        public RegistrationServiceBL(IRegistrationRepo repo, IConfiguration configuration)
        {
            this.repo = repo;
            _configuration = configuration;
        }
        public async Task<bool> AdminRegistration(UserRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new Exception();
                }
                bool isEmailValid = CheckValidation.ValidateEmail(request.EmailId);
                bool isPasswordValid = CheckValidation.ValidatePassword(request.Password);
                if(!isEmailValid && !isPasswordValid)
                {
                    throw new Exception("Invalid Format of Email or Password");
                }
                UserEntity userEntity = new UserEntity
                {
                    FullName = request.FullName,
                    EmailId = request.EmailId,
                    Password = Encrypt(request.Password),
                    Role = request.Role,
                };
                 if(await repo.AdminRegistration(userEntity))
                 {
                    EmailSender.sendMail(request.EmailId, request.Password);
                    return true;
                 }
                return false;
            }
            catch (Exception ex) 
            { 
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddEmployee(UserRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new Exception();
                }
                bool isEmailValid = CheckValidation.ValidateEmail(request.EmailId);
                bool isPasswordValid = CheckValidation.ValidatePassword(request.Password);
                if (!isEmailValid && !isPasswordValid)
                {
                    throw new Exception("Invalid Format of Email or Password");
                }
                UserEntity userEntity = new UserEntity
                {
                    FullName = request.FullName,
                    EmailId = request.EmailId,
                    Password = Encrypt(request.Password),
                    Role = request.Role,
                };
                 if(await repo.AddEmployee(userEntity))
                {
                    EmailSender.sendMail(request.EmailId, request.Password);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddAgent(AgentRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new Exception();
                }
                bool isEmailValid = CheckValidation.ValidateEmail(request.EmailId);
                bool isPasswordValid = CheckValidation.ValidatePassword(request.Password);
                if (!isEmailValid && !isPasswordValid)
                {
                    throw new Exception("Invalid Format of Email or Password");
                }
                UserEntity userEntity = new UserEntity
                {
                    FullName = request.FullName,
                    EmailId = request.EmailId,
                    Password = Encrypt(request.Password),
                    Location = request.Location,
                    Role = request.Role,
                };
                if( await repo.AddAgent(userEntity))
                {
                    EmailSender.sendMail(request.EmailId, request.Password);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddCustomer(UserRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new Exception();
                }
                bool isEmailValid = CheckValidation.ValidateEmail(request.EmailId);
                bool isPasswordValid = CheckValidation.ValidatePassword(request.Password);
                if (!isEmailValid && !isPasswordValid)
                {
                    throw new Exception("Invalid Format of Email or Password");
                }
                UserEntity userEntity = new UserEntity
                {
                    FullName = request.FullName,
                    EmailId = request.EmailId,
                    Password = Encrypt(request.Password),
                    Role = request.Role,
                };

                if(await repo.AddCustomer(userEntity))
                {
                    EmailSender.sendMail(request.EmailId, request.Password);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> Login(UserLoginRequest userLogin)
        {
            var user= await repo.Login(userLogin);

            if (Encrypt(userLogin.Password).Equals(user.Password))
            {
                return GenerateJwtToken(user);
            }
            else
            {
                throw new Exception("Incorrect password");
            }
        }
        public string Encrypt(string password)
        {
            byte[] refer = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(refer);
        }
        public string GenerateJwtToken(UserEntity user)
        {
          

            string jwtSecret = _configuration["JwtSettings:Secret"];
            if (string.IsNullOrEmpty(jwtSecret))
            {
                throw new InvalidOperationException("JWT secret key is null or empty. Make sure it's properly configured.");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSecret);
            if (key.Length < 32)
            {
                throw new ArgumentException("JWT secret key must be at least 256 bits (32 bytes)");
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
  
            new Claim(ClaimTypes.Role,user.Role),
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
