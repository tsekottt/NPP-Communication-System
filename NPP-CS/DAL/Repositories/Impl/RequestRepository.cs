using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Impl
{
    public class RequestRepository
          : BaseRepository<Request>, IRequestRepository
    {

        public RequestRepository(NPPCSContext context)
            : base(context)
        {
        }
        public IEnumerable<Request> GetByUserId(int userId)
        {
            return _set.Where(r => r.UserId == userId).ToList();
        }
    }
}
