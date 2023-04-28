namespace CampusVirtual.Domain.Commands.LearningPath
{
    public class UpdateLearningPaths
    {
        public string CoachID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        //public decimal Duration { get; set; }
        //public int StatePath { get; set; }
    }
}