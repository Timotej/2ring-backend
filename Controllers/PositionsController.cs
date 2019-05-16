using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace project
{
    [Route("/api/[controller]")]
    public class PositionsController : Controller
    {
        private readonly IPositionService _positionService;
        private readonly IMapper _mapper;
        public PositionsController(IPositionService positionService, IMapper mapper)
        {
            _positionService = positionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PositionResource>> GetAllAsync()
        {
            var positions = await _positionService.ListAsync();
            var mappedPositions = _mapper.Map<IEnumerable<Position>, IEnumerable<PositionResource>>(positions);
            return mappedPositions;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SavePositionResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var position = _mapper.Map<SavePositionResource, Position>(resource);
            var result = await _positionService.SaveAsync(position);

            if (!result.Success)
                return BadRequest(result.Message);

            var positionResource = _mapper.Map<Position, PositionResource>(result.Position);
            return Ok(positionResource);
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _positionService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<Position, PositionResource>(result.Position);
            return Ok(categoryResource);
        }

        [HttpPut("restore/{id}")]
        public async Task<IActionResult> RestoreAsync(int id)
        {
            var result = await _positionService.RestoreAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<Position, PositionResource>(result.Position);
            return Ok(categoryResource);
        }
    }
}