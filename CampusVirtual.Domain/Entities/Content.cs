using CampusVirtual.Domain.Common;


namespace CampusVirtual.Domain.Entities
{
	public class Content
	{
		public Guid  ContentID { get; private set; }

		public Guid  CourseID { get; private set; }

		public string Title { get; private set; }

		public string Description { get; private set; }

		public string DeliveryField { get; private set; }

		public Enums.TypeContent Type { get; private set; }

		public decimal Duration { get; private set; }

		public Enums.StateContent StateContent { get; private set; }

		public Content()
		{
		}


		public static Content SetDetailsContentEntity(Content content)
		{
			content.StateContent = Enums.StateContent.Active;

			return content;
		}



		public void SetContentID(Guid contentID)
		{
			ContentID = contentID;
		}

		public void SetCourseID(Guid courseID)
		{
			CourseID = courseID;
		}

		public void SetTitle(string title)
		{
			Title = title;
		}

		public void SetDescription(string description)
		{
			Description = description;
		}

		public void SetDeliveryField(string deliveryField)
		{
			DeliveryField = deliveryField;
		}

		public void SetType(Enums.TypeContent type)
		{
			Type = type;
		}

		public void SetDuration(decimal duration)
		{
			Duration = duration;
		}

		public void SetStateContent(Enums.StateContent stateContent)
		{
			StateContent = stateContent;
		}


	}
}
