using System.Threading.Tasks;
using System.Collections.Generic;

namespace project
{
    public interface IPositionRepository
    {
        Task<IEnumerable<Position>> ListAsync();
        Task AddAsync(Position position);
        Task<Position> FindByIdAsync(int id);
        void Update(Position position);
    }
}