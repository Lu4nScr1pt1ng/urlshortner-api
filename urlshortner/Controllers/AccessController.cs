using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using urlshortner.Data;
using urlshortner.Models;
using urlshortner.Utils;

namespace urlshortner.Controllers
{
    [Route("go")]
    public class AccessController : Controller
    {
        //[HttpGet]
        //[Route("")]
        //[Authorize]
        //public async Task<ActionResult<List<Access>>> GetAllAccess(
        //    [FromServices] DataContext context
        //    )
        //{
        //    try
        //    {
        //        var identity = HttpContext.User.Identity as ClaimsIdentity;
        //        IEnumerable<Claim> claim = identity!.Claims;
        //        var userId = claim.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        //        var accesses = await context.Accesses!.Where(x => x.CreatorOfLinkId == userId).AsNoTracking().ToListAsync();

        //        if (accesses.Count() == 0)
        //        {
        //            return NotFound(new { message = "não foi encontrado nenhum acesso!" });
        //        }

        //        return Ok(accesses);
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest(new { message = "Não foi possivel criar objeto no banco de dados" });
        //    }
        //}


        [HttpPost]
        [Route("{id}")]
        public async Task<ActionResult<Access>> Access(
            string id,
            [FromServices] DataContext context,
            [FromBody] Access model
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var redirectlink = await context.Links!.FirstOrDefaultAsync(x => x.Id == id);

                if (redirectlink == null)
                {
                    return NotFound(new { message = "id de link não encontrado" });
                }
                model.LinkId = id;
                model.CreatorOfLinkId = redirectlink.UserId;
                model.AccesedAt = DateTime.Now;
                context.Accesses!.Add(model);
                await context.SaveChangesAsync();

                return Ok(new { id = redirectlink.Id, redirectlink = redirectlink.RedirectLink });
                //return Redirect(redirectlink.RedirectLink);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possivel criar objeto no banco de dados" });
            }
        }

        [HttpGet]
        [Route("e/{id}")]
        public async Task<ActionResult<List<Access>>> GetAccessesById(
            string id,
            [FromServices] DataContext context
            )
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claim = identity!.Claims;
                var userId = claim.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;


                var accesses = await context.Accesses!.Where(x => x.LinkId == id && x.CreatorOfLinkId == userId).ToListAsync();


                return Ok(accesses);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Erro ao obter informações no banco de dados" });
            }
        }
    }
}
