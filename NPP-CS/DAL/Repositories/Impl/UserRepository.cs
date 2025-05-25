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
    public class UserRepository
          : BaseRepository<User>, IUserRepository
    {

        public UserRepository(NPPCSContext context)
            : base(context)
        {
        }
        public User GetByUsername(string username)
        {
            return _set.FirstOrDefault(u => u.Username == username);
        }
    }
}
