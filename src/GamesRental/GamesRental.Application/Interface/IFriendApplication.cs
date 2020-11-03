using GamesRental.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamesRental.Application.Interface
{
    public interface IFriendApplication
    {
        Task<IEnumerable<FriendViewModel>> GetAll(string descricao);

        Task<FriendViewModel> GetById(int id);

        Task<FriendViewModel> Create(FriendViewModel data);

        Task<FriendViewModel> Update(FriendViewModel data);

        Task<Boolean> Delete(int id);
    }
}
