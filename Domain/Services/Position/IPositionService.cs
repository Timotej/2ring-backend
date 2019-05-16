using System.Threading.Tasks;
using System.Collections.Generic;

namespace project
{
    public interface IPositionService
    {
        Task<IEnumerable<Position>> ListAsync();
        Task<PositionResponse> SaveAsync(Position position);
        Task<PositionResponse> DeleteAsync(int id);
        Task<PositionResponse> RestoreAsync(int id);
    }
}