namespace CampusVirtual.Domain.Common
{
    public class Enums
    {
        public enum StateRegistration
        {
            Active = 1,
            Deleted = 2,
        }
        public enum StateContent
        {
            Active = 1,
            Deleted = 2
        }
        public enum TypeContent
        {
            Workshop = 1,
            Lesson = 2,
            Challenge = 3,
        }
        public enum StateCourse
        {
            Active = 1,
            Assigned = 2,
            Deleted = 3,
        }
    }
}