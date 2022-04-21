using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Projeto_BD1.Data.Contracts;
using Projeto_BD1.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Projeto_BD1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private IDatabaseMethods _context;

        public LoginController(IConfiguration config, IDatabaseMethods context)
        {            
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult>  Login([FromBody] InternalUser internalUser)
        {
            var user = await Authenticate(internalUser);

            if (user != null)
            {
                var token = await Generate(user);
                return Ok(token);
            }

            return NotFound("User not found!");
        }
        private async Task<InternalUser> Authenticate(InternalUser internalUser)
        {
            /*
             * Receber os dados do user;
             * Comparar com que existe na BD;
             * Caso exista: Retorna o utilizador em questão;
             * Caso não exista: Returna o valor nulo;
             */

            var parameters = new DynamicParameters();
            parameters.Add("Password", internalUser.Password, DbType.String);
            parameters.Add("Username", internalUser.Username, DbType.String);

            var userResult = await _context.crudArrayListAsync<InternalUser>("SELECT * FROM InternalUser WHERE Username = @Username AND Password = @Password",parameters);
          

            if ((userResult != null) && (userResult.Count() > 0)){
                return userResult.First();
            }
            return null;
        }

        private async Task<string> Generate(InternalUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            /*
             * Para construir o Claim: 
             *  Obter: 
             *      - Nome
             *      - Email
             *      - Rule -> WHERE ID = (SELECT PermissionID FROM EndUserPermission WHERE EndUserID = 3)
             */

            var parameters = new DynamicParameters();
            parameters.Add("endUserID", user.EndUserId, DbType.Int32);

            EndUser endUser = (await _context.crudArrayListAsync<EndUser>("SELECT * FROM EndUser WHERE ID = @endUserID", parameters)).First();
            
            Permission permission = (await _context.crudArrayListAsync<Permission>("SELECT * FROM Permission WHERE ID = (SELECT PermissionID FROM EndUserPermission WHERE EndUserID = @endUserID)", parameters)).First();
            
            var claims = new[]
            {
                new Claim(type: "ID", value: user.EndUserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Name, endUser.Name),
                new Claim(ClaimTypes.Email, endUser.Email),
                new Claim(ClaimTypes.Role, permission.Label)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
