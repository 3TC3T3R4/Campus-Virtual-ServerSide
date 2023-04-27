using CampusVirtual.Domain.Commands.LearningPath;
using CampusVirtual.Domain.Entities;
using CampusVirtual.UseCases.Gateway.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPathRepository
{
    public class LlearningPathTest
    {
        private readonly Mock<ILearningPathRepository> _mockLearningPathRepository;
        public LlearningPathTest()
        {
            _mockLearningPathRepository = new();
        }

        [Fact]

        public async Task GetLearningPathsAsync()
        {
            // Arrange
            var learningPath = new List<LearningPath>
            {
                new LearningPath
                {
                    PathID = Guid.NewGuid(),
                    CoachID = "test",
                    Title = "Test LearningPath",
                    Description = "Test Description",
                    Duration = 10,
                    StatePath = 60,
                },
                new LearningPath
                {
                    PathID = Guid.NewGuid(),
                    CoachID = "test",
                    Title = "Test LearningPath",
                    Description = "Test Description",
                    Duration = 10,
                    StatePath = 60,
                }
            };

            _mockLearningPathRepository.Setup(x => x.GetLearningPathsAsync()).ReturnsAsync(learningPath);

            //Act
            var result = await _mockLearningPathRepository.Object.GetLearningPathsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(learningPath, result);

        }

        [Fact]

        public async Task CreateLearningPathAsync()
        {
            // Arrange
            var newLearningPath = new LearningPath
            {
                PathID = Guid.NewGuid(),
                CoachID = "test",
                Title = "Test LearningPath",
                Description = "Test Description",
                Duration = 10,
                StatePath = 60,
            };

            var learningPathCreated = new LearningPath
            {
                CoachID = "test",
                Title = "Test LearningPath",
                Description = "Test Description",
            };

            _mockLearningPathRepository.Setup(x => x.CreateLearningPathAsync(newLearningPath)).ReturnsAsync(learningPathCreated);

            //Act
            var result = await _mockLearningPathRepository.Object.CreateLearningPathAsync(newLearningPath);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(learningPathCreated, result);
        }

        [Fact]

        public async Task Delete_LearningPath_By_Id()
        {
            //arrange
            var learningToDelete = new LearningPath
            {
                PathID = Guid.NewGuid(),
            };

            _mockLearningPathRepository.Setup(x => x.DeleteLearningPathByIdAsync("test-learning-123")).ReturnsAsync("Delete Successful for ID: test-learning-123");

            // act
            var result = await _mockLearningPathRepository.Object.DeleteLearningPathByIdAsync("test-learning-123");

            // assert
            Assert.NotNull(result);
            Assert.Equal("Delete Successful for ID: test-learning-123", result);
        }

        [Fact]

        public async Task Get_LearningPath_Id()
        {
            //Arrange
            var learning = new LearningPath
            {
                PathID = Guid.NewGuid(),
                CoachID = "test",
                Title = "Test LearningPath",
                Description = "Test Description",
                Duration = 10,
                StatePath = 60,
            };
            var learningId = new LearningPath();
            _mockLearningPathRepository.Setup(x => x.GetLearningPathsByIdAsync("1")).ReturnsAsync(learningId);

            //Act
            var result = await _mockLearningPathRepository.Object.GetLearningPathsByIdAsync("1");

            //Assert
            Assert.NotNull(result);
            Assert.Equal(learningId, result);

        }
          

    }
}
