using CampusVirtual.Domain.Entities;
using CampusVirtual.Domain.Entities.Wrappers.Content;
using CampusVirtual.UseCases.Gateway;
using CampusVirtual.UseCases.Gateway.Repositories;

namespace CampusVirtual.UseCases.UseCases
{
	public class ContentUseCase : IContentUseCase
	{
		private readonly IContentRepository _contentRepository;

		public ContentUseCase(IContentRepository contentRepository)
		{
			_contentRepository = contentRepository;
		}

		public async Task<string> CreateContentAsync(Content content)
		{
			return await _contentRepository.CreateContentAsync(content);
		}

		public async Task<string> DeleteContentAsync(string idContent)
		{
			return await _contentRepository.DeleteContentAsync(idContent);
		}

		public async Task<string> UpdateContentAsync(string idContent, Content content)
		{
			return await _contentRepository.UpdateContentAsync(idContent, content);
		}

		public async Task<List<ContentWithDeliveries>> GetContentsAsync()
		{
			return await _contentRepository.GetContentsAsync();
		}

		public async Task<ContentWithDelivery> GetContentByIdAsync(string idContent)
		{
			return await _contentRepository.GetContentByIdAsync(idContent);
		}

		public async Task<List<ContentWithDelivery>> GetContentByCourseIdAsync(string courseId)
		{
			return await _contentRepository.GetContentByCourseIdAsync(courseId);
		}
	}
}
