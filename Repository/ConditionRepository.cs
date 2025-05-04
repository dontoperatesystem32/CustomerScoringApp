using ScoringSystem_web_api.Data;
using ScoringSystem_web_api.Interfaces;
using ScoringSystem_web_api.Models.ConditionModels;

namespace ScoringSystem_web_api.Repository
{
    public class ConditionRepository : IConditionRepository
    {
        private readonly DataContext _context;
        public ConditionRepository(DataContext context)
        {
               _context = context;
        }

        public ICollection<BaseCondition> GetConditions()
        {
            return _context.ConditionStrategies.OrderBy(p => p.Id).ToList();
        }

        public bool ConditionExists(int conditionId)
        {
            return _context.ConditionStrategies.Any(с => с.Id == conditionId);
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };

            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon,
            };

            _context.Add(pokemonCategory);

            _context.Add(pokemon);

            return Save();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
