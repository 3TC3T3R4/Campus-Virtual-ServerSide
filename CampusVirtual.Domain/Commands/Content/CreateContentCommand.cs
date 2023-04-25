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
		public Guid CourseID { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public Enums.TypeContent Type { get; set; }

		public decimal Duration { get; set; }
	}
}
