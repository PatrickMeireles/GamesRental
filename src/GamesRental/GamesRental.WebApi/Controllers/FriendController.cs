using System.Threading.Tasks;
using GamesRental.Application.Interface;
using GamesRental.Application.Validation;
using GamesRental.Application.Validation.Util;
using GamesRental.Application.ViewModel;
using GamesRental.Entities.Enuns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamesRental.WebApi.Controllers
{
    [Route("v1/Friend")]
    [Authorize(Roles = nameof(RoleUser.Administrator))]
    public class FriendController : Controller
    {
        private readonly IFriendApplication _friend;

        public FriendController(IFriendApplication friend) => _friend = friend;

        /// <summary>
        /// Lista de amigos cadastrados
        /// </summary>
        /// <param name="descricao">Parâmetro verifica se o Nome ou Email contém o parâmetro digitado.</param>
        /// <returns>Retorna lista de amigos</returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(string descricao) =>  Ok(await _friend.GetAll(descricao));

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await _friend.GetById(id));

        [HttpPost]
        [Route("Post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Post([FromBody]FriendViewModel model)
        {
            if(model == null)
                return NoContent();

            var validation = new FriendValidation(_friend).Validate(model);

            if (!validation.IsValid)
                return BadRequest(new Validator(validation));

            var friend = await _friend.Create(model);

            return Ok(friend);
        }

        [HttpPut]
        [Route("Put/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put(int id, [FromBody] FriendViewModel model)
        {
            if (id == 0 ||model == null)
                return NoContent();

            model.Id = id;
            var validation = new FriendValidation(_friend).Validate(model);

            if (!validation.IsValid)
                return BadRequest(new Validator(validation));

            var friend = await _friend.Update(model);

            return Ok(friend);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return NoContent();

            var friend = await _friend.GetById(id);

            if (friend == null)
                return BadRequest(new { errors = "Amigo não encontrado." });
            else if(friend.HaveRents)
                return BadRequest(new { errors = "Não foi possível excluir o amigo, pois existe empréstimo por ele." });
            else
            {
                await _friend.Delete(id);
                return Ok();
            }            
        }

    }
}
