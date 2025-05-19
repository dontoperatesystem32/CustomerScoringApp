using AutoMapper;
using ScoringSystem_web_api.Data;
using ScoringSystem_web_api.Dto;
using ScoringSystem_web_api.Models.CustomerModels;
using ScoringSystem_web_api.Models.ConditionModels;
using ScoringSystem_web_api.Models.Abstraction;

namespace ScoringSystem_web_api.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AccountRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Account GetAccount(int id)
        {
            return _context.Accounts.FirstOrDefault(a => a.Id == id);

        }
        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

    }
}
