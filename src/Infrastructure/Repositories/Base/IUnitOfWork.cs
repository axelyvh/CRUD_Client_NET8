using Application.Repositories;
using Application.Repositories.Base;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections;

namespace Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {

        private Hashtable _repositories;
        private readonly ApplicationDbContext _context;

        private IClientRepository _clientRepository;
        private IAttachmentRepository _attachmentRepository;

        public IClientRepository ClientRepository => _clientRepository ??= new ClientRepository(_context);
        public IAttachmentRepository AttachmentRepository => _attachmentRepository ??= new AttachmentRepository(_context);

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationDbContext ApplicationDbContext => _context;

        public IGenericAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericAsyncRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericAsyncRepository<TEntity>)_repositories[type];
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

    }
}
