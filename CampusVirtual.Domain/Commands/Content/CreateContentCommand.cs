using CampusVirtual.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusVirtual.Domain.Commands.Content
{
	public class CreateContentCommand
	{
		public Guid CourseID { get; private set; }

		public string Title { get; private set; }

		public string Description { get; private set; }

		public string DeliveryField { get; private set; }

		public Enums.TypeContent Type { get; private set; }

		public decimal Duration { get; private set; }
	}
}
