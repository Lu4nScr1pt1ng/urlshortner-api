using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using urlshortner.Data;
using urlshortner.Models;
using urlshortner.Utils;

namespace urlshortner.Controllers
{
    [Route("short")]
    public class LinkController : Controller
    {
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<Link>>> GetAllLinks(
            [FromServices] DataContext context
            )
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity!.Claims;
            var userId = claim.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var links = await context.Links!.Where(x => x.UserId == userId).ToListAsync();
            return Ok(links);
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<Link>> Post(
            [FromServices] DataContext context,
            [FromBody] Link model
            )
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity!.Claims;
            var userId = claim.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            GenerateLink linkgenerator = new GenerateLink();

            try
            {
                model.Id = linkgenerator.Generate();
                model.UserId = userId;
                model.CreatedAt = DateTime.UtcNow;

                context.Links!.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Ocorreu um erro ao criar o link" });
            }
        }
    }
}
