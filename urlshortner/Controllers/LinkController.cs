using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<List<Link>>> GetAllLinks(
            [FromServices] DataContext context
            )
        {
            var links = await context.Links.AsNoTracking().ToListAsync();
            return Ok(links);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Link>> Post(
            [FromServices] DataContext context,
            [FromBody] Link model
            )
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                GenerateLink linkgenerator = new GenerateLink();
                model.Id = linkgenerator.Generate();

                context.Links.Add(model);
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
