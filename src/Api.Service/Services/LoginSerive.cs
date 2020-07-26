using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
     public class LoginService : ILoginService
     {
          private IUserRepository _repository;
          private SigningConfigurations _signingConfigurations;
          private TokenConfiguration _tokenConfiguration;
          private IConfiguration _configuration { get; }

          public LoginService(
               IUserRepository repository, 
               SigningConfigurations signingConfigurations, 
               TokenConfiguration tokenConfiguration,
               IConfiguration configuration)
          {
              _repository = repository;
               _signingConfigurations = signingConfigurations;
               _tokenConfiguration = tokenConfiguration;
               _configuration = configuration;
          }

          public async Task<object> FindByLogin(LoginDto user)
          {
               var baseUser = new UserEntity();

               if(user != null && !string.IsNullOrWhiteSpace(user.Email))
               {
                    baseUser = await _repository.FindByLogin(user.Email);

                    if(baseUser != null)
                    {
                         var identity = new ClaimsIdentity(
                              new GenericIdentity(baseUser.Email),
                              new []
                              {
                                   new Claim(JwtRegisteredClaimNames.Jti, baseUser.Id.ToString()),
                                   new Claim(JwtRegisteredClaimNames.UniqueName, baseUser.Email)

                              }
                         );

                         DateTime createDate = DateTime.Now;
                         DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                         var handler = new JwtSecurityTokenHandler();

                         string token = CreateToken(identity, createDate, expirationDate, handler);

                         return SuccessObject(createDate, expirationDate, token, user);
                    }
               }

               return new 
               {
                    authenticated = false,
                    message = "Falha ao autenticar usuário."
               };
          }

          public string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
          {
               var securityToken =  handler.CreateToken(new SecurityTokenDescriptor
               {
                    Issuer = _tokenConfiguration.Issuer,
                    Audience = _tokenConfiguration.Audience,
                    SigningCredentials = _signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = createDate,
                    Expires = expirationDate
               });

               var token = handler.WriteToken(securityToken);
               
               return token;
          }

          private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, LoginDto user)
          {
               return new 
               {
                    authenticated = true,
                    created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    userName = user.Email,
                    message = "Sucesso na autenticação do usuário."
               };
          }
     }
}
