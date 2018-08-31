using System.Threading.Tasks;

namespace Santex.League.Domain.Repository
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
