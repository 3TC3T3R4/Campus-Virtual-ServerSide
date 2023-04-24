using CampusVirtual.Domain.Common;

namespace CampusVirtual.Domain.Commands.Content
{
	public class UpdateContentCommand
	{
		public Guid CourseID { get; set; }

		public string? Title { get; set; }

		public string? Description { get; set; }

		public string? DeliveryField { get; set; }

		public Enums.TypeContent Type { get; set; }

		public decimal Duration { get; set; }

		public Enums.StateContent StateContent { get; set; }
	}
}
