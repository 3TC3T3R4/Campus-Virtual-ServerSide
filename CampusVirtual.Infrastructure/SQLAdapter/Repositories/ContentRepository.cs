
using Ardalis.GuardClauses;
using AutoMapper;
using CampusVirtual.Domain.Common;
using CampusVirtual.Domain.Entities;
using CampusVirtual.Domain.Entities.Wrappers.Content;
using CampusVirtual.Infrastructure.SQLAdapter.Gateway;
using CampusVirtual.UseCases.Gateway.Repositories;
using Dapper;
using System.Text.Json;
using static CampusVirtual.Domain.Common.Enums;

namespace CampusVirtual.Infrastructure.SQLAdapter.Repositories
{
	public class ContentRepository : IContentRepository
	{
		private readonly IDbConnectionBuilder _dbConnectionBuilder;
		private readonly string _tableNameContents = "Contents";

		private readonly IMapper _mapper;

		public ContentRepository(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
		{
			_dbConnectionBuilder = dbConnectionBuilder;
			_mapper = mapper;
		}

		public async Task<string> CreateContentAsync(Content content)
		{
			Guard.Against.Null(content, nameof(content));
			Guard.Against.NullOrEmpty(content.CourseID, nameof(content.CourseID));
			Guard.Against.NullOrEmpty(content.Title, nameof(content.Title));
			Guard.Against.NullOrEmpty(content.Description, nameof(content.Description));
			Guard.Against.NullOrEmpty(content.Type.ToString(), nameof(content.Type));
			Guard.Against.NullOrEmpty(content.Duration.ToString(), nameof(content.Duration));

			Content.SetDetailsContentEntity(content);

			var connection = await _dbConnectionBuilder.CreateConnectionAsync();

			var sql = $"INSERT INTO {_tableNameContents} (courseID, title, description, deliveryField, type, duration, stateContent) " +
				$"VALUES (@CourseID, @Title, @Description, @DeliveryField, @Type, @Duration, @StateContent);";
			await connection.ExecuteScalarAsync(sql, content);

			connection.Close();
			return JsonSerializer.Serialize("Created");

		}

		public async Task<string> DeleteContentAsync(string idContent)
		{
			var connection = await _dbConnectionBuilder.CreateConnectionAsync();
			var queryId = $"SELECT * FROM {_tableNameContents} WHERE contentID = @Id AND stateContent = 1";

			var parseId = new { Id = Guid.Parse(idContent) };

			var entityToDelete = await connection.QueryFirstOrDefaultAsync<Content>(queryId, parseId);

			if (entityToDelete == null)
			{
				throw new ArgumentException("Content not found");
			}

			var query = $"UPDATE {_tableNameContents} SET stateContent = @StateContent WHERE contentID = @ContentId";

			entityToDelete.SetStateContent(Enums.StateContent.Deleted);

			await connection.ExecuteScalarAsync(query, entityToDelete);

			connection.Close();
			return JsonSerializer.Serialize("Deleted");
		}

		public async Task<ContentWithDelivery>  GetContentByIdAsync(string idContent)
		{
			var connection = await _dbConnectionBuilder.CreateConnectionAsync();

			string query = $"SELECT * FROM {_tableNameContents} WHERE contentID = @Id AND stateContent = 1";
			var parameters = new { Id = Guid.Parse(idContent) };

			var result = await connection.QueryFirstOrDefaultAsync<ContentWithDelivery>(query, parameters);

			if (result == null)
			{
				throw new ArgumentException("Content not found");
			}

			connection.Close();
			return result;
		}

		public async Task<List<ContentWithDeliveries>> GetContentsAsync()
		{
			var connection = await _dbConnectionBuilder.CreateConnectionAsync();

			string query = $"SELECT * FROM {_tableNameContents} WHERE stateContent = 1";
			var result = await connection.QueryAsync<ContentWithDeliveries>(query);

			if (result == null)
			{
				throw new ArgumentException("Content not found");
			}

			connection.Close();
			return result.ToList();
		}

		public async Task<string> UpdateContentAsync(string idContent, Content content)
		{
			Guard.Against.Null(content, nameof(content));
			Guard.Against.NullOrEmpty(content.CourseID, nameof(content.CourseID));
			Guard.Against.NullOrEmpty(content.Title, nameof(content.Title));
			Guard.Against.NullOrEmpty(content.Description, nameof(content.Description));
			Guard.Against.NullOrEmpty(content.Type.ToString(), nameof(content.Type));
			Guard.Against.NullOrEmpty(content.Duration.ToString(), nameof(content.Duration));

			var connection = await _dbConnectionBuilder.CreateConnectionAsync();
			var queryId = $"SELECT * FROM {_tableNameContents} WHERE contentID = @Id AND stateContent = 1";

			var parseId = new { Id = Guid.Parse(idContent) };

			var entityToUpdate = await connection.QueryFirstOrDefaultAsync<Content>(queryId, parseId);

			if (entityToUpdate == null)
			{
				throw new ArgumentException("Content not found");
			}

			var query = $"UPDATE {_tableNameContents} SET courseID = @CourseID, title = @Title, description = @Description, deliveryField = @DeliveryField," +
				$" type = @Type, duration = @Duration, stateContent = @StateContent WHERE contentID = @ContentId";

			entityToUpdate.SetCourseID(content.CourseID);
			entityToUpdate.SetTitle(content.Title);
			entityToUpdate.SetDescription(content.Description);
			entityToUpdate.SetDeliveryField(content.DeliveryField);
			entityToUpdate.SetType(content.Type);
			entityToUpdate.SetDuration(content.Duration);
			entityToUpdate.SetStateContent(content.StateContent);
			entityToUpdate.SetContentID(Guid.Parse(idContent));

			await connection.ExecuteScalarAsync(query, entityToUpdate);

			connection.Close();
			return JsonSerializer.Serialize("Updated");
		}


		public async Task<List<ContentWithDelivery>> GetContentByCourseIdAsync(string courseId)
		{
			var connection = await _dbConnectionBuilder.CreateConnectionAsync();

			string query = $"SELECT * FROM {_tableNameContents} WHERE courseID = @Id AND stateContent = 1";
			var parameters = new { Id = Guid.Parse(courseId) };

			var result = await connection.QueryAsync<ContentWithDelivery>(query, parameters);

			if (result.Count() == 0)
			{
				throw new ArgumentException("Content not found");
			}

			connection.Close();
			return result.ToList();
		}



	}
}
