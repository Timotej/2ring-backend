using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace project
{
    public class PositionService : IPositionService
    {

        private readonly IPositionRepository _positionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PositionService(IPositionRepository positionRepository, IUnitOfWork unitOfWork)
        {
            _positionRepository = positionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Position>> ListAsync()
        {
            return await _positionRepository.ListAsync();
        }
        public async Task<PositionResponse> SaveAsync(Position position)
        {
            try
            {
                await _positionRepository.AddAsync(position);
                await _unitOfWork.CompleteAsync();

                return new PositionResponse(position);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new PositionResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<PositionResponse> DeleteAsync(int id)
        {
            var existingCategory = await _positionRepository.FindByIdAsync(id);

            if (existingCategory == null)
                return new PositionResponse("Category not found.");

            try
            {
                existingCategory.IsDeleted = true;
                _positionRepository.Update(existingCategory);
                await _unitOfWork.CompleteAsync();

                return new PositionResponse(existingCategory);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new PositionResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }

        public async Task<PositionResponse> RestoreAsync(int id)
        {

            var existingCategory = await _positionRepository.FindByIdAsync(id);

            if (existingCategory == null)
                return new PositionResponse("Category not found.");

            try
            {
                existingCategory.IsDeleted = false;
                _positionRepository.Update(existingCategory);
                await _unitOfWork.CompleteAsync();

                return new PositionResponse(existingCategory);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new PositionResponse($"An error occurred when restoring the category: {ex.Message}");
            }
        }


    }
}