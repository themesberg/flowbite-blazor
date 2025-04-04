namespace DemoApp.Services;

using DemoApp.Data;

public class PokemonService
{
    private readonly List<Pokemon> _pokemon;

    public PokemonService()
    {
        _pokemon = new List<Pokemon>
        {
            new Pokemon { Id = 1, Name = "Bulbasaur", Type1 = "Grass", Type2 = "Poison", HP = 45, Attack = 49, Defense = 49, SpecialAttack = 65, SpecialDefense = 65, Speed = 45 },
            new Pokemon { Id = 2, Name = "Ivysaur", Type1 = "Grass", Type2 = "Poison", HP = 60, Attack = 62, Defense = 63, SpecialAttack = 80, SpecialDefense = 80, Speed = 60 },
            new Pokemon { Id = 3, Name = "Venusaur", Type1 = "Grass", Type2 = "Poison", HP = 80, Attack = 82, Defense = 83, SpecialAttack = 100, SpecialDefense = 100, Speed = 80 },
            new Pokemon { Id = 4, Name = "Charmander", Type1 = "Fire", Type2 = "", HP = 39, Attack = 52, Defense = 43, SpecialAttack = 60, SpecialDefense = 50, Speed = 65 },
            new Pokemon { Id = 5, Name = "Charmeleon", Type1 = "Fire", Type2 = "", HP = 58, Attack = 64, Defense = 58, SpecialAttack = 80, SpecialDefense = 65, Speed = 80 },
            new Pokemon { Id = 6, Name = "Charizard", Type1 = "Fire", Type2 = "Flying", HP = 78, Attack = 84, Defense = 78, SpecialAttack = 109, SpecialDefense = 85, Speed = 100 },
            new Pokemon { Id = 7, Name = "Squirtle", Type1 = "Water", Type2 = "", HP = 44, Attack = 48, Defense = 65, SpecialAttack = 50, SpecialDefense = 64, Speed = 43 },
            new Pokemon { Id = 8, Name = "Wartortle", Type1 = "Water", Type2 = "", HP = 59, Attack = 63, Defense = 80, SpecialAttack = 65, SpecialDefense = 80, Speed = 58 },
            new Pokemon { Id = 9, Name = "Blastoise", Type1 = "Water", Type2 = "", HP = 79, Attack = 83, Defense = 100, SpecialAttack = 85, SpecialDefense = 105, Speed = 78 },
            new Pokemon { Id = 25, Name = "Pikachu", Type1 = "Electric", Type2 = "", HP = 35, Attack = 55, Defense = 40, SpecialAttack = 50, SpecialDefense = 50, Speed = 90 },
            new Pokemon { Id = 143, Name = "Snorlax", Type1 = "Normal", Type2 = "", HP = 160, Attack = 110, Defense = 65, SpecialAttack = 65, SpecialDefense = 110, Speed = 30 },
            new Pokemon { Id = 149, Name = "Dragonite", Type1 = "Dragon", Type2 = "Flying", HP = 91, Attack = 134, Defense = 95, SpecialAttack = 100, SpecialDefense = 100, Speed = 80 },
            new Pokemon { Id = 150, Name = "Mewtwo", Type1 = "Psychic", Type2 = "", HP = 106, Attack = 110, Defense = 90, SpecialAttack = 154, SpecialDefense = 90, Speed = 130 },
            new Pokemon { Id = 151, Name = "Mew", Type1 = "Psychic", Type2 = "", HP = 100, Attack = 100, Defense = 100, SpecialAttack = 100, SpecialDefense = 100, Speed = 100 }
        };
    }

    public IQueryable<Pokemon> GetPokemon() => _pokemon.AsQueryable();
}
