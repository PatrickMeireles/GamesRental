using AutoMapper;
using GamesRental.Application.Interface;
using GamesRental.Application.ViewModel;
using GamesRental.Domain.Interfaces;
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
        private readonly IMapper _mapper;

        public GameApplication(IGame game, IMapper mapper)
        {
            _game = game;
            _mapper = mapper;
        }

        public async Task<GameViewModel> Add(GameViewModel data) => _mapper.Map<GameViewModel>(await _game.Add(_mapper.Map<Game>(data)));

        public async Task<bool> Delete(int id) => await _game.Delete(id);

        public async Task<IEnumerable<GameViewModel>> GetAll(string descricao) 
        {
            var data = _mapper.Map<IEnumerable<GameViewModel>>(await _game.GetAll());

            if (!String.IsNullOrEmpty(descricao) && !String.IsNullOrWhiteSpace(descricao))
                data = data.Where(x => x.Name == descricao);

            return data;
        }
        public async Task<GameViewModel> GetById(int id) => _mapper.Map<GameViewModel>(await _game.GetById(id));

        public async Task<GameViewModel> Update(GameViewModel data) => _mapper.Map<GameViewModel>(await _game.Update(_mapper.Map<Game>(data)));
    }
}
