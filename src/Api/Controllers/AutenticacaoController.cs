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
    public class AutenticacaoController(SignInManager<Usuario> signInManager, 
                                        UserManager<Usuario> userManager,
                                        IJwtService jwtService) : MainController
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

                return RetornoPadrao(ResultadoOperacao<object>.Sucesso(new { token }), HttpStatusCode.Created);
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
                return RetornoPadrao(ResultadoOperacao<object>.Sucesso(new { token }), HttpStatusCode.OK);
            }

            return RetornoPadrao(ResultadoOperacao.Falha("Usuário ou senha incorretos."));
        }
    }
}
