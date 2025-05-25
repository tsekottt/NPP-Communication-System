using DAL.Repositories.Impl;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;
using System;

namespace DAL.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly NPPCSContext db;

        private UserRepository userRepository;
        private RequestRepository requestRepository;

        public EFUnitOfWork(NPPCSContext context)
        {
            db = context;
        }

        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRequestRepository Requests
        {
            get
            {
                if (requestRepository == null)
                    requestRepository = new RequestRepository(db);
                return requestRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
