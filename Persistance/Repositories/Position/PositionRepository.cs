using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace project
{
    public class PositionRepository : BaseRepository, IPositionRepository
    {
        public PositionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Position>> ListAsync()
        {
            return await _context.Positions.ToListAsync();
        }

        public async Task AddAsync(Position position)
        {
            await _context.Positions.AddAsync(position);
        }

        public async Task<Position> FindByIdAsync(int id)
        {
            return await _context.Positions.FindAsync(id);
        }

        public void Update(Position position)
        {
            _context.Positions.Update(position);
        }
    }
}