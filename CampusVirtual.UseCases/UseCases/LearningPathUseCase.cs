using CampusVirtual.Domain.Commands.LearningPath;
using CampusVirtual.Domain.Entities;
using CampusVirtual.UseCases.Gateway;
using CampusVirtual.UseCases.Gateway.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.UseCases.UseCases
{
    public class LearningPathUseCase : ILearningPathUseCase
    {
        private readonly ILearningPathRepository _learningPathRepository;

        public LearningPathUseCase(ILearningPathRepository learningPathRepository)
        {
            _learningPathRepository = learningPathRepository;
        }

        public async Task<InsertNewLearningPath> CreateLearningPathAsync(LearningPath learningPath)
        {
            return await _learningPathRepository.CreateLearningPathAsync(learningPath);
        }

        public async Task<List<LearningPath>> GetLearningPathsAsync()
        {
            return await _learningPathRepository.GetLearningPathsAsync();
        }

        public async Task<List<LearningPath>> GetLearningPathsByCoachAsync(string coachID)
        {
            return await _learningPathRepository.GetLearningPathsByCoachAsync(coachID);
        }
    }
}
