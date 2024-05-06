using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IAttachmentRepository AttachmentRepository { get; }
        IClientRepository ClientRepository { get; }
        IGenericAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<IDbContextTransaction> BeginTransactionAsync();
        void CommitTransaction();
        void RollbackTransaction();
        Task<int> SaveChangesAsync();
    }
}
