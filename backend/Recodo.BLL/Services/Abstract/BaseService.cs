using AutoMapper;
using Recodo.DAL.Context;

namespace Recodo.BLL.Services.Abstract
{
    public class BaseService
    {
        private protected readonly RecodoDbContext _context;
        private protected readonly IMapper _mapper;

        public BaseService(RecodoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
