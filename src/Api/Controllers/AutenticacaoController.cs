using Api.Controllers.Base;
using Business.Dtos;
using Business.Entities;
using Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [Route("api/conta")]
    public class AutenticacaoController(SignInManager<Usuario> signInManager, 
                                        UserManager<Usuario> userManager,
                                        INotificador notificador,
                                        IJwtService jwtService) : MainController(notificador)
    {

        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(RegisterUserDto registerUser)
        {
            if (!ModelState.IsValid) return RetornoPadrao(ModelState);

            var userIdentity = new Usuario
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true,
                Nome = registerUser.Nome
            };

            var result = await userManager.CreateAsync(userIdentity, registerUser.Senha);

            if (result.Succeeded)
            { 
                await signInManager.SignInAsync(userIdentity, false);
                var token = await jwtService.GenerateTokenAsync(userIdentity.Email);

                return RetornoPadrao(HttpStatusCode.Created, new { token });
            }

            return RetornoPadrao(result);
            
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserDto loginUser)
        {
            if (!ModelState.IsValid) return RetornoPadrao(ModelState);

            var result = await signInManager.PasswordSignInAsync(loginUser.Email!, loginUser.Senha!, false, true);

            if (result.Succeeded)
            {
                var token = await jwtService.GenerateTokenAsync(loginUser.Email);
                return RetornoPadrao(HttpStatusCode.OK, new { token });
            }
            NotificarErros("Usuário ou senha incorretos.");
            return RetornoPadrao();

        }
    }
}
