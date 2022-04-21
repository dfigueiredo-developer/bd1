using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto_BD1.Data;
using Projeto_BD1.Data.Contracts;
using Projeto_BD1.Models;
using Projeto_BD1.Models.User;
using System.Data;

namespace Projeto_BD1.Controllers
{
    [Route("api/center")]
    [ApiController]
    public class CenterController : ControllerBase
    {
        private IDatabaseMethods _context;
        public CenterController(IDatabaseMethods context)
        {
            _context = context;
        }

        [HttpPost("new")]
        [Authorize(Roles = "Administrator")]
        public IActionResult NewCenter([FromBody] Center center)
        {
            var currentUser = UserMethods.GetCurrentUser(HttpContext);
            return Ok(center);
        }

        [HttpGet("all")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetCenters()
        {
            List<Center> centers = (List<Center>)await _context.crudArrayListAsync<Center>("SELECT * FROM Center");

            if (centers.Count() < 1)
            {
                return Ok(new BodyResponse(false, "Não existem centros."));
            }

            return Ok(new BodyResponse(true, "Centros encontrados.", centers));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetCenter(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);

            List<Center> centers = (List<Center>)await _context.crudArrayListAsync<Center>("SELECT * FROM Center WHERE ID = @id", parameters);

            if (centers.Count() < 1)
            {
                return Ok(new BodyResponse(false, "Centro não encontrado."));
            }
            return Ok(new BodyResponse(true, "Centro encontrado.", centers.First()));
        }


        // User

        [HttpGet("")]
        [Authorize(Roles = "Colaborator,Administrator")]
        public async Task<IActionResult> GetCenter()
        {

            User currentUser = UserMethods.GetCurrentUser(HttpContext);
            if (currentUser == null)
            {
                return Ok(new BodyResponse(false, "Utilizador não encontrado."));
            }

            var parameters = new DynamicParameters();
            parameters.Add("userId", currentUser.ID, DbType.Int32);

            List<Center> centers = (List<Center>)(await _context.crudArrayListAsync<Center>("SELECT center.* FROM Center INNER JOIN InternalUser internalUser ON(internalUser.CenterID = Center.ID) WHERE internalUser.EndUserID = @userId", parameters));

            if (centers.Count() < 1)
            {
                return Ok(new BodyResponse(false, "Centro não encontrado."));
            }
            return Ok(new BodyResponse(true, "Centro encontrado.", centers.First()));
        }

    }
}
