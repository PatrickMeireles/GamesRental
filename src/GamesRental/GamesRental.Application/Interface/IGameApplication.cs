using GamesRental.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GamesRental.Application.Interface
{
    public interface IGameApplication
    {
        Task<IEnumerable<GameViewModel>> GetAll(string descricao = "");

        Task<GameViewModel> GetById(int id);

        Task<GameViewModel> Add(GameViewModel data);
        Task<GameViewModel> Update(GameViewModel data);

        Task<bool> Delete(int id);
    }
}
