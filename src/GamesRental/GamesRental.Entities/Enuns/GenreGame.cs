using System.ComponentModel;

namespace GamesRental.Entities.Enuns
{
    public enum GenreGame
    {
        [Description("Ação")]
        Action = 1,

        [Description("Aventura")]
        Adventure = 2,

        [Description("Esports")]
        Sports = 3,

        [Description("RPG")]
        RolePlaying = 4,

        [Description("Simulação")]
        Simulation = 5,

        [Description("Estratégia")]
        Strategy = 6,

        [Description("Outros")]
        Other = 7
    }
}
