using System.Threading.Tasks;
using GamesRental.Application.Interface;
using GamesRental.Application.Validation;
using GamesRental.Application.Validation.Util;
using GamesRental.Application.ViewModel;
using GamesRental.WebApi.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GamesRental.WebApi.Controllers
{
    [Route("v1/Login")]
    public class LoginController : Controller
    {
        private readonly IUserApplication _user;
        private readonly IConfiguration _configuration;

        public LoginController(IUserApplication user, IConfiguration configuration)
        {
            _user = user;
            _configuration = configuration;
        }

        /// <summary>
        /// Chamada de Autenticação de usuário.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// - Se o objeto do parâmetro for nulo, retorna 404;
        /// - Se possuir algum erro na validação, retorna 400;
        /// - Se não, irá retornar 200 com o Token.
        /// </returns>
        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel model)
        {
            if (model == null)
                return NoContent();

            var validation = new LoginValidation(_user).Validate(model);

            if (!validation.IsValid)
                return BadRequest(new Validator(validation));

            var user = await _user.Authenticate(model.Login, model.Password);

            if (user == null)
                return NotFound();
            else
                return Ok(new { token = new Token(_configuration).Generate(user) });
        }
    }
}
