using CampusVirtual.Domain.Entities;
using CampusVirtual.Domain.Entities.Wrappers.Content;

namespace CampusVirtual.UseCases.Gateway.Repositories
{
    public interface IContentRepository
	{
		Task<string>CreateContentAsync(Content content);
		Task<string>UpdateContentAsync(string idContent, Content content);
		Task<string>DeleteContentAsync(string idContent);
		Task<List<ContentWithDeliveries>>GetContentsAsync();
		Task<ContentWithDelivery> GetContentByIdAsync(string idContent);
	}
}
