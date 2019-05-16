using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace project
{
    public class SinglePositionDurationRepository : BaseRepository, ISinglePositionDurationRepository
    {
        public SinglePositionDurationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SinglePositionDuration>> ListAsync()
        {
            return await _context.PositionsDuration.ToListAsync();
        }
        public async Task<SinglePositionDuration> FindByIdAsync(int id)
        {
            return await _context.PositionsDuration.FindAsync(id);
        }
        public async Task<IEnumerable<SinglePositionDuration>> ListByEmployeeIdAsync(int employeeId)
        {
            var singlePositions = await _context.PositionsDuration.ToListAsync();
            return singlePositions.Where(x => x.EmployeeId == employeeId);
        }
        public async Task AddAsync(SinglePositionDuration positionDuration)
        {
            await _context.AddAsync(positionDuration);
        }
        public void Update(SinglePositionDuration positionDuration)
        {
            _context.Update(positionDuration);
        }
    }
}