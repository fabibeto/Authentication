using ChallengeAPI.Entities;
using ChallengeAPI.Views.Auth.Login;
using ChallengeAPI.Views.Auth.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeAPI.Controllers
{
    public class AuthenticationController:ControllerBase
    {
        public readonly UserManager<User> _userManager;
        public readonly SignInManager<User> _signInManager;
        private User currentUser;
        private string x;

        public object ClainTypes { get; private set; }
        public object SecurityAlgorithm5 { get; private set; }
        public object TaskJwtSegurityTokens { get; private set; }

        public AuthenticationController(UserManager<User> userManager,SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            
        }
        //Registro
        [HttpPost]
        [Route(template: "registro")]
        public async Task<IActionResult> Register(RegisterRequestModel model)
        {
            //Revisar si existe el usuario
            var userExists = await _userManager.FindByNameAsync(model.Username);

            //Si existe,devolver un error
            if (userExists != null) 
            {
                return StatusCode(StatusCodes.Status400BadRequest);
              
            }

            //Si no existe,registrar al usuario
            var user = new User
            {
                UserName = model.Username,
                Email=model.Email,
                IsActive=true
            };
            var result = await _userManager.CreateAsync(user,model.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  value:new
                 { 
                    Status = "Error",
                    Message = $"User Creation Failed ! Errors:{string.Join(separator: ", ",values:result.Errors.Select(x => x.Description))}"
                 });
            }
            return Ok(new
            {
                Status = "Success",
                Message = $"User created Successfully !!!"
             });
            }
        //Login
        [HttpPost]
        [Route(template: "login")]
        public async Task<IActionResult> Login(LoginRequestViewModel model)
        {
            //Tenemos que chequear que el usuario exista y la password correcta
            
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(model.Username);

                if (currentUser.IsActive)
                {
                    //Generar nuestro Token


                    //Devolver el token creado
                     return Ok(await GetToken(currentUser));
                }
            }
            return StatusCode(StatusCodes.Status401Unauthorized,
                value: new
                {
                    Status = "Error",
                    Message = $"El usuario {model.Username}no esta autorizado"
                });
            
        }
        private async Task<LoginResponseViewModel> GetToken(User currentUser )
        {
            var userRoles= await _userManager.GetRolesAsync(currentUser);

            var authClaims = new List<Claim>()
            {
                new Claim(type:ClaimTypes.Name,value:currentUser.UserName),
                new Claim(type:JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            authClaims.AddRange(collection: userRoles.Select(x => new Claim(ClaimTypes.Role, value: x)));
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s: "KeySecretaSuperLargaDeAUTORIZACION"));

            var token = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return new LoginResponseViewModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };

  
        }

    
    }
}
