using System.Threading.Tasks;
using System.Collections.Generic;

namespace project
{
    public interface ISinglePositionDurationRepository
    {
        Task<IEnumerable<SinglePositionDuration>> ListAsync();
        Task<SinglePositionDuration> FindByIdAsync(int id);
        Task<IEnumerable<SinglePositionDuration>> ListByEmployeeIdAsync(int employeeId);
        Task AddAsync(SinglePositionDuration positionDuration);
        void Update(SinglePositionDuration positionDuration);
    }
}