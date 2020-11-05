using FluentValidation;
using GamesRental.Application.Interface;
using GamesRental.Application.ViewModel;
using System.Linq;

namespace GamesRental.Application.Validation
{
    public class FriendValidation : AbstractValidator<FriendViewModel>
    {
        private readonly IFriendApplication _friend;
        public FriendValidation(IFriendApplication friend)
        {
            _friend = friend;

            RuleFor(x => x.Name)
                    .MaximumLength(180).WithMessage("Nome não pode ser maior que 180 caracteres.")
                    .NotEmpty().WithMessage("Nome não foi informado.")
                    .NotNull().WithMessage("Nome não foi informado.");

            RuleFor(x => x.Email)
                   .MaximumLength(180).WithMessage("Email não pode ser maior que 180 caracteres.")
                   .NotEmpty().WithMessage("Email não foi informado.")
                   .NotNull().WithMessage("Email não foi informado.")
                   .EmailAddress().WithMessage("Email informado não é válido.");

            RuleFor(x => x)
                .Must(x => !GetById(x.Id)).When(x => x.Id != 0).WithMessage("Amigo não encontrado.")
                .Must(x => !Exist(x)).WithMessage("Já possui um amigo cadastrado com esse email.");
        }

        private bool Exist(FriendViewModel model)
        {
            return _friend.GetAll("")
                          .GetAwaiter()
                          .GetResult()
                          .Where(x => model.Id != x.Id && x.Email == model.Email)
                          .Any();
        }

        private bool GetById(int id) =>_friend.GetById(id).GetAwaiter().GetResult() == null;
    }
}
