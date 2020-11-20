using Common.DAL.Context;
using Common.DAL.Models;
using System;

namespace Common.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly dbContext _context = new dbContext();
        private GenericRepository<Goals> _goalsRepository;
        private GenericRepository<TrainingData> _TrainingDataRepository;
        private GenericRepository<TrainingSession> _TrainingSessionRepository;
        private GenericRepository<Users> _usersRepository;
        private GenericRepository<Sport> _sportRepository;
        private GenericRepository<Met> _metRepository;


        public GenericRepository<Goals> GoalsRepository => _goalsRepository ?? (_goalsRepository = new GenericRepository<Goals>(_context));
        public GenericRepository<TrainingData> TrainingDataRepository => _TrainingDataRepository ?? (_TrainingDataRepository = new GenericRepository<TrainingData>(_context));
        public GenericRepository<TrainingSession> TrainingSessionRepository => _TrainingSessionRepository ?? (_TrainingSessionRepository = new GenericRepository<TrainingSession>(_context));
        public GenericRepository<Users> UsersRepository => _usersRepository ?? (_usersRepository = new GenericRepository<Users>(_context));
        public GenericRepository<Sport> SportRepository => _sportRepository ?? (_sportRepository = new GenericRepository<Sport>(_context));
        public GenericRepository<Met> MetRepository => _metRepository ?? (_metRepository = new GenericRepository<Met>(_context));

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}