using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto_BD1.Data.Contracts;
using Projeto_BD1.Models;

namespace Projeto_BD1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyTest : ControllerBase
    {
        private IDatabaseMethods _context;


        public DummyTest(IDatabaseMethods context)
        {
            _context = context;
        }


        [AllowAnonymous]
        [HttpGet("/centers")]
        public async Task<IEnumerable<Center>> getCenters()
        {
            var centers = await _context.crudArrayListAsync<Center>("SELECT * FROM Center");
            return centers;
        }

    }
}
