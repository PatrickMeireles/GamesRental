﻿using System.Threading.Tasks;
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
    [Route("v1/Game")]
    [Authorize(Roles = nameof(RoleUser.Administrator))]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Post([FromBody]GameViewModel model)
        {
            if (model == null)
                return NoContent();

            var validation = new GameValidation(_game).Validate(model);

            if (!validation.IsValid)
                return BadRequest(new Validator(validation));

            var game = await _game.Add(model);

            return Ok(game);
        }

        [HttpPut]
        [Route("Put/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put(int id, [FromBody]GameViewModel model)
        {
            if (id == 0 || model == null)
                return NoContent();

            model.Id = id;

            var validation = new GameValidation(_game).Validate(model);

            if (!validation.IsValid)
                return BadRequest(new Validator(validation));

            var game = await _game.Update(model);

            return Ok(game);
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

            var game = await _game.GetById(id);

            if (game == null)
                return BadRequest(new { errors = "Jogo não foi encontrado" });
            else if(game.HaveRents)
                return BadRequest(new { errors = "Não foi possível excluir o jogo, pois há empréstimos cadastrados com ele." });
            else
            {
                await _game.Delete(id);
                return Ok();
            }
        }
    }
}
