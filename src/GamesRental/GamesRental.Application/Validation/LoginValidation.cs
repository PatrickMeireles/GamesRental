using FluentValidation;
using GamesRental.Application.Interface;
using GamesRental.Application.ViewModel;
using System;

namespace GamesRental.Application.Validation
{
    public class LoginValidation : AbstractValidator<LoginViewModel>
    {
        private readonly IUserApplication _user;

        public LoginValidation(IUserApplication user)
        {
            _user = user;

            RuleFor(x => x.Login)
                   .NotEmpty().WithMessage("Login não foi informado.")
                   .NotNull().WithMessage("Login não foi informado.");

            RuleFor(x => x.Password)
                   .NotEmpty().WithMessage("Senha não foi informado.")
                   .NotNull().WithMessage("Senha não foi informado.");

            RuleFor(x => x)
                    .Must(x => Validate(x))
                    .WithMessage("Usuário não encontrado.");
        }

        private new Boolean Validate(LoginViewModel login)
        {
            return _user.Authenticate(login.Login, login.Password)
                        .GetAwaiter()
                        .GetResult() != null;
        }
    }
}
