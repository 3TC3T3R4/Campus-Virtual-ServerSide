
using Ardalis.GuardClauses;
using AutoMapper;
using CampusVirtual.Domain.Entities;
using CampusVirtual.Domain.Entities.Wrappers.Content;
using CampusVirtual.Infrastructure.SQLAdapter.Gateway;
using CampusVirtual.UseCases.Gateway.Repositories;
using Dapper;
using System.Text.Json;
using System.Transactions;

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

		public Task<string> DeleteContentAsync(string idContent)
		{
			throw new NotImplementedException();
		}

		public Task<ContentWithDelivery>  GetContentByIdAsync(string idContent)
		{
			throw new NotImplementedException();
		}

		public async Task<List<ContentWithDeliveries>> GetContentsAsync()
		{
			var connection = await _dbConnectionBuilder.CreateConnectionAsync();

			string query = $"SELECT * FROM {_tableNameContents}";
			var resultado = await connection.QueryAsync<ContentWithDeliveries>(query);

			connection.Close();
			return resultado.ToList();
		}

		public Task<string> UpdateContentAsync(string idContent, Content content)
		{
			throw new NotImplementedException();
		}
	}
}
