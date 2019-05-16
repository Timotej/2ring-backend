using System.Threading.Tasks;
using System.Collections.Generic;

namespace project
{
    public interface ISinglePositionDurationService
    {
        Task<IEnumerable<SinglePositionDuration>> ListAsync();
        Task<IEnumerable<SinglePositionDuration>> ListByIdAsync(int id);
        Task<SinglePositionDurationResponse> SaveAsync(SinglePositionDuration positionDuration);
        Task<SinglePositionDurationResponse> UpdateAsync(int id, SinglePositionDuration category);

    }
}