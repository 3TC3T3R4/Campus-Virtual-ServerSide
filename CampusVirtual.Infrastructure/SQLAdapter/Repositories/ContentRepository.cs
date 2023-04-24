
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
		private readonly string _tableNameDeliveries = "Deliveries";
		private readonly string _tableNameCourses = "Courses";

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
			Guard.Against.NullOrEmpty(content.DeliveryField, nameof(content.DeliveryField));
			Guard.Against.NullOrEmpty(content.Type.ToString(), nameof(content.Type));
			Guard.Against.NullOrEmpty(content.Duration.ToString(), nameof(content.Type));

			Content.SetDetailsContentEntity(content);

			var connection = await _dbConnectionBuilder.CreateConnectionAsync();

			//var sqlAccount = $"SELECT * FROM {_tableNameCourses} WHERE CourseID = {content.CourseID}";
			//var accountToUpdate = await connection.QuerySingleAsync<Course>(sqlAccount) ?? throw new Exception("The course doesn't exist");

			var sql = $"INSERT INTO {_tableNameContents} (courseID, title, description, deliveryField, type, duration, stateContent) " +
				$"VALUES (@CourseID, @Title, @Description, @DeliveryField, @Type, @Duration, @StateContent);";
			var result = await connection.ExecuteScalarAsync(sql, content);
			connection.Close();
			return JsonSerializer.Serialize("Created");

		}

		public async Task<string> DeleteContentAsync(string idContent)
		{
			var connection = await _dbConnectionBuilder.CreateConnectionAsync();

			var query = $"UPDATE {_tableNameContents} SET stateContent = @StateContent WHERE contentID = @ContentId";

			var parameters = new
			{
				StateContent = Enums.StateContent.Deleted,
				ContentId = Guid.Parse(idContent)
			};

			await connection.ExecuteScalarAsync(query, parameters);

			connection.Close();
			return JsonSerializer.Serialize("Deleted");
		}

		public async Task<ContentWithDelivery>  GetContentByIdAsync(string idContent)
		{
			var connection = await _dbConnectionBuilder.CreateConnectionAsync();

			string query = $"SELECT * FROM {_tableNameContents} WHERE contentID = @Id";
			var parameters = new { Id = Guid.Parse(idContent) };

			var resultado = await connection.QueryFirstOrDefaultAsync<ContentWithDelivery>(query, parameters);

			connection.Close();
			return resultado;
		}

		public async Task<List<ContentWithDeliveries>> GetContentsAsync()
		{
			var connection = await _dbConnectionBuilder.CreateConnectionAsync();

			string query = $"SELECT * FROM {_tableNameContents}";
			var resultado = await connection.QueryAsync<ContentWithDeliveries>(query);

			connection.Close();
			return resultado.ToList();
		}

		public async Task<string> UpdateContentAsync(string idContent, Content content)
		{
			var connection = await _dbConnectionBuilder.CreateConnectionAsync();

			var query = $"UPDATE {_tableNameContents} SET courseID = @CourseID, title = @Title, description = @Description, deliveryField = @DeliveryField," +
				$" type = @Type, duration = @Duration, stateContent = @StateContent WHERE contentID = @ContentId";

			var parameters = new { 
				CourseID = content.CourseID, 
				Title = content.Title,
				Description = content.Description, 
				DeliveryField = content.DeliveryField,
				Type = content.Type,
				Duration = content.Duration,
				StateContent = content.StateContent,
				ContentId = Guid.Parse(idContent)
			};

			await connection.ExecuteScalarAsync(query, parameters);

			connection.Close();
			return JsonSerializer.Serialize("Updated");
		}
	}
}
