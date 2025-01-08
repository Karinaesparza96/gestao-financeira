using Api.Controllers.Base;
using Api.Dtos;
using Business.Entities;
using Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [Route("api/conta")]
    public class AutenticacaoController(SignInManager<IdentityUser> signInManager, 
                                        UserManager<IdentityUser> userManager,
                                        INotificador notificador,
                                        IUsuarioRepository usuarioRepository,
                                        IJwtService jwtService) : MainController(notificador)
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
                var token = await jwtService.GenerateTokenAsync(loginUser.Email!);
                return RetornoPadrao(default, new { token });
            }
            NotificarErro("Usuário ou senha incorretos.");
            return RetornoPadrao();
        }
    }
}
