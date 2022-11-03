using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using urlshortner.Data;
using urlshortner.Models;

namespace urlshortner.Controllers
{
    [Route("v1/users")]
    public class UserController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<User>>> Get(
            [FromServices] DataContext context
            )
        {
            var users = await context.Users.AsNoTracking().ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Post(
            [FromServices] DataContext context,
            [FromBody] User model
            )
        {
            // Checa se o modelo enviado está correto
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Users.Add(model);
                await context.SaveChangesAsync();

                model.Password = "";
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível criar o usuário" });
            }
        }

    }
}
