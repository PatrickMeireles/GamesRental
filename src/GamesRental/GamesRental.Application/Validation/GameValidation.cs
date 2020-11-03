using FluentValidation;
using GamesRental.Application.Interface;
using GamesRental.Application.ViewModel;
using GamesRental.Entities.Enuns;
using System;
using System.Linq;

namespace GamesRental.Application.Validation
{
    public class GameValidation : AbstractValidator<GameViewModel>
    {
        private readonly IGameApplication _game;

        public GameValidation(IGameApplication game)
        {
            _game = game;

            RuleFor(x => x.Name)
                .MaximumLength(180).WithMessage("Nome não pode ser maior que 180 caracteres.")
                .NotEmpty().WithMessage("Nome não foi informado.")
                .NotNull().WithMessage("Nome não foi informado.");

            RuleFor(x => x.Description)
                   .MaximumLength(1000).WithMessage("Nome não pode ser maior que 1000 caracteres.");

            RuleFor(x => x.Genre)
                    .NotEmpty().WithMessage("Gênero não foi informado")
                    .Must(x => GeneroValido(x)).WithMessage("Gênero informado não é válido.");

            RuleFor(x => x)
                    .Must(x => !GetById(x.Id)).When(x => x.Id != 0).WithMessage("Jogo não encontrado.")
                    .Must(x => !Existe(x)).WithMessage("Já possui um game cadastrado com esse nome.");
        }

        private Boolean Existe(GameViewModel model)
        {
            return _game.GetAll()
                        .GetAwaiter()
                        .GetResult()
                        .Where(x => x.Id != model.Id && x.Name == model.Name)
                        .Any();
        }

        private bool GetById(int id) => _game.GetById(id).GetAwaiter().GetResult() == null;

        private bool GeneroValido(int Genre)
        {
            return Enum.GetValues(typeof(GenreGame)).Cast<GenreGame>().Cast<int>().Where(x => x == Genre).Any();
        }
    }
}
