using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using urlshortner.Data;
using urlshortner.Models;
using urlshortner.Utils;

namespace urlshortner.Controllers
{
    [Route("go")]
    public class AccessController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Access>>> GetAllAccess(
            [FromServices] DataContext context
            )
        {
            try
            {
                var accesses = await context.Accesses.AsNoTracking().ToListAsync();
                if (accesses == null)
                {
                    return NotFound(new { message = "não foi encontrado nenhum acesso!" });
                }

                return Ok(accesses);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possivel criar objeto no banco de dados" });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Access>> Access(
            string id,
            [FromServices] DataContext context,
            [FromBody] Access model
            )
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var redirectlink = await context.Links.FirstOrDefaultAsync(x => x.Id == id);
                    if (redirectlink == null)
                {
                    return NotFound(new { message = "id de link não encontrado" });
                }
                model.LinkId = id;
                model.AccesedAt = DateTime.Now;
                context.Accesses.Add(model);
                await context.SaveChangesAsync();

                return Ok(redirectlink?.RedirectLink);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possivel criar objeto no banco de dados" });
            }
        }
    }
}
