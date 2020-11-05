using AutoMapper;
using GamesRental.Application.Interface;
using GamesRental.Application.ViewModel;
using GamesRental.Domain.Interfaces;
using GamesRental.Domain.Methods;
using GamesRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesRental.Application.Services
{
    public class GameApplication : IGameApplication
    {
        private readonly IGame _game;
        private readonly IRental _rental;
        private readonly IMapper _mapper;

        public GameApplication(IGame game, IRental rental, IMapper mapper)
        {
            _game = game;
            _rental = rental;
            _mapper = mapper;
        }

        public async Task<GameViewModel> Add(GameViewModel data) => _mapper.Map<GameViewModel>(await _game.Add(_mapper.Map<Game>(data)));

        public async Task<bool> Avaliable(int id)
        {
            var rents = await _rental.GetAll();

            var lastRent = rents.Where(x => x.IdGame == id)
                                .OrderByDescending(x => x.Id)
                                .FirstOrDefault();

            return lastRent.Finished();
        }

        public async Task<bool> Delete(int id) => await _game.Delete(id);

        public async Task<IEnumerable<GameViewModel>> GetAll(string descricao, bool? avaliable) 
        {
            var data = _mapper.Map<IEnumerable<GameViewModel>>(await _game.GetAll());

            if (!String.IsNullOrEmpty(descricao) && !String.IsNullOrWhiteSpace(descricao))
                data = data.Where(x => x.Name == descricao);

            if (avaliable.HasValue)
                data = data.Where(x => x.Avaliable == avaliable.Value);

            return data;
        }
        public async Task<GameViewModel> GetById(int id) => _mapper.Map<GameViewModel>(await _game.GetById(id));

        public async Task<GameViewModel> Update(GameViewModel data) => _mapper.Map<GameViewModel>(await _game.Update(_mapper.Map<Game>(data)));
    }
}
