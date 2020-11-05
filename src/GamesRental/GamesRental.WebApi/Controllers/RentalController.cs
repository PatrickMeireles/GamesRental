using System;
using System.Threading.Tasks;
using GamesRental.Application.Interface;
using GamesRental.Application.Validation;
using GamesRental.Application.Validation.Util;
using GamesRental.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GamesRental.Entities.Enuns;

namespace GamesRental.WebApi.Controllers
{
    [Route("v1/Rental")]
    [Authorize(Roles = nameof(RoleUser.Administrator))]
    public class RentalController : Controller
    {
        private readonly IRentalApplication _rental;
        private readonly IFriendApplication _friend;
        private readonly IGameApplication _game;

        public RentalController(IRentalApplication rental, IFriendApplication friend, IGameApplication game)
        {
            _rental = rental;
            _friend = friend;
            _game = game;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(int? idGame, int? idFriend, DateTime? date, DateTime? dateFinish) => Ok(await _rental.GetAll(idGame, idFriend, date, dateFinish));

        /// <summary>
        /// Criação do aluguel
        /// </summary>
        /// <param name="model">Preencher os valores de qual jogo e qual amigo ira alugar.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody]RentalCreateViewModel model)
        {
            if (model == null || model.IdFriend <= 0 || model.IdGame <= 0)
                return NotFound();

            var rentalVM = new RentalViewModel(model.IdGame, model.IdFriend, DateTime.Now);

            var validation = new RentalValidation(_rental, _game, _friend).Validate(rentalVM);

            if (!validation.IsValid)
                return BadRequest(new Validator(validation));

            var retorno = await _rental.Create(rentalVM);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("Finish/{id}")]
        public async Task<IActionResult> Finish(int id)
        {
            if (id == 0)
                return NotFound();

            var rental = await _rental.GetById(id);

            if (rental == null)
                return BadRequest(new { error = "Aluguel não encontrado." });

            if (rental.DateFinish.HasValue)
                return BadRequest(new { error = "Não é possível finalizar o Aluguel, pois o mesmo já foi finalizado." });
            else
                return Ok(await _rental.Finish(id));

        }
    }
}
