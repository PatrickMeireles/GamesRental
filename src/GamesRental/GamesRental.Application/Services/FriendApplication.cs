using AutoMapper;
using GamesRental.Application.Interface;
using GamesRental.Application.ViewModel;
using GamesRental.Domain.Interfaces;
using GamesRental.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesRental.Application.Services
{
    public class FriendApplication : IFriendApplication
    {
        private readonly IFriend _friend;
        private readonly IMapper _mapper;

        public FriendApplication(IFriend friend, IMapper mapper)
        {
            _friend = friend;
            _mapper = mapper;
        }
        public async Task<FriendViewModel> Create(FriendViewModel data) =>  _mapper.Map<FriendViewModel>(await _friend.Add(_mapper.Map<Friend>(data)));

        public async Task<bool> Delete(int id) => await _friend.Delete(id);

        public async Task<IEnumerable<FriendViewModel>> GetAll(string descricao)
        {
            var data = await _friend.GetAll();

            if (!string.IsNullOrEmpty(descricao) || !string.IsNullOrWhiteSpace(descricao))
                data = data.Where(x => x.Name.Contains(descricao) || x.Email.Contains(descricao));

            return _mapper.Map<IEnumerable<FriendViewModel>>(data);
        }

        public async Task<FriendViewModel> GetById(int id) => _mapper.Map<FriendViewModel>(await _friend.GetById(id));

        public async Task<FriendViewModel> Update(FriendViewModel data) =>  _mapper.Map<FriendViewModel>(await _friend.Update(_mapper.Map<Friend>(data)));
    }
}
