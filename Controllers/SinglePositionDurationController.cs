using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace project
{
    [Route("/api/[controller]")]
    public class SinglePositionDurationController : Controller
    {
        private readonly ISinglePositionDurationService _singlePosService;
        private readonly IMapper _mapper;
        public SinglePositionDurationController(ISinglePositionDurationService singlePosService, IMapper mapper)
        {
            _singlePosService = singlePosService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SinglePositionDurationResource>> GetPositionDurationsAsync()
        {
            var positionDurations = await _singlePosService.ListAsync();
            var mappedPositionDurations = _mapper.Map<IEnumerable<SinglePositionDuration>, IEnumerable<SinglePositionDurationResource>>(positionDurations);
            return mappedPositionDurations;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<SinglePositionDurationResource>> GetPositionDurationByIdAsync(int id)
        {
            var positionDurations = await _singlePosService.ListByIdAsync(id);
            var mappedPositionDurations = _mapper.Map<IEnumerable<SinglePositionDuration>, IEnumerable<SinglePositionDurationResource>>(positionDurations);
            return mappedPositionDurations;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SinglePositionDurationResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var position = _mapper.Map<SinglePositionDurationResource, SinglePositionDuration>(resource);
            var result = await _singlePosService.SaveAsync(position);

            if (!result.Success)
                return BadRequest(result.Message);

            var positionResource = _mapper.Map<SinglePositionDuration, SinglePositionDurationResource>(result.SinglePositionDuration);
            return Ok(positionResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSinglePositionDurationResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var positionDuration = _mapper.Map<SaveSinglePositionDurationResource, SinglePositionDuration>(resource);
            var result = await _singlePosService.UpdateAsync(id, positionDuration);

            if (!result.Success)
                return BadRequest(result.Message);

            var employeeResource = _mapper.Map<SinglePositionDuration, SinglePositionDurationResource>(result.SinglePositionDuration);
            return Ok(employeeResource);
        }
    }
}