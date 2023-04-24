﻿

using CampusVirtual.Domain.Common;

namespace CampusVirtual.Domain.Entities.Wrappers.Content
{
	public class ContentWithDelivery
	{
		public Guid ContentID { get; set; }

		public Guid CourseID { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string DeliveryField { get; set; }

		public Enums.TypeContent Type { get; set; }

		public decimal Duration { get; set; }

		public Enums.StateContent StateContent { get; set; }

		//public Delivery Deliveries { get; set; }
	}
}