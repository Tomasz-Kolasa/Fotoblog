using AutoMapper;
using Fotoblog.DAL;

namespace Fotoblog.BLL.Services
{
    public abstract class BaseService
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly IMapper _mapper;

        protected BaseService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
    }
}
