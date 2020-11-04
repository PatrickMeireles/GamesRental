using GamesRental.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GamesRental.Application.Interface
{
    public interface IRentalApplication
    {
        Task<IEnumerable<RentalViewModel>> GetAll(int? idGame, int? idFriend, DateTime? date, DateTime? dateFinish);
        Task<RentalViewModel> Create(RentalViewModel data);
        Task<RentalViewModel> Update(RentalViewModel data);
        Task<RentalViewModel> Finish(int id);
        Task<RentalViewModel> GetById(int id);
    }
}
