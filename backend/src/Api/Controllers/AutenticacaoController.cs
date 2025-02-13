using Api.Controllers.Base;
using Api.Dtos;
using Business.Entities;
using Business.Interfaces;
using Api.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Business.Messages;

namespace Api.Controllers
{
    [Route("api/conta")]
    public class AutenticacaoController(SignInManager<IdentityUser> signInManager, 
                                        UserManager<IdentityUser> userManager,
                                        INotificador notificador,
                                        IUsuarioRepository usuarioRepository,
                                        IOptions<JwtSettings> jwtSettings) : MainController(notificador)
    {

        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(RegisterUserDto registerUser)
        {
            if (!ModelState.IsValid)
            {
                NotificarErro(ModelState);
                return RetornoPadrao();
            }

            var userIdentity = new IdentityUser()
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(userIdentity, registerUser.Senha!);

            if (!result.Succeeded)
            {
                NotificarErro(result);
                return RetornoPadrao();
            }

            var usuario = new Usuario()
            {
                Id = userIdentity.Id,
                Nome = registerUser.Nome

            };

            await usuarioRepository.Adicionar(usuario);
            
            return RetornoPadrao(HttpStatusCode.Created);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserDto loginUser)
        {
            if (!ModelState.IsValid)
            {
                NotificarErro(ModelState);
                return RetornoPadrao();
            }

            var result = await signInManager.PasswordSignInAsync(loginUser.Email!, loginUser.Senha!, false, true);

            if (result.Succeeded)
            {
                var loginResponse = await GenerateTokenAsync(loginUser.Email!);
                return RetornoPadrao(HttpStatusCode.Created, loginResponse);
            }

            NotificarErro(Mensagens.UsuarioEouSenhaIncorretos);
            return RetornoPadrao();
        }

        private async Task<LoginResponseDto> GenerateTokenAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var roles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id)
            };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(jwtSettings.Value.Segredo!);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = jwtSettings.Value.Emissor,
                Audience = jwtSettings.Value.Audiencia,
                Expires = DateTime.UtcNow.AddHours(jwtSettings.Value.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseDto
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(jwtSettings.Value.ExpiracaoHoras).TotalSeconds,
                UserToken = new UserTokenDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimDto {Type = c.Type, Value = c.Value})
                }
            };

            return response;
        }

    }
}
