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
    public class RentalApplication : IRentalApplication
    {
        private readonly IRental _rental;
        private readonly IMapper _mapper;

        public RentalApplication(IRental rental, IMapper mapper)
        {
            _rental = rental;
            _mapper = mapper;
        }

        public async Task<RentalViewModel> Create(RentalViewModel data) => _mapper.Map<RentalViewModel>(await _rental.Add(_mapper.Map<Rental>(data)));

        public async Task<RentalViewModel> Finish(int id)
        {
            var rental = await _rental.GetById(id);
            rental.DateFinish = DateTime.Now;

            return _mapper.Map<RentalViewModel>(await _rental.Update(_mapper.Map<Rental>(rental)));
        }

        public async Task<IEnumerable<RentalViewModel>> GetAll(int? idGame = null, int? idFriend = null, DateTime? date = null, DateTime? dateFinish = null)
        {
            var data = _mapper.Map<IEnumerable<RentalViewModel>>(await _rental.GetAll());

            if (idGame.HasValue)
                data = data.Where(x => x.IdGame == idGame.Value);

            if (idFriend.HasValue)
                data = data.Where(x => x.IdFriend == idFriend.Value);

            if (date.HasValue)
                data = data.Where(x => x.Date >= date.Value);

            if (dateFinish.HasValue)
                data = data.Where(x => x.DateFinish <= dateFinish.Value);

            return data;
        }

        public async  Task<RentalViewModel> GetById(int id) => _mapper.Map<RentalViewModel>(await _rental.GetById(id));

        public async Task<RentalViewModel> Update(RentalViewModel data) => _mapper.Map<RentalViewModel>(await _rental.Update(_mapper.Map<Rental>(data)));
    }
}
