namespace AgentRestApi.Models
{
    public class MissionModel
    {
        public enum Status
        {
            taskProposal,
            MitzvahForTheTask,
            TheMissionIsOver

        }
        public int Id { get; set; }

        public int TargetId { get; set; }

        public int AgentId { get; set; }
        public TagentModel Targets { get; set; }
        public AgentModel Agent { get; set; }

        public double TimeLeft { get; set; }

        public double ExecuteTime {  get; set; }

        public Status MissionStatus { get; set; }

    }
}
