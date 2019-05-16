using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace project
{
    public class SinglePositionDurationService : ISinglePositionDurationService
    {
        private readonly ISinglePositionDurationRepository _singlePositionDurationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SinglePositionDurationService(ISinglePositionDurationRepository singlePositionDurationRepository, IUnitOfWork unitOfWork)
        {
            _singlePositionDurationRepository = singlePositionDurationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SinglePositionDuration>> ListAsync()
        {
            return await _singlePositionDurationRepository.ListAsync();
        }

        public async Task<IEnumerable<SinglePositionDuration>> ListByIdAsync(int id)
        {
            return await _singlePositionDurationRepository.ListByEmployeeIdAsync(id);
        }

        public async Task<SinglePositionDurationResponse> SaveAsync(SinglePositionDuration positionDuration)
        {
            try
            {
                await _singlePositionDurationRepository.AddAsync(positionDuration);
                await _unitOfWork.CompleteAsync();

                return new SinglePositionDurationResponse(positionDuration);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SinglePositionDurationResponse($"An error occurred when saving the position duration: {ex.Message}");
            }
        }

        public async Task<SinglePositionDurationResponse> UpdateAsync(int id, SinglePositionDuration positionDuration)
        {
            var existingPositionDuration = await _singlePositionDurationRepository.FindByIdAsync(id);

            if (existingPositionDuration == null)
                return new SinglePositionDurationResponse("Position duration not found.");

            existingPositionDuration.StartDate = positionDuration.StartDate;
            existingPositionDuration.EndDate = positionDuration.EndDate;

            try
            {
                _singlePositionDurationRepository.Update(existingPositionDuration);
                await _unitOfWork.CompleteAsync();

                return new SinglePositionDurationResponse(existingPositionDuration);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SinglePositionDurationResponse($"An error occurred when updating the position duration: {ex.Message}");
            }
        }
    }
}