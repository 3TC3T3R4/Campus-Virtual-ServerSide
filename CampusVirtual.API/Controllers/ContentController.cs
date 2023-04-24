using AutoMapper;
using CampusVirtual.Domain.Commands.Content;
using CampusVirtual.Domain.Entities;
using CampusVirtual.Domain.Entities.Wrappers.Content;
using CampusVirtual.UseCases.Gateway;
using Microsoft.AspNetCore.Mvc;

namespace CampusVirtual.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContentController : ControllerBase
	{
		private readonly IContentUseCase _contentUseCase;
		private readonly IMapper _mapper;

		public ContentController(IContentUseCase contentUseCase, IMapper mapper)
		{
			_contentUseCase = contentUseCase;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<string> Create_Content(CreateContentCommand command)
		{
			return await _contentUseCase.CreateContentAsync(_mapper.Map<Content>(command));
		}

		[HttpGet]
		public async Task<List<ContentWithDeliveries>> Get_All_Content()
		{
			return await _contentUseCase.GetContentsAsync();
		}
	}
}
