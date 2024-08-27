namespace AgentRestApi.Models
{
    public class MissionModel
    {
        public enum Status
        {
            taskProposal,
            TheMissionIsOccupied,
            TheMissionIsOver

        }
        public int Id { get; set; }

        public int TargetId { get; set; }

        public int AgentId { get; set; }
        public TargetModel Targets { get; set; }
        public AgentModel Agent { get; set; }

        public double TimeLeft { get; set; }

        //Task start time is used to check how much time was taken at the end of the task
        public DateTime taskStartTime { get; set; }
        public string? ExecuteTime {  get; set; }

        public Status MissionStatus { get; set; }

    }
}
