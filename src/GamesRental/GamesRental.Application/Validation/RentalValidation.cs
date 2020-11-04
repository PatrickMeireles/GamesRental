using FluentValidation;
using GamesRental.Application.Interface;
using GamesRental.Application.ViewModel;

namespace GamesRental.Application.Validation
{
    public class RentalValidation : AbstractValidator<RentalViewModel>
    {
        private readonly IRentalApplication _rental;
        private readonly IGameApplication _game;
        private readonly IFriendApplication _friend;

        public RentalValidation(IRentalApplication rental, IGameApplication game, IFriendApplication friend)
        {
            _rental = rental;
            _game = game;
            _friend = friend;

            RuleFor(x => x.IdFriend)
                    .NotEmpty().WithMessage("Amigo não foi informado.")
                    .NotNull().WithMessage("Amigo não foi informado.")
                    .Must(x => x > 0).WithMessage("Jogo não foi informado.")
                    .Must(x => !FriendValid(x)).When(x => x.IdFriend != 0).WithMessage("Amigo não foi encontrado.");

            RuleFor(x => x.IdGame)                    
                    .NotEmpty().WithMessage("Jogo não foi informado.")
                    .NotNull().WithMessage("Jogo não foi informado.")
                    .Must(x => x > 0).WithMessage("Jogo não foi informado.")
                    .Must(x => !GameValid(x)).When(x => x.IdGame != 0).WithMessage("Jogo não foi encontrado.");

            RuleFor(x => x).Must(x => GameAvaliable(x.IdGame)).When(x => x.IdGame != 0).WithMessage("Jogo não está disponível.");
        }

        private bool FriendValid(int idFriend)
        {
            return _friend.GetById(idFriend)
                          .GetAwaiter()
                          .GetResult() == null;
        }

        private bool GameValid(int idGame)
        {
            return _game.GetById(idGame)
                          .GetAwaiter()
                          .GetResult() == null;
        }

        private bool GameAvaliable(int idGame)
        {
            return _game.Avaliable(idGame)
                        .GetAwaiter()
                        .GetResult();
        }
    }
}
