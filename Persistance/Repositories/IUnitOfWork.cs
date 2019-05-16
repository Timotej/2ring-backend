using System.Threading.Tasks;

namespace project
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}