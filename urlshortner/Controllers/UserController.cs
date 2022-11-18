using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using urlshortner.Data;
using urlshortner.Models;
using urlshortner.Services;

namespace urlshortner.Controllers
{
    [Route("v1/user")]
    public class UserController : Controller
    {
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<User>>> Get(
            [FromServices] DataContext context
            )
        {
            var users = await context.Users!.AsNoTracking().ToListAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("verify")]
        [Authorize]
        public ActionResult Verify(
            [FromServices] DataContext context
            )
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity!.Claims;
            var username = claim.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;


            return Ok(new { name = username });
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
                context.Users!.Add(model);
                await context.SaveChangesAsync();

                model.Password = "";
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível criar o usuário" });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromServices] DataContext context,
            [FromBody] User model
            )
        {
            if (model == null || model.Email == null || model.Password == null) return BadRequest(new { message = "Email ou senha não inserido" });

            var user = await context.Users!.AsNoTracking().Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new { message = "Email ou senha inválidos" });

            var token = TokenService.GenerateToken(user);

            HttpContext.Response.Cookies.Append("jwt", token, new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = true,
                Path = "/",
                Expires = DateTime.Now.AddHours(1),
                IsEssential = true,
                SameSite = SameSiteMode.None,
                Secure = true,
            });

            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
           
            
        }

    }
}
