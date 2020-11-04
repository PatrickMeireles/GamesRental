using System.Threading.Tasks;
using GamesRental.Application.Interface;
using GamesRental.Application.Validation;
using GamesRental.Application.Validation.Util;
using GamesRental.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GamesRental.WebApi.Controllers
{
    [Route("v1/Game")]
    public class GameController : Controller
    {
        private readonly IGameApplication _game;

        public GameController(IGameApplication game) => _game = game;

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(string descricao = "") => Ok(await _game.GetAll(descricao));

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await _game.GetById(id));

        /// <summary>
        /// Chamada para lista de jogos que estão disponíveis.
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Avaliable")]
        public async Task<IActionResult> Avaliable(string descricao) => Ok(await _game.GetAll("", true));

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody]GameViewModel model)
        {
            if (model == null)
                return NotFound();

            var validation = new GameValidation(_game).Validate(model);

            if (!validation.IsValid)
                return BadRequest(new Validator(validation));

            var game = await _game.Add(model);

            return Ok(game);
        }

        [HttpPut]
        [Route("Put/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]GameViewModel model)
        {
            if (id == 0 || model == null)
                return NotFound();

            model.Id = id;

            var validation = new GameValidation(_game).Validate(model);

            if (!validation.IsValid)
                return BadRequest(new Validator(validation));

            var game = await _game.Update(model);

            return Ok(game);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return NotFound();

            var game = await _game.GetById(id);

            if (game == null)
                return BadRequest(new { errors = "Jogo não foi encontrado" });

            await _game.Delete(id);

            return Ok();
        }
    }
}
