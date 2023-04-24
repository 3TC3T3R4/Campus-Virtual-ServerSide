using CampusVirtual.Domain.Common;

namespace CampusVirtual.Domain.Commands.Content
{
	public class UpdateContentCommand
	{
		public Guid CourseID { get; private set; }

		public string? Title { get; private set; }

		public string? Description { get; private set; }

		public string? DeliveryField { get; private set; }

		public Enums.TypeContent Type { get; private set; }

		public decimal Duration { get; private set; }

		public Enums.StateContent StateContent { get; private set; }
	}
}
